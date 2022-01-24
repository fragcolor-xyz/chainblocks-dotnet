/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using System;
using System.Collections.Generic;

using UnityEditor;

namespace Fragcolor.Chainblocks.UnityEditor.Settings
{
  /// <summary>
  /// RUns after any asset has been imported.
  /// </summary>
  internal sealed class ChainblocksPostProcessor : AssetPostprocessor
  {
    private static Action<(string[], string[], string[], string[])>? _handler;
    private static List<ImportSet>? _importBuffer;

    public static Action<(string[] imported, string[] deleted, string[] moved, string[] movedFrom)> OnPostProcessAssets
    {
      set
      {
        _handler = value;
        if (value != null && _importBuffer != null)
        {
          _importBuffer.ForEach(set => value((set.imported, set.deleted, set.moved, set.movedFrom)));
          _importBuffer = null;
        }
      }
    }

    /// <summary>
    /// This is called after importing of any number of assets is complete (when the Assets progress bar has reached the end).
    /// </summary>
    /// <remarks>See https://docs.unity3d.com/ScriptReference/AssetPostprocessor.OnPostprocessAllAssets.html</remarks>
#pragma warning disable RCS1213 // Remove unused member declaration.
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
#pragma warning restore RCS1213 // Remove unused member declaration.
    {
      if (_handler != null)
      {
        _handler((importedAssets, deletedAssets, movedAssets, movedFromAssetPaths));
      }
      else
      {
        if (!ChainblocksSettingsDefaultObject.SettingsExists) return;

        (_importBuffer ??= new List<ImportSet>()).Add(new ImportSet
        {
          imported = importedAssets,
          deleted = deletedAssets,
          moved = movedAssets,
          movedFrom = movedFromAssetPaths
        });
      }
    }

    private struct ImportSet
    {
      public string[] imported;
      public string[] deleted;
      public string[] moved;
      public string[] movedFrom;
    }
  }
}
