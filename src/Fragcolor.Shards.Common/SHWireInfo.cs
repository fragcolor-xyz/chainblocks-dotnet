/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Shards
{
  /// <summary>
  /// Represents information about a wire.
  /// </summary>
  /// <remarks>
  /// See <see cref="SHWireInfoExtensions"/> for available methods on this struct.
  /// </remarks>
  [StructLayout(LayoutKind.Sequential)]
  public struct SHWireInfo
  {
    //! Native struct, don't edit
    internal SHString _name;
    internal SHBool _looped;
    internal SHBool _unsafe;
    internal SHWireRef Wire;
    internal Collections.Shards _shards;
    internal SHBool _running;
    internal SHBool _failed;
    internal IntPtr _failureMessage;
    internal IntPtr _finalOutput;
  }
}
