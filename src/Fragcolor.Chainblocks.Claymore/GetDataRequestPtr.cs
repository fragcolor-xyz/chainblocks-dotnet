/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Claymore
{
  /// <summary>
  /// Represents a pointer to a <see cref="GetDataRequest"/>.
  /// </summary>
  /// <seealso cref="GetDataRequestExtensions.AsPointer(ref GetDataRequest)"/>
  /// <seealso cref="GetDataRequestExtensions.AsRef(GetDataRequestPtr)"/>
  [StructLayout(LayoutKind.Sequential)]
  public struct GetDataRequestPtr
  {
    internal IntPtr _ptr;

    public bool IsValid()
    {
      return _ptr != IntPtr.Zero;
    }
  }
}
