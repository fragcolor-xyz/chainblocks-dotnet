/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using Fragcolor.Chainblocks.UnityEditor.Settings;

using UnityEditor;

using UnityEngine;

namespace Fragcolor.Chainblocks.UnityEditor.Build
{
  internal abstract class BuilderBase : ScriptableObject, IBuilder
  {
    public abstract string Name { get; }

    protected static ChainblocksSettings? Settings
    {
      get { return ChainblocksSettingsDefaultObject.Settings; }
    }

    internal abstract void Build(ref BuildPlayerOptions options);
  }
}
