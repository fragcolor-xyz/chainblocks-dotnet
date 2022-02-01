/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBImage
  {
    public ushort width;
    public ushort height;
    public byte channelCount;
    public byte flags;
    public IntPtr data;
  }
}
