/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBChainRef
  {
    internal IntPtr _ref;

    public bool IsValid()
    {
      return _ref != IntPtr.Zero;
    }
  }
}
