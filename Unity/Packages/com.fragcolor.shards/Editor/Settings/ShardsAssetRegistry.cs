/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using UnityEditor;

using UnityEngine;

namespace Fragcolor.Shards.UnityEditor.Settings
{
  /// <summary>
  /// A collection of asset entries.
  /// </summary>
  [Serializable]
  internal sealed class ShardsAssetRegistry : ScriptableObject, ISerializationCallbackReceiver
  {
    [SerializeField]
    private string _guid;

    [SerializeField]
    private string _name;

    [SerializeField]
    private List<ShardsAssetEntry>? _serializedEntries = new List<ShardsAssetEntry>();

    private readonly Dictionary<string, ShardsAssetEntry> _map = new Dictionary<string, ShardsAssetEntry>();

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
      if (_serializedEntries == null)
        _serializedEntries = new List<ShardsAssetEntry>(_map.Values);
    }

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
      _map.Clear();
      foreach (var entry in _serializedEntries!)
      {
        try
        {
          _map.Add(entry.guid, entry);
        }
        catch (ArgumentException)
        {
          // TODO: deal with duplicate entry
        }
      }
    }

    internal string Guid
    {
      get
      {
        if (string.IsNullOrEmpty(_guid))
          _guid = GUID.Generate().ToString();
        return _guid;
      }
    }

    internal string Name
    {
      get { return _name; }
    }

    internal void Initialize(string name, string guid)
    {
      _guid = guid;
      _name = name;
    }

    internal void AddAssetEntry(ShardsAssetEntry entry, bool postEvent = true)
    {
      _map[entry.guid] = entry;
      _serializedEntries = null;
      SetDirty(ShardsSettings.ModificationEvent.EntryCreated, entry, postEvent);
    }

    internal void RemoveAssetEntry(ShardsAssetEntry entry, bool postEvent = true)
    {
      if (!_map.Remove(entry.guid)) return;
      _serializedEntries = null;
      SetDirty(ShardsSettings.ModificationEvent.EntryRemoved, entry, postEvent);
    }

    internal void SetDirty(ShardsSettings.ModificationEvent modificationEvent, object eventData, bool postEvent)
    {
      if (ShardsSettingsDefaultObject.Settings != null)
      {
        if (this != null)
          EditorUtility.SetDirty(this);
        ShardsSettingsDefaultObject.Settings.SetDirty(modificationEvent, eventData, postEvent);
      }
    }

    internal bool TryGetAssetEntry(string guid, [NotNullWhen(true)] out ShardsAssetEntry? entry)
    {
      return _map.TryGetValue(guid, out entry);
    }
  }
}
