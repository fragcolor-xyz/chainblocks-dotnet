/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Collections
{
  /// <summary>
  /// Extension methods for <see cref="CBSet"/>.
  /// </summary>
  public static class CBSetExtensions
  {
    /// <summary>
    /// Clears the set, removing all its elements.
    /// </summary>
    /// <param name="set">A reference to the set.</param>
    public static void Clear(this ref CBSet set)
    {
      Debug.Assert(set.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBSetInterface>(set._api.ToPointer());
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
    public static bool Contains(this ref CBSet set, ref CBVar value)
    {
      Debug.Assert(set.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBSetInterface>(set._api.ToPointer());
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
    public static bool Exclude(this ref CBSet set, ref CBVar value)
    {
      Debug.Assert(set.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBSetInterface>(set._api.ToPointer());
        var exludeDelegate = Marshal.GetDelegateForFunctionPointer<SetExcludeDelegate>(api._setExclude);
        return exludeDelegate(set, value);
      }
    }

    /// <summary>
    /// Gets an iterator that can be used to retrieve values from the set.
    /// </summary>
    /// <param name="set">A reference to the set.</param>
    /// <returns>A set iterator.</returns>
    public static CBSetIterator GetIterator(this ref CBSet set)
    {
      Debug.Assert(set.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBSetInterface>(set._api.ToPointer());
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
    public static bool Next(this ref CBSet set, ref CBSetIterator iter, out CBVar value)
    {
      Debug.Assert(set.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBSetInterface>(set._api.ToPointer());
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
    public static bool Include(this ref CBSet set, ref CBVar value)
    {
      Debug.Assert(set.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBSetInterface>(set._api.ToPointer());
        var includeDelegate = Marshal.GetDelegateForFunctionPointer<SetIncludeDelegate>(api._setInclude);
        return includeDelegate(set, value);
      }
    }

    /// <summary>
    /// Gets the number of values contained in the set.
    /// </summary>
    /// <param name="set">A reference to the set.</param>
    /// <returns>The number of values contained in the set.</returns>
    public static ulong Size(this ref CBSet set)
    {
      Debug.Assert(set.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBSetInterface>(set._api.ToPointer());
        var sizeDelegate = Marshal.GetDelegateForFunctionPointer<SetSizeDelegate>(api._setSize);
        return sizeDelegate(set);
      }
    }
  }

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void SetClearDelegate(CBSet set);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBBool SetContainsDelegate(CBSet set, CBVar value);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBBool SetExcludeDelegate(CBSet set, CBVar value);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void SetGetIteratorDelegate(CBSet set, out CBSetIterator iter);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBBool SetIncludeDelegate(CBSet set, CBVar value);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBBool SetNextDelegate(CBSet set, ref CBSetIterator iter, out CBVar value);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate ulong SetSizeDelegate(CBSet set);
}
