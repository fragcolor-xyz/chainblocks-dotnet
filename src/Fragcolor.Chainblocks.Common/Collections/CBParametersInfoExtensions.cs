/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Collections
{
  /// <summary>
  /// Extension methods for <see cref="CBParameterInfo"/>.
  /// </summary>
  public static class CBParametersInfoExtensions
  {
    /// <summary>
    /// Gets a reference to the <see cref="CBParameterInfo"/> at the specified index.
    /// </summary>
    /// <param name="infos">A reference to the collection.</param>
    /// <param name="index">The element index.</param>
    /// <returns>A reference to the element at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is equal to or greater than <see cref="CBParametersInfo.Count"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref CBParameterInfo At(this ref CBParametersInfo infos, uint index)
    {
      if (index >= infos._length) throw new ArgumentOutOfRangeException(nameof(index));
      return ref infos[index];
    }

    /// <summary>
    /// Inserts a <see cref="CBParameterInfo"/> at the specified index in the collection.
    /// </summary>
    /// <param name="infos">A reference to the collection.</param>
    /// <param name="index">The index at which the element is inserted.</param>
    /// <param name="info">A reference to the element to insert in the collection.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is greater than <see cref="CBParametersInfo.Count"/>.</exception>
    public static void Insert(this ref CBParametersInfo infos, uint index, ref CBParameterInfo info)
    {
      if (index > infos._length) throw new ArgumentOutOfRangeException(nameof(index));

      if (index == infos._length)
      {
        Push(ref infos, ref info);
        return;
      }

      var insertDelegate = Marshal.GetDelegateForFunctionPointer<ParamsInfoInsertDelegate>(Native.Core._paramsInsert);
      insertDelegate(ref infos, index, ref info);
    }

    /// <summary>
    /// Returns and removes the <see cref="CBParameterInfo"/> at the end of the collection.
    /// </summary>
    /// <param name="infos">A reference to the collection.</param>
    /// <returns>The element that is removed from the end of the collection.</returns>
    /// <exception cref="InvalidOperationException">The collection is empty.</exception>
    public static CBParameterInfo Pop(this ref CBParametersInfo infos)
    {
      if (infos._length == 0) throw new InvalidOperationException();

      var popDelegate = Marshal.GetDelegateForFunctionPointer<ParamsInfoPopDelegate>(Native.Core._paramsPop);
      return popDelegate(ref infos);
    }

    /// <summary>
    /// Adds a <see cref="CBParameterInfo"/> to the end of the collection.
    /// </summary>
    /// <param name="infos">A reference to the collection.</param>
    /// <param name="info">A reference to the element to add to the collection.</param>
    public static void Push(this ref CBParametersInfo infos, ref CBParameterInfo info)
    {
      var pushDelegate = Marshal.GetDelegateForFunctionPointer<ParamsInfoPushDelegate>(Native.Core._paramsPush);
      pushDelegate(ref infos, ref info);
    }

    /// <summary>
    /// Removes the element at the specified <paramref name="index"/> of the collection.
    /// </summary>
    /// <param name="infos">A reference to the collection.</param>
    /// <param name="index">The index of the element to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is equal to or greater than <see cref="CBParametersInfo.Count"/>.</exception>
    public static void RemoveAt(this ref CBParametersInfo infos, uint index)
    {
      if (index >= infos._length) throw new ArgumentOutOfRangeException(nameof(index));

      var deleteDelegate = Marshal.GetDelegateForFunctionPointer<ParamsInfoSlowDeleteDelegate>(Native.Core._paramsSlowDelete);
      deleteDelegate(ref infos, index);
    }
  }

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void ParamsInfoSlowDeleteDelegate(ref CBParametersInfo infos, uint index);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void ParamsInfoInsertDelegate(ref CBParametersInfo infos, uint index, ref CBParameterInfo info);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBParameterInfo ParamsInfoPopDelegate(ref CBParametersInfo infos);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void ParamsInfoPushDelegate(ref CBParametersInfo infos, ref CBParameterInfo info);
}
