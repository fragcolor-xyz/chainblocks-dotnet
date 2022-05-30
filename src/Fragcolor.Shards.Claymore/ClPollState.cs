/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

namespace Fragcolor.Shards.Claymore
{
  /// <summary>
  /// Represents the state of a data request.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct ClPollState
  {
    internal ClPollStateTag _tag;
    internal SHVar _state;
  }
}
