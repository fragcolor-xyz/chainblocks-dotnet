/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using System.IO;
using Fragcolor.Chainblocks.Claymore;
using Fragcolor.Chainblocks.UnityEditor.Build;
using UnityEditor;

namespace Packages.com.fragcolor.chainblocks.Editor.Build
{
  internal class BuildAndUpload : BuilderBase
  {
    public override string Name => "Build and Upload";

    internal override void Build(ref BuildPlayerOptions options)
    {
      DefaultBuilder.BuildWithOptions(ref options);

      var path = Path.GetDirectoryName(options.locationPathName)!;
      Claymore.UploadPath(path);
    }
  }
}
