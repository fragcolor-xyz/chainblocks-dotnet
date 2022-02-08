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

    public static void Insert(this ref CBStrings strings, uint index, string @string)
    {
      if (index > strings._length) throw new IndexOutOfRangeException();

      if (index == strings._length)
      {
        Push(ref strings, @string);
        return;
      }

      var insertDelegate = Marshal.GetDelegateForFunctionPointer<StringsInsertDelegate>(Native.Core._stringsInsert);
      var cbstr = (CBString)@string;
      insertDelegate(ref strings, index, ref cbstr);
    }

    public static string? Pop(this ref CBStrings strings)
    {
      if (strings._length == 0) throw new InvalidOperationException();

      var popDelegate = Marshal.GetDelegateForFunctionPointer<StringsPopDelegate>(Native.Core._stringsPop);
      return (string?)popDelegate(ref strings);
    }

    public static void Push(this ref CBStrings strings, string @string)
    {
      var pushDelegate = Marshal.GetDelegateForFunctionPointer<StringsPushDelegate>(Native.Core._stringsPush);
      var cbstr = (CBString)@string;
      pushDelegate(ref strings, ref cbstr);
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
  internal delegate void StringsInsertDelegate(ref CBStrings strings, uint index, ref CBString str);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate CBString StringsPopDelegate(ref CBStrings strings);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void StringsPushDelegate(ref CBStrings strings, ref CBString str);
}
