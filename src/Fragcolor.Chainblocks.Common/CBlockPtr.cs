/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  /// <summary>
  /// Represents a pointer to a <see cref="CBlock"/>.
  /// </summary>
  /// <seealso cref="CBlockExtensions.AsPointer(ref CBlock)"/>
  /// <seealso cref="CBlockExtensions.AsRef(CBlockPtr)"/>
  [StructLayout(LayoutKind.Sequential)]
  public struct CBlockPtr
  {
    internal IntPtr _ref;

    public readonly bool IsValid()
    {
      return _ref != IntPtr.Zero;
    }
  }
}
