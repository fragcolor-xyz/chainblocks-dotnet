/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Collections
{
  public static class CBTableExtensions
  {
    public static ref CBVar At(this ref CBTable table, string key)
    {
      Debug.Assert(table.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBTableInterface>(table._api.ToPointer());
        var atDelegate = Marshal.GetDelegateForFunctionPointer<TableAtDelegate>(api._tableAt);
        var cbstr = (CBString)key;
        try
        {
          return ref atDelegate(table, cbstr);
        }
        finally
        {
          cbstr.Dispose();
        }
      }
    }

    public static void Clear(this ref CBTable table)
    {
      Debug.Assert(table.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBTableInterface>(table._api.ToPointer());
        var clearDelegate = Marshal.GetDelegateForFunctionPointer<TableClearDelegate>(api._tableClear);
        clearDelegate(table);
      }
    }

    public static bool Contains(this ref CBTable table, string key)
    {
      Debug.Assert(table.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBTableInterface>(table._api.ToPointer());
        var containsDelegate = Marshal.GetDelegateForFunctionPointer<TableContainsDelegate>(api._tableContains);
        var cbstr = (CBString)key;
        try
        {
          return containsDelegate(table, cbstr);
        }
        finally
        {
          cbstr.Dispose();
        }
      }
    }

    public static CBTableIterator GetIterator(this ref CBTable table)
    {
      Debug.Assert(table.IsValid());
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
      Debug.Assert(table.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBTableInterface>(table._api.ToPointer());
        var nextDelegate = Marshal.GetDelegateForFunctionPointer<TableNextDelegate>(api._tableNext);
        var result = nextDelegate(table, ref iter, out var ptr, out value);
        key = result ? (string?)ptr : null;
        return result;
      }
    }

    public static void Remove(this ref CBTable table, string key)
    {
      Debug.Assert(table.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBTableInterface>(table._api.ToPointer());
        var removeDelegate = Marshal.GetDelegateForFunctionPointer<TableRemoveDelegate>(api._tableRemove);
        var cbstr = (CBString)key;
        try
        {
          removeDelegate(table, cbstr);
        }
        finally
        {
          cbstr.Dispose();
        }
      }
    }

    public static ulong Size(this ref CBTable table)
    {
      Debug.Assert(table.IsValid());
      unsafe
      {
        ref var api = ref Unsafe.AsRef<CBTableInterface>(table._api.ToPointer());
        var sizeDelegate = Marshal.GetDelegateForFunctionPointer<TableSizeDelegate>(api._tableSize);
        return sizeDelegate(table);
      }
    }
  }

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate ref CBVar TableAtDelegate(CBTable table, CBString key);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void TableClearDelegate(CBTable table);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBBool TableContainsDelegate(CBTable table, CBString key);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void TableGetIteratorDelegate(CBTable table, out CBTableIterator iter);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBBool TableNextDelegate(CBTable table, ref CBTableIterator iter, out CBString key, out CBVar value);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void TableRemoveDelegate(CBTable table, CBString key);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate ulong TableSizeDelegate(CBTable table);
}
