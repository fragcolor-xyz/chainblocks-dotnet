/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBTableInterface
  {
    //! Native struct, don't edit
    internal IntPtr _tableGetIterator;
    internal IntPtr _tableNext;
    internal IntPtr _tableSize;
    internal IntPtr _tableContains;
    internal IntPtr _tableAt;
    internal IntPtr _tableRemove;
    internal IntPtr _tableClear;
    internal IntPtr _tableFree;
  }
}
