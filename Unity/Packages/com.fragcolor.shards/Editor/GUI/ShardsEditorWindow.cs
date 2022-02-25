/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

using Fragcolor.Shards.Claymore;
using Fragcolor.Shards.Collections;
using Fragcolor.Shards.UnityEditor.Settings;

using UnityEditor;

using UnityEngine;

namespace Fragcolor.Shards.UnityEditor.GUI
{
  internal sealed class ShardsEditorWindow : EditorWindow
  {
    [MenuItem("Shards/Editor")]
    private static void ShowWindow()
    {
      var window = GetWindow<ShardsEditorWindow>();
      window.titleContent.text = "Shards Editor";
      window.Show();
    }

    private static ScriptingEnv? _env;
    private static Mesh? _mesh;
    private static int _initializedState;

    private static readonly Dictionary<string, GetRequest> _requests = new Dictionary<string, GetRequest>();
    private string _hash = "";

    internal static ShardsSettings? Settings => ShardsSettingsDefaultObject.Settings;

    private static void EnsureInitialize()
    {
      if (Interlocked.CompareExchange(ref _initializedState, 1, 0) != 0)
        return;

      _env = new ScriptingEnv();
      _mesh = new Mesh();
      EditorApplication.update += OnUpdate;
    }

    private void OnGUI()
    {
      if (Settings == null)
      {
        if (GUILayout.Button("Start using Shards..."))
        {
          ShardsSettingsDefaultObject.GetOrCreateSettings();
        }

        return;
      }

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
          request = new GetRequest(_hash, _mesh);
          _requests.Add(_hash, request);
        }
      }

      EditorGUI.EndDisabledGroup();
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
          if (result.Value.type != SHType.Table) continue;

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
