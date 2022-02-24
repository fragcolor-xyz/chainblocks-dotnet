/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Claymore
{
  [StructLayout(LayoutKind.Sequential)]
  public struct ClGetDataRequest
  {
    internal CBVar _chain;
    internal CBVar _hash;
    internal CBVar _result;
  }
}
