/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  /// <summary>
  /// Represents an object.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct CBObject
  {
    //! Native struct, don't edit
    internal IntPtr _obj;
    internal int _vendorId;
    internal int _typeId;
  }
}
