/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Collections
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBTable
  {
    //! Native struct, don't edit
    internal IntPtr _opaque;
    internal IntPtr _api;

    public static CBTable New()
    {
      var tableNewDelegate = Marshal.GetDelegateForFunctionPointer<TableNewDelegate>(Native.Core._tableNew);
      return tableNewDelegate();
    }

    public readonly bool IsValid()
    {
      return _api != IntPtr.Zero;
    }
  }

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBTable TableNewDelegate();
}
