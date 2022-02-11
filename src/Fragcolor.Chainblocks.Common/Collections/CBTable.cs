/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Collections
{
  /// <summary>
  /// Represents a table of <see cref="CBVar"/> indexed by a <see cref="string"/> key.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct CBTable
  {
    //! Native struct, don't edit
    internal IntPtr _opaque;
    internal IntPtr _api;

    /// <summary>
    /// Initializes a new instance of <see cref="CBTable"/>.
    /// </summary>
    /// <returns>A new instance of <see cref="CBTable"/>.</returns>
    public static CBTable New()
    {
      var tableNewDelegate = Marshal.GetDelegateForFunctionPointer<TableNewDelegate>(Native.Core._tableNew);
      return tableNewDelegate();
    }

    /// <summary>
    /// Checks whether this instance is a valid set (i.e. its memory is properly allocated, and its API can be called).
    /// </summary>
    /// <returns><c>true</c> if this instance represents a valid set; otherwise, <c>false</c>.</returns>
    public readonly bool IsValid()
    {
      return _api != IntPtr.Zero;
    }
  }

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBTable TableNewDelegate();
}
