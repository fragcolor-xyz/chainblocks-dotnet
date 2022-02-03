/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  public static class CBTableExtensions
  {
    public static ref CBVar At(this ref CBTable table, string key)
    {
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBTableInterface>(table._api.ToPointer());
        var atDelegate = Marshal.GetDelegateForFunctionPointer<TableAtDelegate>(api._tableAt);
        return ref atDelegate(table, key);
      }
    }

    public static void Clear(this ref CBTable table)
    {
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBTableInterface>(table._api.ToPointer());
        var clearDelegate = Marshal.GetDelegateForFunctionPointer<TableClearDelegate>(api._tableClear);
        clearDelegate(table);
      }
    }

    public static bool Contains(this ref CBTable table, string key)
    {
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBTableInterface>(table._api.ToPointer());
        var containsDelegate = Marshal.GetDelegateForFunctionPointer<TableContainsDelegate>(api._tableContains);
        return containsDelegate(table, key);
      }
    }

    public static CBTableIterator GetIterator(this ref CBTable table)
    {
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBTableInterface>(table._api.ToPointer());
        var getIteratorDelegate = Marshal.GetDelegateForFunctionPointer<TableGetIteratorDelegate>(api._tableGetIterator);
        getIteratorDelegate(table, out var iter);
        return iter;
      }
    }

    public static bool Next(this ref CBTable table, ref CBTableIterator iter, out string? key, out CBVar value)
    {
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBTableInterface>(table._api.ToPointer());
        var nextDelegate = Marshal.GetDelegateForFunctionPointer<TableNextDelegate>(api._tableNext);
        var result = nextDelegate(table, ref iter, out var ptr, out value);
        key = result ? Marshal.PtrToStringAnsi(ptr) : null;
        return result;
      }
    }

    public static void Remove(this ref CBTable table, string key)
    {
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBTableInterface>(table._api.ToPointer());
        var removeDelegate = Marshal.GetDelegateForFunctionPointer<TableRemoveDelegate>(api._tableRemove);
        removeDelegate(table, key);
      }
    }

    public static ulong Size(this ref CBTable table)
    {
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBTableInterface>(table._api.ToPointer());
        var sizeDelegate = Marshal.GetDelegateForFunctionPointer<TableSizeDelegate>(api._tableSize);
        return sizeDelegate(table);
      }
    }
  }

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate ref CBVar TableAtDelegate(CBTable table, string key);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void TableClearDelegate(CBTable table);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate CBBool TableContainsDelegate(CBTable table, string key);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void TableGetIteratorDelegate(CBTable table, out CBTableIterator iter);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate CBBool TableNextDelegate(CBTable table, ref CBTableIterator iter, out IntPtr key, out CBVar value);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void TableRemoveDelegate(CBTable table, string key);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate ulong TableSizeDelegate(CBTable table);
}
