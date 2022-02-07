/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Fragcolor.Chainblocks.Collections;

namespace Fragcolor.Chainblocks
{
  public static class CBParameterInfoExtensions
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Name(this ref CBParameterInfo info)
    {
      return Marshal.PtrToStringAnsi(info._name);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CBTypesInfo Types(this ref CBParameterInfo info)
    {
      return info._types;
    }
  }
}
