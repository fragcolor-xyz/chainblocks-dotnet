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

    public static explicit operator string?(CBString str)
    {
      return Marshal.PtrToStringAnsi(str._str);
    }
  }
}
