/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Shards.Collections
{
  /// <summary>
  /// Represents a set of <see cref="SHVar"/>.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct SHSet
  {
    //! Native struct, don't edit
    internal IntPtr _opaque;
    internal IntPtr _api;

    /// <summary>
    /// Initializes a new instance of <see cref="SHSet"/>.
    /// </summary>
    /// <returns>A new instance of <see cref="SHSet"/>.</returns>
    public static SHSet New()
    {
      var setNewDelegate = Marshal.GetDelegateForFunctionPointer<SetNewDelegate>(Native.Core._setNew);
      return setNewDelegate();
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
  internal delegate SHSet SetNewDelegate();
}
