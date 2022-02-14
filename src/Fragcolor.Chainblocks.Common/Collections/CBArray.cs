/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Collections
{
  /// <summary>
  /// Represents a pointer to an unmanaged array.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct CBArray
  {
    //! Native struct, don't edit
    internal IntPtr _ptr;
  }
}
