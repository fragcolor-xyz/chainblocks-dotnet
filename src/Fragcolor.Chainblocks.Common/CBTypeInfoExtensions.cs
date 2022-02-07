/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.CompilerServices;

namespace Fragcolor.Chainblocks
{
  public static class CBTypeInfoExtensions
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CBType BasicType(this ref CBTypeInfo info)
    {
      return info._basicType;
    }
  }
}
