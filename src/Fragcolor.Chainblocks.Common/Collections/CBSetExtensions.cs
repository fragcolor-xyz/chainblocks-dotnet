/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Collections
{
  public static class CBSetExtensions
  {
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

    public static bool Include(this ref CBSet set, ref CBVar var)
    {
      Debug.Assert(set.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBSetInterface>(set._api.ToPointer());
        var includeDelegate = Marshal.GetDelegateForFunctionPointer<SetIncludeDelegate>(api._setInclude);
        return includeDelegate(set, var);
      }
    }

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
