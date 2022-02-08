/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Collections
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBTableIterator
  {
    //! Native struct, don't edit
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
    internal byte[] _raw;
  }
}
