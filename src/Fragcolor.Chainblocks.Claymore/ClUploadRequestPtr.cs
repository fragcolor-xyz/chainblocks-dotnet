/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Claymore
{
  /// <summary>
  /// Represents a pointer to a <see cref="ClGetDataRequest"/>.
  /// </summary>
  /// <seealso cref="ClUploadRequestExtensions.AsPointer(ref ClUploadRequest)"/>
  /// <seealso cref="ClUploadRequestExtensions.AsRef(ClUploadRequestPtr)"/>
  [StructLayout(LayoutKind.Sequential)]
  public struct ClUploadRequestPtr
  {
    internal IntPtr _ptr;

    public bool IsValid()
    {
      return _ptr != IntPtr.Zero;
    }
  }
}
