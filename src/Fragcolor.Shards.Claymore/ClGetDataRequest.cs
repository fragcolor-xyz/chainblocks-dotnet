/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

namespace Fragcolor.Shards.Claymore
{
  [StructLayout(LayoutKind.Sequential)]
  public struct ClGetDataRequest
  {
    internal SHVar _wire;
    internal SHVar _hash;
    internal SHVar _result;
  }
}
