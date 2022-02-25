/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

using Fragcolor.Chainblocks.Claymore;
using Fragcolor.Chainblocks.Collections;
using Fragcolor.Chainblocks.UnityEditor.Settings;

using UnityEditor;

using UnityEngine;

namespace Fragcolor.Chainblocks.UnityEditor.GUI
{
  internal sealed class ChainblocksEditorWindow : EditorWindow
  {
    [MenuItem("Chainblocks/Editor")]
    private static void ShowWindow()
    {
      var window = GetWindow<ChainblocksEditorWindow>();
      window.titleContent.text = "Chainblocks Editor";
      window.Show();
    }

    private static LispEnv? _env;
    private static Node? _node;
    private static int _initializedState;

    private static readonly Dictionary<string, GetRequest> _requests = new Dictionary<string, GetRequest>();
    private string _hash = "";

    internal static ChainblocksSettings? Settings => ChainblocksSettingsDefaultObject.Settings;

    private static void EnsureInitialize()
    {
      if (Interlocked.CompareExchange(ref _initializedState, 1, 0) != 0)
        return;

      _env = new LispEnv();
      _node = new Node();
      EditorApplication.update += OnUpdate;
    }

    private void OnGUI()
    {
      if (Settings == null)
      {
        if (GUILayout.Button("Start using Chainblocks..."))
        {
          ChainblocksSettingsDefaultObject.GetOrCreateSettings();
        }

        return;
      }

      var contentRect = new Rect(0, 0, position.width, position.height);
      TopToolbar(contentRect);

      _hash = EditorGUILayout.TextField("Fragment hash", _hash);
      EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(_hash));
      if (GUILayout.Button(!_requests.ContainsKey(_hash) ? "Get Fragment" : "Cancel"))
      {
        EnsureInitialize();
        if (_requests.TryGetValue(_hash, out var request))
        {
          request.Cancel();
          _requests.Remove(_hash);
        }
        else
        {
          request = new GetRequest(_hash, _node);
          _requests.Add(_hash, request);
        }
      }

      EditorGUI.EndDisabledGroup();
    }

    private static void TopToolbar(Rect _)
    {
      if (Settings == null) return;

      GUILayout.BeginHorizontal(EditorStyles.toolbar);

      var cBuild = new GUIContent("Build");
      var rBuild = GUILayoutUtility.GetRect(cBuild, EditorStyles.toolbarDropDown);
      if (EditorGUI.DropdownButton(rBuild, cBuild, FocusType.Passive, EditorStyles.toolbarDropDown))
      {
        var menu = new GenericMenu();
        for (var i = 0; i < Settings.builders!.Count; i++)
        {
          var builder = Settings.builders[i];
          menu.AddItem(new GUIContent(builder.Name), false, OnBuild, i);
        }

        menu.DropDown(rBuild);
      }

      GUILayout.EndHorizontal();

      static void OnBuild(object boxed)
      {
        var options = BuildPlayerWindow.DefaultBuildMethods.GetBuildPlayerOptions(default);
        var builder = Settings!.builders![(int) boxed];
        builder.Build(options);
      }
    }

    private static void OnUpdate()
    {
      // tick all
      foreach (var request in _requests.Values)
      {
        request.Tick();
      }

      // deal with completed
      var completed = _requests.Where(x => x.Value.IsCompleted).ToList();
      if (completed.Count == 0) return;

      var importedAssets = new List<(string, string)>();
      AssetDatabase.StartAssetEditing();
      try
      {
        foreach (var kv in completed)
        {
          var hash = kv.Key;
          var request = kv.Value;
          var result = request.GetResult();
          if (result.Value.type != CBType.Table) continue;

          ref var container = ref result.Value.table.At("container");
          switch (container.GetString())
          {
            case "audio":
            {
              ref var data = ref result.Value.table.At("data");
              var bytes = data.GetBytes();
              if (bytes == null) continue;
              ImportAudioFile(hash, bytes, importedAssets);
              break;
            }

            case null:
              break;
          }
        }
      }
      finally
      {
        AssetDatabase.StopAssetEditing();

      }

      // clear
      foreach (var kv in completed)
        _requests.Remove(kv.Key);

      // locate the new assets and create or update entries
      foreach (var (path, hash) in importedAssets)
      {
        foreach (var candidate in AssetDatabase.FindAssets(Path.GetFileNameWithoutExtension(path)))
        {
          if (string.Equals(path, AssetDatabase.GUIDToAssetPath(candidate)))
          {
            var entry = Settings!.CreateOrMoveEntry(candidate, Settings.DefaultRegistry);
            entry.hash = hash;
            entry.localPath = path;
          }
        }
      }
      AssetDatabase.SaveAssetIfDirty(Settings!.DefaultRegistry);
    }

    private static void ImportAudioFile(string hash, byte[] bytes, ICollection<(string, string)> importedAssets)
    {
      var path = $"Assets/{hash}.ogg";
      // FIXME: this is a prototype implementation
      using var stream = File.Open(path, FileMode.Create, FileAccess.ReadWrite);
      stream.Write(bytes, 0, bytes.Length);
      stream.Flush();

      AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceSynchronousImport);
      importedAssets.Add((path, hash));
    }
  }
}
