/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Collections
{
  public static class CBParametersInfoExtensions
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref CBParameterInfo At(this ref CBParametersInfo infos, uint index)
    {
      return ref infos[index];
    }

    public static void Insert(this ref CBParametersInfo infos, uint index, ref CBParameterInfo info)
    {
      if (index > infos._length) throw new IndexOutOfRangeException();

      if (index == infos._length)
      {
        Push(ref infos, ref info);
        return;
      }

      var insertDelegate = Marshal.GetDelegateForFunctionPointer<ParamsInfoInsertDelegate>(Native.Core._paramsInsert);
      insertDelegate(ref infos, index, ref info);
    }

    public static CBParameterInfo Pop(this ref CBParametersInfo infos)
    {
      if (infos._length == 0) throw new InvalidOperationException();

      var popDelegate = Marshal.GetDelegateForFunctionPointer<ParamsInfoPopDelegate>(Native.Core._paramsPop);
      return popDelegate(ref infos);
    }

    public static void Push(this ref CBParametersInfo infos, ref CBParameterInfo info)
    {
      var pushDelegate = Marshal.GetDelegateForFunctionPointer<ParamsInfoPushDelegate>(Native.Core._paramsPush);
      pushDelegate(ref infos, ref info);
    }

    public static void RemoveAt(this ref CBParametersInfo infos, uint index)
    {
      if (index >= infos._length) throw new IndexOutOfRangeException();

      var deleteDelegate = Marshal.GetDelegateForFunctionPointer<ParamsInfoSlowDeleteDelegate>(Native.Core._paramsSlowDelete);
      deleteDelegate(ref infos, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Size(this ref CBParametersInfo infos)
    {
      return infos._length;
    }
  }

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void ParamsInfoSlowDeleteDelegate(ref CBParametersInfo infos, uint index);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void ParamsInfoInsertDelegate(ref CBParametersInfo infos, uint index, ref CBParameterInfo info);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate CBParameterInfo ParamsInfoPopDelegate(ref CBParametersInfo infos);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void ParamsInfoPushDelegate(ref CBParametersInfo infos, ref CBParameterInfo info);
}

