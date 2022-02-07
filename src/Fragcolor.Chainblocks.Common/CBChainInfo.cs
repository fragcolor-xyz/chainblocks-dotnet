/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

using Fragcolor.Chainblocks.Collections;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBChainInfo
  {
    //! Native struct, don't edit
    internal IntPtr _name;
    internal CBBool _looped;
    internal CBBool _unsafe;
    internal CBChainRef _chain;
    internal CBlocks _blocks;
    internal CBBool _running;
    internal IntPtr _failureMessage;
    internal IntPtr _finalOutput;
  }
}
