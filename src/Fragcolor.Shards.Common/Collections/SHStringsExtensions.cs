/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Shards.Collections
{
  /// <summary>
  /// Extension methods for <see cref="SHStrings"/>.
  /// </summary>
  public static class SHStringsExtensions
  {
    /// <summary>
    /// Gets the element at the specified index.
    /// </summary>
    /// <param name="strings">A reference to the collection.</param>
    /// <param name="index">The element index.</param>
    /// <returns>A string representation of the element at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is equal to or greater than <see cref="SHStrings.Count"/>.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? At(this ref SHStrings strings, uint index)
    {
      if (index >= strings._length) throw new ArgumentOutOfRangeException(nameof(index));
      return strings[index];
    }

    /// <summary>
    /// Inserts a <see cref="string"/> at the specified index in the collection.
    /// </summary>
    /// <param name="strings">A reference to the collection.</param>
    /// <param name="index">The index at which the element is inserted.</param>
    /// <param name="string">A string representation of the element to insert in the collection.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is greater than <see cref="SHStrings.Count"/>.</exception>
    public static void Insert(this ref SHStrings strings, uint index, string @string)
    {
      if (index > strings._length) throw new ArgumentOutOfRangeException(nameof(index));

      if (index == strings._length)
      {
        Push(ref strings, @string);
        return;
      }

      var insertDelegate = Marshal.GetDelegateForFunctionPointer<StringsInsertDelegate>(Native.Core._stringsInsert);
      var cbstr = (SHString)@string;
      insertDelegate(ref strings, index, ref cbstr);
    }

    /// <summary>
    /// Returns and removes the <see cref="string"/> at the end of the collection.
    /// </summary>
    /// <param name="strings">A reference to the collection.</param>
    /// <returns>The element that is removed from the end of the collection.</returns>
    /// <exception cref="InvalidOperationException">The collection is empty.</exception>
    public static string? Pop(this ref SHStrings strings)
    {
      if (strings._length == 0) throw new InvalidOperationException();

      var popDelegate = Marshal.GetDelegateForFunctionPointer<StringsPopDelegate>(Native.Core._stringsPop);
      return (string?)popDelegate(ref strings);
    }

    /// <summary>
    /// Adds a <see cref="string"/> to the end of the collection.
    /// </summary>
    /// <param name="strings">A reference to the collection.</param>
    /// <param name="string">A string representation of the element to add to the collection.</param>
    public static void Push(this ref SHStrings strings, string @string)
    {
      var pushDelegate = Marshal.GetDelegateForFunctionPointer<StringsPushDelegate>(Native.Core._stringsPush);
      var cbstr = (SHString)@string;
      pushDelegate(ref strings, ref cbstr);
    }

    /// <summary>
    /// Removes the element at the specified <paramref name="index"/> of the collection.
    /// </summary>
    /// <param name="strings">A reference to the collection.</param>
    /// <param name="index">The index of the element to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is equal to or greater than <see cref="SHStrings.Count"/>.</exception>
    public static void RemoveAt(this ref SHStrings strings, uint index)
    {
      if (index >= strings._length) throw new ArgumentOutOfRangeException(nameof(index));

      var deleteDelegate = Marshal.GetDelegateForFunctionPointer<StringsSlowDeleteDelegate>(Native.Core._stringsSlowDelete);
      deleteDelegate(ref strings, index);
    }
  }

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void StringsSlowDeleteDelegate(ref SHStrings strings, uint index);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void StringsInsertDelegate(ref SHStrings strings, uint index, ref SHString str);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate SHString StringsPopDelegate(ref SHStrings strings);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void StringsPushDelegate(ref SHStrings strings, ref SHString str);
}
