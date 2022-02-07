/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Collections
{
  public static class CBTypesInfoExtensions
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref CBTypeInfo At(this ref CBTypesInfo infos, uint index)
    {
      return ref infos[index];
    }

    public static void Insert(this ref CBTypesInfo infos, uint index, ref CBTypeInfo info)
    {
      if (index > infos._length) throw new IndexOutOfRangeException();

      if (index == infos._length)
      {
        Push(ref infos, ref info);
        return;
      }

      var insertDelegate = Marshal.GetDelegateForFunctionPointer<TypesInfoInsertDelegate>(Native.Core._typesInsert);
      insertDelegate(ref infos, index, ref info);
    }

    public static CBTypeInfo Pop(this ref CBTypesInfo infos)
    {
      if (infos._length == 0) throw new InvalidOperationException();

      var popDelegate = Marshal.GetDelegateForFunctionPointer<TypesInfoPopDelegate>(Native.Core._typesPop);
      return popDelegate(ref infos);
    }

    public static void Push(this ref CBTypesInfo infos, ref CBTypeInfo info)
    {
      var pushDelegate = Marshal.GetDelegateForFunctionPointer<TypesInfoPushDelegate>(Native.Core._typesPush);
      pushDelegate(ref infos, ref info);
    }

    public static void RemoveAt(this ref CBTypesInfo infos, uint index)
    {
      if (index >= infos._length) throw new IndexOutOfRangeException();

      var deleteDelegate = Marshal.GetDelegateForFunctionPointer<TypesInfoSlowDeleteDelegate>(Native.Core._typesSlowDelete);
      deleteDelegate(ref infos, index);
    }
  }

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void TypesInfoSlowDeleteDelegate(ref CBTypesInfo infos, uint index);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void TypesInfoInsertDelegate(ref CBTypesInfo infos, uint index, ref CBTypeInfo info);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate CBTypeInfo TypesInfoPopDelegate(ref CBTypesInfo infos);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void TypesInfoPushDelegate(ref CBTypesInfo infos, ref CBTypeInfo info);
}
