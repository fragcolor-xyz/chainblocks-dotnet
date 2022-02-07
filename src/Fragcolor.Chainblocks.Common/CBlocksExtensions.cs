/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  public static class CBlocksExtensions
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref CBlock At(this ref CBlocks blocks, uint index)
    {
      return ref blocks[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Insert(this ref CBlocks blocks, uint index, ref CBlock block)
    {
      Insert(ref blocks, index, block.AsPointer());
    }

    public static void Insert(this ref CBlocks blocks, uint index, CBlockPtr blockPtr)
    {
      if (index > blocks._length) throw new IndexOutOfRangeException();

      if (index == blocks._length)
      {
        Push(ref blocks, blockPtr);
        return;
      }

      var insertDelegate = Marshal.GetDelegateForFunctionPointer<BlocksInsertDelegate>(Native.Core._blocksInsert);
      insertDelegate(ref blocks, index, ref blockPtr);
    }

    public static CBlockPtr Pop(this ref CBlocks blocks)
    {
      if (blocks._length == 0) throw new InvalidOperationException();

      var popDelegate = Marshal.GetDelegateForFunctionPointer<BlocksPopDelegate>(Native.Core._blocksPop);
      return popDelegate(ref blocks);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Push(this ref CBlocks blocks, ref CBlock block)
    {
      Push(ref blocks, block.AsPointer());
    }

    public static void Push(this ref CBlocks blocks, CBlockPtr blockPtr)
    {
      var pushDelegate = Marshal.GetDelegateForFunctionPointer<BlocksPushDelegate>(Native.Core._blocksPush);
      pushDelegate(ref blocks, ref blockPtr);
    }

    public static void RemoveAt(this ref CBlocks blocks, uint index)
    {
      if (index >= blocks._length) throw new IndexOutOfRangeException();

      var deleteDelegate = Marshal.GetDelegateForFunctionPointer<BlocksSlowDeleteDelegate>(Native.Core._blocksSlowDelete);
      deleteDelegate(ref blocks, index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Size(this ref CBlocks blocks)
    {
      return blocks._length;
    }
  }

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void BlocksSlowDeleteDelegate(ref CBlocks blocks, uint index);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void BlocksInsertDelegate(ref CBlocks blocks, uint index, ref CBlockPtr block);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate CBlockPtr BlocksPopDelegate(ref CBlocks blocks);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void BlocksPushDelegate(ref CBlocks blocks, ref CBlockPtr block);
}
