/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBOptionalString
  {
    //! Native struct, don't edit
    internal CBString _str;
    internal uint _crc;

    public static explicit operator string?(CBOptionalString str)
    {
      return (string?)str._str;
    }
  }
}
