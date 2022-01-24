/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using System;

namespace Fragcolor.Chainblocks.UnityEditor.Settings
{
  internal static class ChainblocksUtility
  {
    internal static bool IsInResources(string path)
    {
#if UNITY_2022_1_OR_NEWER
      return path.Replace('\\', '/').Contains("/resources/", StringComparison.OrdinalIgnoreCase);
#else
      return path.Replace('\\', '/').IndexOf("/resources/", StringComparison.OrdinalIgnoreCase) >= 0;
#endif
    }
  }
}
