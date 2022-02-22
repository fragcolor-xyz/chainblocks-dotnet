/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Collections
{
  /// <summary>
  /// Extension methods for <see cref="CBTable"/>.
  /// </summary>
  public static class CBTableExtensions
  {
    /// <summary>
    /// Gets a reference to the <see cref="CBVar"/> at the specified <paramref name="key"/>.
    /// </summary>
    /// <param name="table">A reference to the collection.</param>
    /// <param name="key">The key of the value to get.</param>
    /// <returns>A reference to the element associated with the specified key.</returns>
    /// <remarks>
    /// If the specified key is not found, a new entry is created.
    /// </remarks>
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
          return ref Unsafe.AsRef<CBVar>(atDelegate(table, cbstr).ToPointer());
        }
        finally
        {
          cbstr.Dispose();
        }
      }
    }

    /// <summary>
    /// Clears the table, removing all its elements.
    /// </summary>
    /// <param name="table">A reference to the table.</param>
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

    /// <summary>
    /// Determines whether the table contains the specified <paramref name="key"/>.
    /// </summary>
    /// <param name="table">A reference to the table.</param>
    /// <param name="key">The key to locate in the table.</param>
    /// <returns></returns>
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

    /// <summary>
    /// Gets an iterator that can be used to retrieve key/value pairs from the table.
    /// </summary>
    /// <param name="table">A reference to the table.</param>
    /// <returns>A table iterator.</returns>
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

    /// <summary>
    /// Gets the next key/value pair in the table using the specified iterator.
    /// </summary>
    /// <param name="table">A reference to the table.</param>
    /// <param name="iter">A reference to a table iterator.</param>
    /// <param name="key">
    /// When this method returns, contains the next key in the table, if any; otherwise, <c>null</c>.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <param name="value">
    /// When this method returns, contains the next value in the table, if any;
    /// otherwise, the default value for the type of the value parameter.
    /// This parameter is passed uninitialized.
    /// </param>
    /// <returns><c>true</c> if the table contained another key/value pair; otherwise, <c>false</c> when the iteration is complete.</returns>
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

    /// <summary>
    /// Removes the value associated with the specified key from the table.
    /// </summary>
    /// <param name="table">A reference to the table.</param>
    /// <param name="key">The key of the value to remove.</param>
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

    /// <summary>
    /// Gets the number of key/value pairs contained in the table.
    /// </summary>
    /// <param name="table">A reference to the table.</param>
    /// <returns>The number of key/value pairs contained in the table.</returns>
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
  internal delegate IntPtr TableAtDelegate(CBTable table, CBString key);

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
