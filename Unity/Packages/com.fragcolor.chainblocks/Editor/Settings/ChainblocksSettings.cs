/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

using UnityEditor;

using UnityEngine;

namespace Fragcolor.Chainblocks.UnityEditor.Settings
{
  /// <summary>
  /// Contains editor data for the Chainblocks integration.
  /// </summary>
  internal sealed class ChainblocksSettings : ScriptableObject
  {
    /// <summary>
    /// The list of asset registries.
    /// </summary>
    [SerializeField]
    internal List<ChainblocksAssetRegistry> registries = new List<ChainblocksAssetRegistry>();

    [SerializeField]
    internal string? defaultRegistry;

    [InitializeOnLoadMethod]
    private static void RegisterWithAssetPostProcessor()
    {
      if (ChainblocksSettingsDefaultObject.Settings != null)
        ChainblocksPostProcessor.OnPostProcessAssets = ChainblocksSettingsDefaultObject.Settings.OnPostProcessAssets;
      else
        EditorApplication.update += TryAddAssetPostProcessorOnNextUpdate;

      static void TryAddAssetPostProcessorOnNextUpdate()
      {
        if (ChainblocksSettingsDefaultObject.Settings != null)
          ChainblocksPostProcessor.OnPostProcessAssets = ChainblocksSettingsDefaultObject.Settings.OnPostProcessAssets;
        EditorApplication.update -= TryAddAssetPostProcessorOnNextUpdate;
      }
    }

    /// <summary>
    /// This event is triggered when any instance of <see cref="ChainblocksSettings"/> is modified.
    /// </summary>
    /// <seealso cref="ModificationEvent"/>
    public static event Action<ChainblocksSettings, ModificationEvent, object?>? OnModificationGlobal;

    internal enum ModificationEvent
    {
      /// <summary>
      /// Use to indicate that a registry was added to the settings object.
      /// </summary>
      RegistryAdded,

      /// <summary>
      /// Use to indicate that a registry was removed from the settings object.
      /// </summary>
      RegistryRemoved,

      /// <summary>
      /// Indicates that an asset entry was created.
      /// </summary>
      EntryCreated,

      /// <summary>
      /// Indicates that an asset entry was removed.
      /// </summary>
      EntryRemoved,

      /// <summary>
      /// Use to indicate that a batch of asset entries was modified. Note that the posted object will be null.
      /// </summary>
      BatchModification,
    }

    internal ChainblocksAssetRegistry DefaultRegistry
    {
      get
      {
        ChainblocksAssetRegistry? registry = null;
        if (!string.IsNullOrEmpty(defaultRegistry))
        {
          registry = registries.FirstOrDefault(x => x != null && x.Guid == defaultRegistry);
        }

        if (registry == null)
        {
          registry = registries.FirstOrDefault(x => x != null);
        }

        if (registry == null)
        {
          // create a default registry
          registry = CreateDefaultRegistry(this);
        }

        defaultRegistry = registry.Guid;

        return registry;
      }
      set
      {
        if (value == null || string.IsNullOrEmpty(value.Guid)) return;

        defaultRegistry = value.Guid;
      }
    }

    /// <summary>
    /// The path of the settings asset.
    /// </summary>
    internal string AssetPath
    {
      get
      {
        if (!AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out var guid, out long _))
          throw new InvalidOperationException($"{nameof(ChainblocksSettings)} is not persisted.  Unable to determine {nameof(AssetPath)}.");

        var assetPath = AssetDatabase.GUIDToAssetPath(guid);
        if (string.IsNullOrEmpty(assetPath))
          throw new Exception($"{nameof(ChainblocksSettings)} - Unable to determine {nameof(AssetPath)} from guid {guid}.");

        return assetPath;
      }
    }

    /// <summary>
    /// The folder of the settings asset.
    /// </summary>
    internal string ConfigFolder
    {
      get { return Path.GetDirectoryName(AssetPath); }
    }

    /// <summary>
    /// The folder for the registry assets.
    /// </summary>
    private string RegistryFolder
    {
      get { return ConfigFolder + "/Registries"; }
    }

    /// <summary>
    /// Creates a new instance with the given name and at the given location, if it doesn't already exist.
    /// </summary>
    /// <param name="configFolder"></param>
    /// <param name="configName"></param>
    /// <returns>The newly created settings, or the existing one.</returns>
    internal static ChainblocksSettings Create(string configFolder, string configName)
    {
      var path = configFolder + "/" + configName + ".asset";
      var settings = AssetDatabase.LoadAssetAtPath<ChainblocksSettings>(path);
      if (settings == null)
      {
        settings = CreateInstance<ChainblocksSettings>();

        Directory.CreateDirectory(configFolder);
        AssetDatabase.CreateAsset(settings, path);
        settings = AssetDatabase.LoadAssetAtPath<ChainblocksSettings>(path);
        settings.Validate();

        CreateDefaultRegistry(settings);

        AssetDatabase.SaveAssets();
      }

      return settings;
    }

    internal ChainblocksAssetEntry CreateOrMoveEntry(string guid, ChainblocksAssetRegistry parent, bool postEvent = true)
    {
      if (parent == null) throw new ArgumentNullException(nameof(parent));

      if (string.IsNullOrEmpty(guid)) throw new ArgumentException(nameof(guid));

      if (TryFindAssetEntry(guid, out var entry))
      {
        // TODO: move
      }
      else
      {
        entry = CreateAndAddEntryToRegistry(guid, parent, postEvent);
      }

      return entry;
    }

    /// <summary>
    /// Creates a new registry and adds it to the list.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="setAsDefault"></param>
    /// <param name="postEvent"></param>
    /// <returns>The newly created registry.</returns>
    internal ChainblocksAssetRegistry CreateRegistry(string name, bool setAsDefault, bool postEvent)
    {
      var registry = CreateInstance<ChainblocksAssetRegistry>();
      registry.Initialize(name, GUID.Generate().ToString());

      if (!Directory.Exists(RegistryFolder))
        Directory.CreateDirectory(RegistryFolder);
      AssetDatabase.CreateAsset(registry, $"{RegistryFolder}/{registry.Name}.asset");

      if (!registries.Contains(registry))
        registries.Add(registry);

      if (setAsDefault)
        DefaultRegistry = registry;

      SetDirty(ModificationEvent.RegistryAdded, registry, postEvent, true);

      return registry;
    }

    internal void OnPostProcessAssets((string[] imported, string[] deleted, string[] moved, string[] movedFrom) args)
    {
      var relatedAssetChanged = false;
      var settingsChanged = false;

      foreach (var path in args.imported)
      {
        var assetType = AssetDatabase.GetMainAssetTypeAtPath(path);
        if (typeof(ChainblocksSettings).IsAssignableFrom(assetType))
        {
          var settings = AssetDatabase.LoadAssetAtPath<ChainblocksSettings>(path);
          if (settings != null) settings.Validate();
        }

        if (typeof(ChainblocksAssetRegistry).IsAssignableFrom(assetType))
        {
          var name = Path.GetFileNameWithoutExtension(path);
          var registry = registries.Find(r => r != null && r.name == name);
          if (registry == null)
          {
            var foundRegistry = AssetDatabase.LoadAssetAtPath<ChainblocksAssetRegistry>(path);
            if (!registries.Contains(foundRegistry))
            {
              registries.Add(foundRegistry);
              registry = registries.Find(r => r != null && r.name == name);
              relatedAssetChanged = true;
              settingsChanged = true;
            }
          }

          if (registry != null)
          {
            // TODO: detect and remove duplicates entries existing in other registries
          }
        }

        var guid = AssetDatabase.AssetPathToGUID(path);
        if (TryFindAssetEntry(guid, out _))
        {
          relatedAssetChanged = true;
          // TODO: fixup path here?
        }

        if (ChainblocksUtility.IsInResources(path))
          relatedAssetChanged = true;
      }

      if (args.deleted.Length > 0)
      {
        // note: Unity has special meaning for null values of Object, so the following code is valid!
        if (registries.Remove(null!))
          relatedAssetChanged = true;
      }

      foreach (var path in args.deleted)
      {
        if (ChainblocksUtility.IsInResources(path))
        {
          relatedAssetChanged = true;
          continue;
        }

        if (CheckForRegistryDeletion(path))
        {
          relatedAssetChanged = true;
          settingsChanged = true;
          continue;
        }

        var deletedGuid = AssetDatabase.AssetPathToGUID(path);
        if (RemoveAssetEntry(deletedGuid, true))
        {
          relatedAssetChanged = true;
        }
      }

      for (var i = 0; i < args.moved.Length; i++)
      {
        var newPath = args.moved[i];
        var oldPath = args.movedFrom[i];
        var assetType = AssetDatabase.GetMainAssetTypeAtPath(newPath);

        if (typeof(ChainblocksAssetRegistry).IsAssignableFrom(assetType))
        {
          var oldName = Path.GetFileNameWithoutExtension(oldPath);
          var registry = registries.Find(r => r != null && r.name == oldName);
          if (registry != null)
          {
            registry.name = Path.GetFileNameWithoutExtension(newPath);
            relatedAssetChanged = true;
          }

          continue;
        }

        var guid = AssetDatabase.AssetPathToGUID(newPath);
        var alreadyHasEntry = TryFindAssetEntry(guid, out var entry);
        var wasInResources = ChainblocksUtility.IsInResources(oldPath);
        var isInResources = ChainblocksUtility.IsInResources(newPath);

        // TODO: deal with the resource case

        // update entry path
        if (entry != null) entry.localPath = newPath;

        relatedAssetChanged = alreadyHasEntry || wasInResources || isInResources;
      }

      if (relatedAssetChanged || settingsChanged)
        SetDirty(ModificationEvent.BatchModification, null, true, settingsChanged);

      bool CheckForRegistryDeletion(string path)
      {
        if (string.IsNullOrEmpty(path)) return false;

        var deleteGroup = false;
        ChainblocksAssetRegistry? toDelete = null;
        foreach (var registry in registries)
        {
          if (registry == null) continue;

          if (AssetDatabase.GUIDToAssetPath(registry.Guid) == path)
          {
            deleteGroup = true;
            toDelete = registry;
            break;
          }
        }

        if (deleteGroup)
        {
          RemoveRegistry(toDelete!, true);
          return true;
        }

        return false;
      }

      bool RemoveAssetEntry(string guid, bool postEvent)
      {
        if (TryFindAssetEntry(guid, out var entry))
        {
          if (entry.Parent != null)
            entry.Parent.RemoveAssetEntry(entry, postEvent);

          return true;
        }

        return false;
      }

      void RemoveRegistry(ChainblocksAssetRegistry registry, bool postEvent)
      {
        registries.Remove(registry);
        SetDirty(ModificationEvent.RegistryRemoved, registry, postEvent, true);
      }
    }

    /// <summary>
    /// Marks this object as modified.
    /// </summary>
    /// <param name="modificationEvent"></param>
    /// <param name="eventData"></param>
    /// <param name="postEvent"></param>
    /// <param name="settingsModified"></param>
    internal void SetDirty(ModificationEvent modificationEvent, object? eventData, bool postEvent, bool settingsModified = false)
    {
      if (this == null) return;

      if (postEvent)
        OnModificationGlobal?.Invoke(this, modificationEvent, eventData);

      if (settingsModified)
        EditorUtility.SetDirty(this);
    }

    /// <summary>
    /// Attemps to get an asset entry by <paramref name="guid"/> in any of the registries.
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="entry"></param>
    /// <returns><c>true</c> if the entry was found; otherwise, <c>false</c>.</returns>
    internal bool TryFindAssetEntry(string guid, [NotNullWhen(true)] out ChainblocksAssetEntry? entry)
    {
      foreach (var registry in registries)
      {
        if (registry != null && registry.TryGetAssetEntry(guid, out entry))
          return true;
      }

      entry = null;
      return false;
    }

    private static ChainblocksAssetRegistry CreateDefaultRegistry(ChainblocksSettings settings)
    {
      return settings.CreateRegistry("Default Registry", setAsDefault: true, postEvent: false);
    }

    private static ChainblocksAssetEntry CreateAndAddEntryToRegistry(string guid, ChainblocksAssetRegistry parent, bool postEvent)
    {
      var entry = new ChainblocksAssetEntry(guid, parent);
      parent.AddAssetEntry(entry, postEvent);
      return entry;
    }

    /// <summary>
    /// Ensures the state of this object is consistent.
    /// </summary>
    private void Validate()
    {
      // TODO: sanity checks
    }
  }
}
