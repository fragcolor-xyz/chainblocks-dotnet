/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using System.Collections.Generic;
using System.IO;

using UnityEditor;

using UnityEngine;

namespace Fragcolor.Chainblocks.UnityEditor
{
  internal static class ChainblocksEditorUtility
  {
    public const string EditorResourcesFolder = "Editor Resources";
    public const string PackageName = "com.fragcolor.chainblocks";
    public const string PackageDisplayName = "Chainblocks";

    private static string? _packageAbsolutePath;
    private static string? _packageRelativePath;

    internal static string? PackageAbsolutePath
    {
      get
      {
        if (string.IsNullOrEmpty(_packageAbsolutePath))
          _packageAbsolutePath = GetPackagePath(true);

        return _packageAbsolutePath;
      }
    }

    internal static string? PackageRelativePath
    {
      get
      {
        if (string.IsNullOrEmpty(_packageRelativePath))
          _packageRelativePath = GetPackagePath(false);

        return _packageRelativePath;
      }
    }

    public static T? LoadEditorAssetAtPath<T>(string assetPath) where T : Object
    {
      var path = string.Join("/", PackageRelativePath, EditorResourcesFolder, assetPath);
      return AssetDatabase.LoadAssetAtPath<T>(path);
    }

    private static string? GetPackagePath(bool isAbsolute)
    {
      // Check for potential UPM package
      var packagePath = Path.GetFullPath($"Packages/{PackageName}");
      if (Directory.Exists(packagePath))
      {
        return (isAbsolute ? packagePath : "") + $"Packages/{PackageName}";
      }

      packagePath = Path.GetFullPath("Assets/..");
      if (!Directory.Exists(packagePath)) return null;

      // Search default location for development package
      if (Directory.Exists(packagePath + $"/Assets/Packages/{PackageName}/{EditorResourcesFolder}"))
      {
        return (isAbsolute ? packagePath : "") + $"Assets/Packages/{PackageName}";
      }

      // Search for default location of normal Chainblocks AssetStore package
      if (Directory.Exists(packagePath + $"/Assets/{PackageDisplayName}/{EditorResourcesFolder}"))
      {
        return $"{(isAbsolute ? packagePath : "")}/Assets/{PackageDisplayName}";
      }

      // Search for potential alternative locations in the user project
      var matchingPaths = Directory.GetDirectories(packagePath, PackageDisplayName, SearchOption.AllDirectories);
      var path = ValidateLocation(matchingPaths, packagePath);
      if (path != null) return (isAbsolute ? packagePath : "") + path;

      return null;
    }

    private static string? ValidateLocation(IEnumerable<string> paths, string projectPath)
    {
      foreach (var path in paths)
      {
        // Check if any of the matching directories contain an editor resource directory.
        if (Directory.Exists(path + $"/{EditorResourcesFolder}"))
        {
          return path.Replace(projectPath, "").TrimStart('\\', '/');
        }
      }

      return null;
    }
  }
}
