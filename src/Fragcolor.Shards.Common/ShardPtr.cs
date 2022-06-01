/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Shards
{
  /// <summary>
  /// Represents a pointer to a <see cref="Shard"/>.
  /// </summary>
  /// <seealso cref="ShardExtensions.AsPointer(ref Shard)"/>
  /// <seealso cref="ShardExtensions.AsRef(ShardPtr)"/>
  [StructLayout(LayoutKind.Sequential)]
  public struct ShardPtr
  {
    internal IntPtr _ref;

    public readonly bool IsValid()
    {
      return _ref != IntPtr.Zero;
    }
  }
}
