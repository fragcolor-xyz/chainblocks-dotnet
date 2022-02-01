/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBAudio
  {
    public uint sampleRate;
    public ushort sampleCount;
    public ushort channelCount;
    public IntPtr samples;
  }
}
