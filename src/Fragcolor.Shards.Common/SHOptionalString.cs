/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

namespace Fragcolor.Shards
{
  [StructLayout(LayoutKind.Sequential)]
  public struct SHOptionalString
  {
    //! Native struct, don't edit
    internal SHString _str;
    internal uint _crc;

    public static explicit operator string?(SHOptionalString str)
    {
      return (string?)str._str;
    }
  }
}
