/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Collections
{
  /// <summary>
  /// Extension methods for <see cref="CBlocks"/>.
  /// </summary>
  public static class CBlocksExtensions
  {
    /// <summary>
    /// Gets a reference to the <see cref="CBlock"/> at the specified index.
    /// </summary>
    /// <param name="blocks">A reference to the collection.</param>
    /// <param name="index">The element index.</param>
    /// <returns>A reference to the element at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is equal to or greater than <see cref="CBlocks.Count"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref CBlock At(this ref CBlocks blocks, uint index)
    {
      if (index >= blocks._length) throw new ArgumentOutOfRangeException(nameof(index));
      return ref blocks[index];
    }

    /// <summary>
    /// Inserts a <see cref="CBlock"/> at the specified index in the collection.
    /// </summary>
    /// <param name="blocks">A reference to the collection.</param>
    /// <param name="index">The index at which the element is inserted.</param>
    /// <param name="block">A reference to the element to insert in the collection.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is greater than <see cref="CBlocks.Count"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Insert(this ref CBlocks blocks, uint index, ref CBlock block)
    {
      Insert(ref blocks, index, block.AsPointer());
    }

    /// <summary>
    /// Inserts a <see cref="CBlockPtr"/> at the specified index in the collection.
    /// </summary>
    /// <param name="blocks">A reference to the collection.</param>
    /// <param name="index">The index at which the element is inserted.</param>
    /// <param name="blockPtr">A pointer to the element to insert in the collection.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is greater than <see cref="CBlocks.Count"/>.</exception>
    public static void Insert(this ref CBlocks blocks, uint index, CBlockPtr blockPtr)
    {
      if (index > blocks._length) throw new ArgumentOutOfRangeException(nameof(index));

      if (index == blocks._length)
      {
        Push(ref blocks, blockPtr);
        return;
      }

      var insertDelegate = Marshal.GetDelegateForFunctionPointer<BlocksInsertDelegate>(Native.Core._blocksInsert);
      insertDelegate(ref blocks, index, ref blockPtr);
    }

    /// <summary>
    /// Returns and removes the <see cref="CBlockPtr"/> at the end of the collection.
    /// </summary>
    /// <param name="blocks">A reference to the collection.</param>
    /// <returns>A pointer to the element that is removed from the end of the collection.</returns>
    /// <exception cref="InvalidOperationException">The collection is empty.</exception>
    public static CBlockPtr Pop(this ref CBlocks blocks)
    {
      if (blocks._length == 0) throw new InvalidOperationException();

      var popDelegate = Marshal.GetDelegateForFunctionPointer<BlocksPopDelegate>(Native.Core._blocksPop);
      return popDelegate(ref blocks);
    }

    /// <summary>
    /// Adds a <see cref="CBlock"/> to the end of the collection.
    /// </summary>
    /// <param name="blocks">A reference to the collection.</param>
    /// <param name="block">A reference to the element to add to the collection.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Push(this ref CBlocks blocks, ref CBlock block)
    {
      Push(ref blocks, block.AsPointer());
    }

    /// <summary>
    /// Adds a <see cref="CBlockPtr"/> to the end of the collection.
    /// </summary>
    /// <param name="blocks">A reference to the collection.</param>
    /// <param name="blockPtr">A pointer to the element to add to the collection.</param>
    public static void Push(this ref CBlocks blocks, CBlockPtr blockPtr)
    {
      var pushDelegate = Marshal.GetDelegateForFunctionPointer<BlocksPushDelegate>(Native.Core._blocksPush);
      pushDelegate(ref blocks, ref blockPtr);
    }

    /// <summary>
    /// Removes the element at the specified <paramref name="index"/> of the collection.
    /// </summary>
    /// <param name="blocks">A reference to the collection.</param>
    /// <param name="index">The index of the element to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is equal to or greater than <see cref="CBlocks.Count"/>.</exception>
    public static void RemoveAt(this ref CBlocks blocks, uint index)
    {
      if (index >= blocks._length) throw new ArgumentOutOfRangeException(nameof(index));

      var deleteDelegate = Marshal.GetDelegateForFunctionPointer<BlocksSlowDeleteDelegate>(Native.Core._blocksSlowDelete);
      deleteDelegate(ref blocks, index);
    }
  }

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void BlocksSlowDeleteDelegate(ref CBlocks blocks, uint index);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void BlocksInsertDelegate(ref CBlocks blocks, uint index, ref CBlockPtr block);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBlockPtr BlocksPopDelegate(ref CBlocks blocks);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void BlocksPushDelegate(ref CBlocks blocks, ref CBlockPtr block);
}
