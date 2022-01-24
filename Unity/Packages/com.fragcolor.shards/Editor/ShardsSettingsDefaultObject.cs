/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using Fragcolor.Shards.UnityEditor.Settings;

using UnityEditor;

using UnityEngine;

namespace Fragcolor.Shards.UnityEditor
{
  /// <summary>
  /// Facilitates access to the default Shards settings object.
  /// </summary>
  internal sealed class ShardsSettingsDefaultObject : ScriptableObject
  {
    /// <summary>
    /// The default name for the settings.
    /// </summary>
    public const string DefaultConfigAssetName = "ShardsSettings";

    /// <summary>
    /// The default folder location for the serialized version of this class.
    /// </summary>
    public const string DefaultConfigFolder = "Assets/ShardsData";

    /// <summary>
    /// The name of the default config object.
    /// </summary>
    /// <seealso cref="EditorBuildSettings.TryGetConfigObject{T}(string, out T)"/>
    public const string DefaultConfigObjectName = "com.fragcolor.shards";

    /// <summary>
    /// The guid of the default settings object.
    /// </summary>
    [SerializeField]
    internal string? shardsSettingsGuid;

    private static ShardsSettings? _settings;
    private bool _loadingSettings;

    /// <summary>
    /// Indicates whether the default settings object exists and is referenced.
    /// </summary>
    internal static bool SettingsExists
    {
      get
      {
        return EditorBuildSettings.TryGetConfigObject(DefaultConfigObjectName, out ShardsSettingsDefaultObject obj)
               && !string.IsNullOrEmpty(AssetDatabase.GUIDToAssetPath(obj.shardsSettingsGuid));
      }
    }

    /// <summary>
    /// Gets or sets the default settings object.
    /// </summary>
    internal static ShardsSettings? Settings
    {
      get
      {
        if (_settings == null)
        {
          if (EditorBuildSettings.TryGetConfigObject(DefaultConfigObjectName, out ShardsSettingsDefaultObject obj))
          {
            _settings = obj.LoadSettingsObject();
          }
        }

        return _settings;
      }
      private set
      {
        _settings = value;
        if (!EditorBuildSettings.TryGetConfigObject(DefaultConfigObjectName, out ShardsSettingsDefaultObject obj))
        {
          obj = CreateInstance<ShardsSettingsDefaultObject>();
          AssetDatabase.CreateAsset(obj, DefaultConfigFolder + "/DefaultObject.asset");
          AssetDatabase.SaveAssets();
          EditorBuildSettings.AddConfigObject(DefaultConfigObjectName, obj, true);
        }

        obj.SetSettingsObject(_settings);
        EditorUtility.SetDirty(obj);
        AssetDatabase.SaveAssets();
      }
    }

    /// <summary>
    /// Gets the default settings object or creates one if missing.
    /// </summary>
    /// <returns>The default settings object.</returns>
    internal static ShardsSettings GetOrCreateSettings()
    {
      if (Settings == null)
        Settings = ShardsSettings.Create(DefaultConfigFolder, DefaultConfigAssetName);
      return _settings!;
    }

    private ShardsSettings? LoadSettingsObject()
    {
      // prevent re-entrant stack overflow
      if (_loadingSettings)
      {
        Debug.LogWarning($"Detected stack overflow when accessing {nameof(ShardsSettingsDefaultObject)}.{nameof(Settings)} object.");
        return null;
      }

      if (string.IsNullOrEmpty(shardsSettingsGuid))
      {
        Debug.LogError($"Invalid guid for default {nameof(ShardsSettings)} object.");
        return null;
      }

      var path = AssetDatabase.GUIDToAssetPath(shardsSettingsGuid);
      if (string.IsNullOrEmpty(path))
      {
        Debug.LogError($"Unable to determine path for default {nameof(ShardsSettings)} object with guid {shardsSettingsGuid}");
      }

      _loadingSettings = true;
      var settings = AssetDatabase.LoadAssetAtPath<ShardsSettings>(path);
      if (settings != null) ShardsPostProcessor.OnPostProcessAssets = settings.OnPostProcessAssets;
      _loadingSettings = false;

      return settings;
    }

    private void SetSettingsObject(ShardsSettings? settings)
    {
      if (settings == null)
      {
        shardsSettingsGuid = null;
        return;
      }

      var path = AssetDatabase.GetAssetPath(settings);
      if (string.IsNullOrEmpty(path))
      {
        Debug.LogError($"Unable to determine path for default {nameof(ShardsSettings)} object with guid {shardsSettingsGuid}.");
        return;
      }

      ShardsPostProcessor.OnPostProcessAssets = settings.OnPostProcessAssets;
      shardsSettingsGuid = AssetDatabase.AssetPathToGUID(path);
    }
  }
}
