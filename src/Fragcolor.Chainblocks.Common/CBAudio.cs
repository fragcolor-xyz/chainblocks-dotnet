/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBAudio
  {
    //! Native struct, don't edit
    internal uint _sampleRate;
    internal ushort _sampleCount;
    internal ushort _channelCount;
    internal IntPtr _samples;
  }
}
