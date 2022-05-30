/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Shards.Claymore
{
  /// <summary>
  /// Represents a pointer to a <see cref="ClGetDataRequest"/>.
  /// </summary>
  /// <seealso cref="ClGetDataRequestExtensions.AsPointer(ref ClGetDataRequest)"/>
  /// <seealso cref="ClGetDataRequestExtensions.AsRef(ClGetDataRequestPtr)"/>
  [StructLayout(LayoutKind.Sequential)]
  public struct ClGetDataRequestPtr
  {
    internal IntPtr _ptr;

    public bool IsValid()
    {
      return _ptr != IntPtr.Zero;
    }
  }
}
