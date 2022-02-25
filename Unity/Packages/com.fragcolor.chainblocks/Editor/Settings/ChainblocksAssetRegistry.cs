/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using UnityEditor;

using UnityEngine;

namespace Fragcolor.Chainblocks.UnityEditor.Settings
{
  /// <summary>
  /// A collection of asset entries.
  /// </summary>
  [Serializable]
  internal sealed class ChainblocksAssetRegistry : ScriptableObject, ISerializationCallbackReceiver
  {
    [SerializeField]
    private string _guid;

    [SerializeField]
    private string _name;

    [SerializeField]
    private List<ChainblocksAssetEntry>? _serializedEntries = new List<ChainblocksAssetEntry>();

    private readonly Dictionary<string, ChainblocksAssetEntry> _map = new Dictionary<string, ChainblocksAssetEntry>();

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
      _serializedEntries ??= new List<ChainblocksAssetEntry>(_map.Values);
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

    internal void AddAssetEntry(ChainblocksAssetEntry entry, bool postEvent = true)
    {
      _map[entry.guid] = entry;
      _serializedEntries = null;
      SetDirty(ChainblocksSettings.ModificationEvent.EntryCreated, entry, postEvent);
    }

    internal void RemoveAssetEntry(ChainblocksAssetEntry entry, bool postEvent = true)
    {
      if (!_map.Remove(entry.guid)) return;
      _serializedEntries = null;
      SetDirty(ChainblocksSettings.ModificationEvent.EntryRemoved, entry, postEvent);
    }

    internal void SetDirty(ChainblocksSettings.ModificationEvent modificationEvent, object eventData, bool postEvent)
    {
      if (ChainblocksSettingsDefaultObject.Settings != null)
      {
        if (this != null)
          EditorUtility.SetDirty(this);
        ChainblocksSettingsDefaultObject.Settings.SetDirty(modificationEvent, eventData, postEvent);
      }
    }

    internal bool TryGetAssetEntry(string guid, [NotNullWhen(true)] out ChainblocksAssetEntry? entry)
    {
      return _map.TryGetValue(guid, out entry);
    }
  }
}
