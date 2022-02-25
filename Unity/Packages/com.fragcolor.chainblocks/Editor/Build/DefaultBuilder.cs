/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using System.Collections.Generic;
using System.IO;
using System.Linq;

using Fragcolor.Chainblocks.UnityEditor.Settings;

using Newtonsoft.Json;

using UnityEditor;
using UnityEditor.Build.Reporting;

using UnityEngine;

namespace Fragcolor.Chainblocks.UnityEditor.Build
{
  internal sealed class DefaultBuilder : BuilderBase
  {
    internal const string DefaultBuilderName = "Default";

    public override string Name => DefaultBuilderName;

    internal override void Build(BuildPlayerOptions options)
    {
      if (Settings == null)
      {
        // Do a regular build
        BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);
        return;
      }

#if false
      // HACK: artificially mark all scenes as dirty to force a rebuild
      foreach (var scenePath in options.scenes)
      {
        var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
        EditorUtility.SetDirty(scene);
      }
      AssetDatabase.SaveAssets();
#endif
      options.options |= BuildOptions.DetailedBuildReport | BuildOptions.StrictMode;
#if UNITY_2021_2_OR_NEWER
      options.options |= BuildOptions.CleanBuildCache
#endif
      var report = BuildPipeline.BuildPlayer(options);
      var summary = report.summary;
      switch (summary.result)
      {
        case BuildResult.Succeeded:
          Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
          var foundEntries = new List<ChainblocksAssetEntry>();
          foreach (var asset in report.scenesUsingAssets.SelectMany(s => s.list))
          {
            var guid = AssetDatabase.AssetPathToGUID(asset.assetPath);
            if (Settings.TryFindAssetEntry(guid, out var entry))
            {
              foundEntries.Add(entry);
            }
          }

          foreach (var entry in foundEntries)
          {
            Debug.Log($"{entry.guid} - {entry.localPath}");
          }

          if (foundEntries.Count > 0)
          {
            var json = JsonConvert.SerializeObject(foundEntries, Formatting.Indented);
            var basePath = Path.GetDirectoryName(options.locationPathName);
            File.WriteAllText(Path.Combine(basePath!, "FragInfo.data"), json);
          }

          break;

        case BuildResult.Failed:
        case BuildResult.Cancelled:
          Debug.Log("Build failed or cancelled.");
          break;
      }
    }

    [InitializeOnLoadMethod]
    private static void Initialize()
    {
      BuildPlayerWindow.RegisterBuildPlayerHandler(BuildWithOptions);
    }

    [MenuItem("Chainblocks/Build/Default")]
    private static void BuildFromMenu()
    {
      var options = BuildPlayerWindow.DefaultBuildMethods.GetBuildPlayerOptions(default);
      BuildWithOptions(options);
    }

    [MenuItem("Chainblocks/Build/Default", true)]
    private static bool CanBuildFromMenu()
    {
      return Settings != null;
    }

    private static void BuildWithOptions(BuildPlayerOptions options)
    {
      Settings!.builders!.Find(x => x.Name == DefaultBuilderName).Build(options);
    }
  }
}
