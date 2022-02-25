/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using Fragcolor.Shards.UnityEditor.Settings;

using UnityEditor;

using UnityEngine;

namespace Fragcolor.Shards.UnityEditor.Build
{
  internal abstract class BuilderBase : ScriptableObject, IBuilder
  {
    public abstract string Name { get; }

    protected static ShardsSettings? Settings
    {
      get { return ShardsSettingsDefaultObject.Settings; }
    }

    internal abstract void Build(BuildPlayerOptions options);
  }
}
