/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Collections
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBArray
  {
    //! Native struct, don't edit
    internal IntPtr _str;
  }
}
