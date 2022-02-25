/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using System;

using UnityEngine;

namespace Fragcolor.Chainblocks.UnityEditor.Settings
{
  /// <summary>
  /// Contains data for a Chainblocks asset.
  /// </summary>
  [Serializable]
  internal sealed class ChainblocksAssetEntry
  {
    [SerializeField]
    public string guid;

    [SerializeField]
    public string? hash;

    [SerializeField]
    public string? localPath;

    //[SerializeField]
    //internal string ipfsUrl;

    //[SerializeField]
    //internal long sizeInBytes;

    internal ChainblocksAssetEntry(string guid, ChainblocksAssetRegistry parent)
    {
      this.guid = guid;
      Parent = parent;
    }

    internal ChainblocksAssetRegistry? Parent { get; set; }
  }
}
