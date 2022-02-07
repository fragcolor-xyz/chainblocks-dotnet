/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Fragcolor.Chainblocks.Collections;

namespace Fragcolor.Chainblocks
{
  public static class CBChainInfoExtensions
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CBlocks Blocks(this ref CBChainInfo info)
    {
      return info._blocks;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLooped(this ref CBChainInfo info)
    {
      return info._looped;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsRunning(this ref CBChainInfo info)
    {
      return info._running;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnsafe(this ref CBChainInfo info)
    {
      return info._unsafe;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Name(this ref CBChainInfo info)
    {
      return Marshal.PtrToStringAnsi(info._name);
    }
  }
}
