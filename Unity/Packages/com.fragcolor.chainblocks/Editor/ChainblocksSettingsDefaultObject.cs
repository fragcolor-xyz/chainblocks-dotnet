/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using Fragcolor.Chainblocks.UnityEditor.Settings;

using UnityEditor;

using UnityEngine;

namespace Fragcolor.Chainblocks.UnityEditor
{
  /// <summary>
  /// Facilitates access to the default Chainblocks settings object.
  /// </summary>
  internal sealed class ChainblocksSettingsDefaultObject : ScriptableObject
  {
    /// <summary>
    /// The default name for the settings.
    /// </summary>
    public const string DefaultConfigAssetName = "ChainblocksSettings";

    /// <summary>
    /// The default folder location for the serialized version of this class.
    /// </summary>
    public const string DefaultConfigFolder = "Assets/ChainblocksData";

    /// <summary>
    /// The name of the default config object.
    /// </summary>
    /// <seealso cref="EditorBuildSettings.TryGetConfigObject{T}(string, out T)"/>
    public const string DefaultConfigObjectName = "com.fragcolor.chainblocks";

    /// <summary>
    /// The guid of the default settings object.
    /// </summary>
    [SerializeField]
    internal string? chainblocksSettingsGuid;

    private static ChainblocksSettings? _settings;
    private bool _loadingSettings;

    /// <summary>
    /// Indicates whether the default settings object exists and is referenced.
    /// </summary>
    internal static bool SettingsExists
    {
      get
      {
        return EditorBuildSettings.TryGetConfigObject(DefaultConfigObjectName, out ChainblocksSettingsDefaultObject obj)
               && !string.IsNullOrEmpty(AssetDatabase.GUIDToAssetPath(obj.chainblocksSettingsGuid));
      }
    }

    /// <summary>
    /// Gets or sets the default settings object.
    /// </summary>
    internal static ChainblocksSettings? Settings
    {
      get
      {
        if (_settings == null)
        {
          if (EditorBuildSettings.TryGetConfigObject(DefaultConfigObjectName, out ChainblocksSettingsDefaultObject obj))
          {
            _settings = obj.LoadSettingsObject();
          }
        }

        return _settings;
      }
      private set
      {
        _settings = value;
        if (!EditorBuildSettings.TryGetConfigObject(DefaultConfigObjectName, out ChainblocksSettingsDefaultObject obj))
        {
          obj = CreateInstance<ChainblocksSettingsDefaultObject>();
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
    internal static ChainblocksSettings GetOrCreateSettings()
    {
      if (Settings == null)
        Settings = ChainblocksSettings.Create(DefaultConfigFolder, DefaultConfigAssetName);
      return _settings!;
    }

    private ChainblocksSettings? LoadSettingsObject()
    {
      // prevent re-entrant stack overflow
      if (_loadingSettings)
      {
        Debug.LogWarning($"Detected stack overflow when accessing {nameof(ChainblocksSettingsDefaultObject)}.{nameof(Settings)} object.");
        return null;
      }

      if (string.IsNullOrEmpty(chainblocksSettingsGuid))
      {
        Debug.LogError($"Invalid guid for default {nameof(ChainblocksSettings)} object.");
        return null;
      }

      var path = AssetDatabase.GUIDToAssetPath(chainblocksSettingsGuid);
      if (string.IsNullOrEmpty(path))
      {
        Debug.LogError($"Unable to determine path for default {nameof(ChainblocksSettings)} object with guid {chainblocksSettingsGuid}");
      }

      _loadingSettings = true;
      var settings = AssetDatabase.LoadAssetAtPath<ChainblocksSettings>(path);
      if (settings != null) ChainblocksPostProcessor.OnPostProcessAssets = settings.OnPostProcessAssets;
      _loadingSettings = false;

      return settings;
    }

    private void SetSettingsObject(ChainblocksSettings? settings)
    {
      if (settings == null)
      {
        chainblocksSettingsGuid = null;
        return;
      }

      var path = AssetDatabase.GetAssetPath(settings);
      if (string.IsNullOrEmpty(path))
      {
        Debug.LogError($"Unable to determine path for default {nameof(ChainblocksSettings)} object with guid {chainblocksSettingsGuid}.");
        return;
      }

      ChainblocksPostProcessor.OnPostProcessAssets = settings.OnPostProcessAssets;
      chainblocksSettingsGuid = AssetDatabase.AssetPathToGUID(path);
    }
  }
}
