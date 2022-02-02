/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBSetInterface
  {
    //! Native struct, don't edit
    internal IntPtr _setGetIterator;
    internal IntPtr _setNext;
    internal IntPtr _setSize;
    internal IntPtr _setContains;
    internal IntPtr _setInclude;
    internal IntPtr _setExclude;
    internal IntPtr _setClear;
    internal IntPtr _setFree;
  }
}
