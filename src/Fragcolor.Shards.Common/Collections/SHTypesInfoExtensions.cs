/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Shards.Collections
{
  /// <summary>
  /// Extension methods for <see cref="SHTypesInfo"/>.
  /// </summary>
  public static class SHTypesInfoExtensions
  {
    /// <summary>
    /// Gets a reference to the <see cref="SHTypeInfo"/> at the specified index.
    /// </summary>
    /// <param name="infos">A reference to the collection.</param>
    /// <param name="index">The element index.</param>
    /// <returns>A reference to the element at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is equal to or greater than <see cref="SHTypesInfo.Count"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref SHTypeInfo At(this ref SHTypesInfo infos, uint index)
    {
      if (index >= infos._length) throw new ArgumentOutOfRangeException(nameof(index));
      return ref infos[index];
    }

    /// <summary>
    /// Inserts a <see cref="SHTypeInfo"/> at the specified index in the collection.
    /// </summary>
    /// <param name="infos">A reference to the collection.</param>
    /// <param name="index">The index at which the element is inserted.</param>
    /// <param name="info">A reference to the element to insert in the collection.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is greater than <see cref="SHTypesInfo.Count"/>.</exception>
    public static void Insert(this ref SHTypesInfo infos, uint index, ref SHTypeInfo info)
    {
      if (index > infos._length) throw new ArgumentOutOfRangeException(nameof(index));

      if (index == infos._length)
      {
        Push(ref infos, ref info);
        return;
      }

      var insertDelegate = Marshal.GetDelegateForFunctionPointer<TypesInfoInsertDelegate>(Native.Core._typesInsert);
      insertDelegate(ref infos, index, ref info);
    }

    /// <summary>
    /// Returns and removes the <see cref="SHTypeInfo"/> at the end of the collection.
    /// </summary>
    /// <param name="infos">A reference to the collection.</param>
    /// <returns>The element that is removed from the end of the collection.</returns>
    /// <exception cref="InvalidOperationException">The collection is empty.</exception>
    public static SHTypeInfo Pop(this ref SHTypesInfo infos)
    {
      if (infos._length == 0) throw new InvalidOperationException();

      var popDelegate = Marshal.GetDelegateForFunctionPointer<TypesInfoPopDelegate>(Native.Core._typesPop);
      return popDelegate(ref infos);
    }

    /// <summary>
    /// Adds a <see cref="SHTypeInfo"/> to the end of the collection.
    /// </summary>
    /// <param name="infos">A reference to the collection.</param>
    /// <param name="info">A reference to the element to add to the collection.</param>
    public static void Push(this ref SHTypesInfo infos, ref SHTypeInfo info)
    {
      var pushDelegate = Marshal.GetDelegateForFunctionPointer<TypesInfoPushDelegate>(Native.Core._typesPush);
      pushDelegate(ref infos, ref info);
    }

    /// <summary>
    /// Removes the element at the specified <paramref name="index"/> of the collection.
    /// </summary>
    /// <param name="infos">A reference to the collection.</param>
    /// <param name="index">The index of the element to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is equal to or greater than <see cref="SHTypesInfo.Count"/>.</exception>
    public static void RemoveAt(this ref SHTypesInfo infos, uint index)
    {
      if (index >= infos._length) throw new ArgumentOutOfRangeException(nameof(index));

      var deleteDelegate = Marshal.GetDelegateForFunctionPointer<TypesInfoSlowDeleteDelegate>(Native.Core._typesSlowDelete);
      deleteDelegate(ref infos, index);
    }
  }

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void TypesInfoSlowDeleteDelegate(ref SHTypesInfo infos, uint index);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void TypesInfoInsertDelegate(ref SHTypesInfo infos, uint index, ref SHTypeInfo info);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate SHTypeInfo TypesInfoPopDelegate(ref SHTypesInfo infos);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void TypesInfoPushDelegate(ref SHTypesInfo infos, ref SHTypeInfo info);
}
