/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Collections
{
  public static class CBStringsExtensions
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? At(this ref CBStrings strings, uint index)
    {
      return strings[index];
    }

    public static string? Pop(this ref CBStrings strings)
    {
      if (strings._length == 0) throw new InvalidOperationException();

      var popDelegate = Marshal.GetDelegateForFunctionPointer<StringsPopDelegate>(Native.Core._stringsPop);
      return (string?)popDelegate(ref strings);
    }

    public static void RemoveAt(this ref CBStrings strings, uint index)
    {
      if (index >= strings._length) throw new IndexOutOfRangeException();

      var deleteDelegate = Marshal.GetDelegateForFunctionPointer<StringsSlowDeleteDelegate>(Native.Core._stringsSlowDelete);
      deleteDelegate(ref strings, index);
    }
  }

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void StringsSlowDeleteDelegate(ref CBStrings strings, uint index);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate CBString StringsPopDelegate(ref CBStrings strings);
}
