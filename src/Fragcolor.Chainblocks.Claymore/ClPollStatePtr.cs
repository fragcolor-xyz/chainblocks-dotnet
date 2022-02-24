/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Claymore
{
  /// <summary>
  /// Represents a pointer to a <see cref="ClPollState"/>.
  /// </summary>
  /// <seealso cref="ClPollStateExtensions.AsRef(ClPollStatePtr)"/>
  [StructLayout(LayoutKind.Sequential)]
  public struct ClPollStatePtr
  {
    internal IntPtr _ptr;

    public bool IsValid()
    {
      return _ptr != IntPtr.Zero;
    }
  }
}
