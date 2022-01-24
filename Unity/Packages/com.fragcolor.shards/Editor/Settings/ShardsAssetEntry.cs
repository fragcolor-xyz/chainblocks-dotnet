/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using System;

using UnityEngine;

namespace Fragcolor.Shards.UnityEditor.Settings
{
  /// <summary>
  /// Contains data for a Shards asset.
  /// </summary>
  [Serializable]
  internal sealed class ShardsAssetEntry
  {
    [SerializeField]
    internal string guid;

    [SerializeField]
    internal string? hash;

    [SerializeField]
    internal string? localPath;

    //[SerializeField]
    //internal string ipfsUrl;

    //[SerializeField]
    //internal long sizeInBytes;

    internal ShardsAssetEntry(string guid, ShardsAssetRegistry parent)
    {
      this.guid = guid;
      Parent = parent;
    }

    internal ShardsAssetRegistry? Parent { get; set; }
  }
}
