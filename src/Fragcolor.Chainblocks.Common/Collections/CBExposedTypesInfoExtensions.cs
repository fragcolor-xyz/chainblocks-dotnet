/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Collections
{
  public static class CBExposedTypesInfoExtensions
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref CBExposedTypeInfo At(this ref CBExposedTypesInfo infos, uint index)
    {
      return ref infos[index];
    }

    public static void Insert(this ref CBExposedTypesInfo infos, uint index, ref CBExposedTypeInfo info)
    {
      if (index > infos._length) throw new IndexOutOfRangeException();

      if (index == infos._length)
      {
        Push(ref infos, ref info);
        return;
      }

      var insertDelegate = Marshal.GetDelegateForFunctionPointer<ExposedTypesInfoInsertDelegate>(Native.Core._expTypesInsert);
      insertDelegate(ref infos, index, ref info);
    }

    public static CBExposedTypeInfo Pop(this ref CBExposedTypesInfo infos)
    {
      if (infos._length == 0) throw new InvalidOperationException();

      var popDelegate = Marshal.GetDelegateForFunctionPointer<ExposedTypesInfoPopDelegate>(Native.Core._expTypesPop);
      return popDelegate(ref infos);
    }

    public static void Push(this ref CBExposedTypesInfo infos, ref CBExposedTypeInfo info)
    {
      var pushDelegate = Marshal.GetDelegateForFunctionPointer<ExposedTypesInfoPushDelegate>(Native.Core._expTypesPush);
      pushDelegate(ref infos, ref info);
    }

    public static void RemoveAt(this ref CBExposedTypesInfo infos, uint index)
    {
      if (index >= infos._length) throw new IndexOutOfRangeException();

      var deleteDelegate = Marshal.GetDelegateForFunctionPointer<ExposedTypesInfoSlowDeleteDelegate>(Native.Core._expTypesSlowDelete);
      deleteDelegate(ref infos, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Size(this ref CBExposedTypesInfo infos)
    {
      return infos._length;
    }
  }

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void ExposedTypesInfoSlowDeleteDelegate(ref CBExposedTypesInfo infos, uint index);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void ExposedTypesInfoInsertDelegate(ref CBExposedTypesInfo infos, uint index, ref CBExposedTypeInfo info);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate CBExposedTypeInfo ExposedTypesInfoPopDelegate(ref CBExposedTypesInfo infos);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void ExposedTypesInfoPushDelegate(ref CBExposedTypesInfo infos, ref CBExposedTypeInfo info);
}
