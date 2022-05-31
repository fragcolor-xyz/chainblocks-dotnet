/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Shards
{
  /// <summary>
  /// Represents a reference to a wire.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct SHWireRef
  {
    internal IntPtr _ref;

    public readonly bool IsValid()
    {
      return _ref != IntPtr.Zero;
    }
  }
}
