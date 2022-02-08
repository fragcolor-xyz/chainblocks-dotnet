/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBImage
  {
    //! Native struct, don't edit
    internal ushort _width;
    internal ushort _height;
    internal byte _channelCount;
    internal byte _flags;
    internal IntPtr _data;
  }
}
