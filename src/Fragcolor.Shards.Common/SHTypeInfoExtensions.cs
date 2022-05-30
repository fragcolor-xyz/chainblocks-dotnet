/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.CompilerServices;

namespace Fragcolor.Shards
{
  public static class SHTypeInfoExtensions
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SHType BasicType(this ref SHTypeInfo info)
    {
      return info._basicType;
    }
  }
}
