/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Shards.Collections
{
  /// <summary>
  /// Extension methods for <see cref="SHSet"/>.
  /// </summary>
  public static class SHSetExtensions
  {
    /// <summary>
    /// Clears the set, removing all its elements.
    /// </summary>
    /// <param name="set">A reference to the set.</param>
    public static void Clear(this ref SHSet set)
    {
      Debug.Assert(set.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<SHSetInterface>(set._api.ToPointer());
        var clearDelegate = Marshal.GetDelegateForFunctionPointer<SetClearDelegate>(api._setClear);
        clearDelegate(set);
      }
    }

    /// <summary>
    /// Determines whether the set contains the specified <paramref name="value"/>.
    /// </summary>
    /// <param name="set">A reference to the set.</param>
    /// <param name="value">The value to locate in the set.</param>
    /// <returns></returns>
    public static bool Contains(this ref SHSet set, ref SHVar value)
    {
      Debug.Assert(set.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<SHSetInterface>(set._api.ToPointer());
        var containsDelegate = Marshal.GetDelegateForFunctionPointer<SetContainsDelegate>(api._setContains);
        return containsDelegate(set, value);
      }
    }

    /// <summary>
    /// Excludes the <paramref name="value"/> from the set.
    /// </summary>
    /// <param name="set">A reference to the set.</param>
    /// <param name="value">The value to exclude from the set.</param>
    /// <returns><c>true</c> if the value was found before excluding it; otherwise, <c>false</c>.</returns>
    public static bool Exclude(this ref SHSet set, ref SHVar value)
    {
      Debug.Assert(set.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<SHSetInterface>(set._api.ToPointer());
        var exludeDelegate = Marshal.GetDelegateForFunctionPointer<SetExcludeDelegate>(api._setExclude);
        return exludeDelegate(set, value);
      }
    }

    /// <summary>
    /// Gets an iterator that can be used to retrieve values from the set.
    /// </summary>
    /// <param name="set">A reference to the set.</param>
    /// <returns>A set iterator.</returns>
    public static SHSetIterator GetIterator(this ref SHSet set)
    {
      Debug.Assert(set.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<SHSetInterface>(set._api.ToPointer());
        var getIteratorDelegate = Marshal.GetDelegateForFunctionPointer<SetGetIteratorDelegate>(api._setGetIterator);
        getIteratorDelegate(set, out var iter);
        return iter;
      }
    }

    /// <summary>
    /// Gets the next value in the set using the specified iterator.
    /// </summary>
    /// <param name="set">A reference to the set.</param>
    /// <param name="iter">A reference to a set iterator.</param>
    /// <param name="value">
    /// When this method returns, contains the next value in the set, if any;
    /// otherwise, the default value for the type of the value parameter.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns><c>true</c> if the set contained another element; otherwise, <c>false</c> when the iteration is complete.</returns>
    public static bool Next(this ref SHSet set, ref SHSetIterator iter, out SHVar value)
    {
      Debug.Assert(set.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<SHSetInterface>(set._api.ToPointer());
        var nextDelegate = Marshal.GetDelegateForFunctionPointer<SetNextDelegate>(api._setNext);
        return nextDelegate(set, ref iter, out value);
      }
    }

    /// <summary>
    /// Includes the <paramref name="value"/> in the set.
    /// </summary>
    /// <param name="set">A reference to the set.</param>
    /// <param name="value">The value to include in the set.</param>
    /// <returns><c>true</c> if the value was not already found before including it; otherwise, <c>false</c>.</returns>
    public static bool Include(this ref SHSet set, ref SHVar value)
    {
      Debug.Assert(set.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<SHSetInterface>(set._api.ToPointer());
        var includeDelegate = Marshal.GetDelegateForFunctionPointer<SetIncludeDelegate>(api._setInclude);
        return includeDelegate(set, value);
      }
    }

    /// <summary>
    /// Gets the number of values contained in the set.
    /// </summary>
    /// <param name="set">A reference to the set.</param>
    /// <returns>The number of values contained in the set.</returns>
    public static ulong Size(this ref SHSet set)
    {
      Debug.Assert(set.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<SHSetInterface>(set._api.ToPointer());
        var sizeDelegate = Marshal.GetDelegateForFunctionPointer<SetSizeDelegate>(api._setSize);
        return sizeDelegate(set);
      }
    }
  }

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void SetClearDelegate(SHSet set);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate SHBool SetContainsDelegate(SHSet set, SHVar value);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate SHBool SetExcludeDelegate(SHSet set, SHVar value);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void SetGetIteratorDelegate(SHSet set, out SHSetIterator iter);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate SHBool SetIncludeDelegate(SHSet set, SHVar value);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate SHBool SetNextDelegate(SHSet set, ref SHSetIterator iter, out SHVar value);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate ulong SetSizeDelegate(SHSet set);
}
