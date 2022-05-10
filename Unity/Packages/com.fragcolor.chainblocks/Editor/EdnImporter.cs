/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using System.IO;
using UnityEditor.AssetImporters;

using UnityEngine;

namespace Packages.com.fragcolor.chainblocks.Editor
{
  [ScriptedImporter(1, "edn")]
  internal sealed class EdnImporter : ScriptedImporter
  {
    public override void OnImportAsset(AssetImportContext ctx)
    {
      var textAsset = new TextAsset(File.ReadAllText(ctx.assetPath));
      ctx.AddObjectToAsset("text", textAsset);
      ctx.SetMainObject(textAsset);
    }
  }
}
