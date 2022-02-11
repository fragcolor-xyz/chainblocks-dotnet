/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Collections
{
  /// <summary>
  /// Extension methods for <see cref="CBSeq"/>.
  /// </summary>
  public static class CBSeqExtensions
  {
    /// <summary>
    /// Gets a reference to the <see cref="CBVar"/> at the specified index.
    /// </summary>
    /// <param name="seq">A reference to the sequence.</param>
    /// <param name="index">The element index.</param>
    /// <returns>A reference to the element at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is equal to or greater than <see cref="CBSeq.Count"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref CBVar At(this ref CBSeq seq, uint index)
    {
      if (index >= seq._length) throw new ArgumentOutOfRangeException(nameof(index));
      return ref seq[index];
    }

    /// <summary>
    /// Inserts a <see cref="CBVar"/> at the specified index in the sequence.
    /// </summary>
    /// <param name="seq">A reference to the sequence.</param>
    /// <param name="index">The index at which the element is inserted.</param>
    /// <param name="var">A reference to the element to insert in the sequence.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is greater than <see cref="CBSeq.Count"/>.</exception>
    public static void Insert(this ref CBSeq seq, uint index, ref CBVar var)
    {
      if (index > seq._length) throw new ArgumentOutOfRangeException(nameof(index));

      if (index == seq._length)
      {
        Push(ref seq, ref var);
        return;
      }

      var insertDelegate = Marshal.GetDelegateForFunctionPointer<SeqInsertDelegate>(Native.Core._seqInsert);
      insertDelegate(ref seq, index, ref var);
    }

    /// <summary>
    /// Returns and removes the <see cref="CBVar"/> at the end of the sequence.
    /// </summary>
    /// <param name="seq">A reference to the sequence.</param>
    /// <returns>The element that is removed from the end of the sequence.</returns>
    /// <exception cref="InvalidOperationException">The sequence is empty.</exception>
    public static CBVar Pop(this ref CBSeq seq)
    {
      if (seq._length == 0) throw new InvalidOperationException();

      var popDelegate = Marshal.GetDelegateForFunctionPointer<SeqPopDelegate>(Native.Core._seqPop);
      return popDelegate(ref seq);
    }

    /// <summary>
    /// Adds a <see cref="CBVar"/> to the end of the sequence.
    /// </summary>
    /// <param name="seq">A reference to the sequence.</param>
    /// <param name="var">A reference to the element to add to the sequence.</param>
    public static void Push(this ref CBSeq seq, ref CBVar var)
    {
      var pushDelegate = Marshal.GetDelegateForFunctionPointer<SeqPushDelegate>(Native.Core._seqPush);
      pushDelegate(ref seq, ref var);
    }

    /// <summary>
    /// Removes the element at the specified <paramref name="index"/> of the sequence.
    /// </summary>
    /// <param name="seq">A reference to the sequence.</param>
    /// <param name="index">The index of the element to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is equal to or greater than <see cref="CBSeq.Count"/>.</exception>
    public static void RemoveAt(this ref CBSeq seq, uint index)
    {
      if (index >= seq._length) throw new ArgumentOutOfRangeException(nameof(index));

      var deleteDelegate = Marshal.GetDelegateForFunctionPointer<SeqSlowDeleteDelegate>(Native.Core._seqSlowDelete);
      deleteDelegate(ref seq, index);
    }
  }

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void SeqSlowDeleteDelegate(ref CBSeq seq, uint index);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void SeqInsertDelegate(ref CBSeq seq, uint index, ref CBVar var);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBVar SeqPopDelegate(ref CBSeq seq);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void SeqPushDelegate(ref CBSeq seq, ref CBVar var);
}
