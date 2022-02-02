/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  public static class CBSeqExtensions
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref CBVar At(this ref CBSeq seq, uint index)
    {
      return ref seq[index];
    }

    public static void Insert(this ref CBSeq seq, uint index, ref CBVar var)
    {
      if (index > seq._length) throw new IndexOutOfRangeException();

      if (index == seq._length)
      {
        Push(ref seq, ref var);
        return;
      }

      unsafe
      {
        var insertDelegate = Marshal.GetDelegateForFunctionPointer<SeqInsertDelegate>(Native.Core._seqInsert);
        insertDelegate(ref seq, index, ref var);
      }
    }

    public static CBVar Pop(this ref CBSeq seq)
    {
      if (seq._length == 0) throw new InvalidOperationException();

      unsafe
      {
        var popDelegate = Marshal.GetDelegateForFunctionPointer<SeqPopDelegate>(Native.Core._seqPop);
        return popDelegate(ref seq);
      }
    }

    public static void Push(this ref CBSeq seq, ref CBVar var)
    {
      unsafe
      {
        var pushDelegate = Marshal.GetDelegateForFunctionPointer<SeqPushDelegate>(Native.Core._seqPush);
        pushDelegate(ref seq, ref var);
      }
    }

    public static void RemoveAt(this ref CBSeq seq, uint index)
    {
      if (index >= seq._length) throw new IndexOutOfRangeException();

      unsafe
      {
        var deleteDelegate = Marshal.GetDelegateForFunctionPointer<SeqSlowDeleteDelegate>(Native.Core._seqSlowDelete);
        deleteDelegate(ref seq, index);
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Size(this ref CBSeq seq)
    {
      return seq._length;
    }
  }

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void SeqSlowDeleteDelegate(ref CBSeq seq, uint index);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void SeqInsertDelegate(ref CBSeq seq, uint index, ref CBVar var);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate CBVar SeqPopDelegate(ref CBSeq seq);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void SeqPushDelegate(ref CBSeq seq, ref CBVar var);
}
