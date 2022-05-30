/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Shards.Collections
{
  /// <summary>
  /// Extension methods for <see cref="Shards"/>.
  /// </summary>
  public static class ShardsExtensions
  {
    /// <summary>
    /// Gets a reference to the <see cref="Shard"/> at the specified index.
    /// </summary>
    /// <param name="shards">A reference to the collection.</param>
    /// <param name="index">The element index.</param>
    /// <returns>A reference to the element at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is equal to or greater than <see cref="Shards.Count"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref Shard At(this ref Shards shards, uint index)
    {
      if (index >= shards._length) throw new ArgumentOutOfRangeException(nameof(index));
      return ref shards[index];
    }

    /// <summary>
    /// Inserts a <see cref="Shard"/> at the specified index in the collection.
    /// </summary>
    /// <param name="shards">A reference to the collection.</param>
    /// <param name="index">The index at which the element is inserted.</param>
    /// <param name="shard">A reference to the element to insert in the collection.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is greater than <see cref="Shards.Count"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Insert(this ref Shards shards, uint index, ref Shard shard)
    {
      Insert(ref shards, index, shard.AsPointer());
    }

    /// <summary>
    /// Inserts a <see cref="ShardPtr"/> at the specified index in the collection.
    /// </summary>
    /// <param name="shards">A reference to the collection.</param>
    /// <param name="index">The index at which the element is inserted.</param>
    /// <param name="shardPtr">A pointer to the element to insert in the collection.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is greater than <see cref="Shards.Count"/>.</exception>
    public static void Insert(this ref Shards shards, uint index, ShardPtr shardPtr)
    {
      if (index > shards._length) throw new ArgumentOutOfRangeException(nameof(index));

      if (index == shards._length)
      {
        Push(ref shards, shardPtr);
        return;
      }

      var insertDelegate = Marshal.GetDelegateForFunctionPointer<ShardsInsertDelegate>(Native.Core._shardsInsert);
      insertDelegate(ref shards, index, ref shardPtr);
    }

    /// <summary>
    /// Returns and removes the <see cref="ShardPtr"/> at the end of the collection.
    /// </summary>
    /// <param name="shards">A reference to the collection.</param>
    /// <returns>A pointer to the element that is removed from the end of the collection.</returns>
    /// <exception cref="InvalidOperationException">The collection is empty.</exception>
    public static ShardPtr Pop(this ref Shards shards)
    {
      if (shards._length == 0) throw new InvalidOperationException();

      var popDelegate = Marshal.GetDelegateForFunctionPointer<ShardsPopDelegate>(Native.Core._shardsPop);
      return popDelegate(ref shards);
    }

    /// <summary>
    /// Adds a <see cref="Shard"/> to the end of the collection.
    /// </summary>
    /// <param name="shards">A reference to the collection.</param>
    /// <param name="shard">A reference to the element to add to the collection.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Push(this ref Shards shards, ref Shard shard)
    {
      Push(ref shards, shard.AsPointer());
    }

    /// <summary>
    /// Adds a <see cref="ShardPtr"/> to the end of the collection.
    /// </summary>
    /// <param name="shards">A reference to the collection.</param>
    /// <param name="shardPtr">A pointer to the element to add to the collection.</param>
    public static void Push(this ref Shards shards, ShardPtr shardPtr)
    {
      var pushDelegate = Marshal.GetDelegateForFunctionPointer<ShardsPushDelegate>(Native.Core._shardsPush);
      pushDelegate(ref shards, ref shardPtr);
    }

    /// <summary>
    /// Removes the element at the specified <paramref name="index"/> of the collection.
    /// </summary>
    /// <param name="shards">A reference to the collection.</param>
    /// <param name="index">The index of the element to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is equal to or greater than <see cref="Shards.Count"/>.</exception>
    public static void RemoveAt(this ref Shards shards, uint index)
    {
      if (index >= shards._length) throw new ArgumentOutOfRangeException(nameof(index));

      var deleteDelegate = Marshal.GetDelegateForFunctionPointer<ShardsSlowDeleteDelegate>(Native.Core._shardsSlowDelete);
      deleteDelegate(ref shards, index);
    }
  }

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void ShardsSlowDeleteDelegate(ref Shards shards, uint index);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void ShardsInsertDelegate(ref Shards shards, uint index, ref ShardPtr shard);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate ShardPtr ShardsPopDelegate(ref Shards shards);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void ShardsPushDelegate(ref Shards shards, ref ShardPtr shard);
}
