/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBString
  {
    //! Native struct, don't edit
    internal IntPtr _str;
    internal uint _length;
    internal uint _capacity;
  }
}
