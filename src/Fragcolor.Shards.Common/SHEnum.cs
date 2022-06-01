/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

namespace Fragcolor.Shards
{
  /// <summary>
  /// Represents an enum.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct SHEnum
  {
    //! Native struct, don't edit
    internal int _value;
    internal int _vendorId;
    internal int _typeId;
  }
}
