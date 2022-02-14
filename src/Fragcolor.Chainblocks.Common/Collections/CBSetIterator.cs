/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Collections
{
  /// <summary>
  /// Represents an iterator inside a <see cref="CBSet"/>.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct CBSetIterator
  {
    //! Native struct, don't edit
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
    internal byte[] _raw;
  }
}
