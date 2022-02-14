/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Claymore
{
  /// <summary>
  /// Represents the state of a data request.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct PollState
  {
    internal PollStateTag _tag;
    internal CBVar _state;
  }
}
