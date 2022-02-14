/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.CompilerServices;

using Fragcolor.Chainblocks.Collections;

namespace Fragcolor.Chainblocks
{
  /// <summary>
  /// Extension methods for <see cref="CBChainInfo"/>.
  /// </summary>
  public static class CBChainInfoExtensions
  {
    /// <summary>
    /// Returns the blocks referenced in the chain.
    /// </summary>
    /// <param name="info">A reference to the chain info.</param>
    /// <returns>A collection of the blocks referenced in the chain.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CBlocks Blocks(this ref CBChainInfo info)
    {
      return info._blocks;
    }

    /// <summary>
    /// Determines whether the chain is looped.
    /// </summary>
    /// <param name="info">A reference to the chain info.</param>
    /// <returns><c>true</c> is the chain is looped; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLooped(this ref CBChainInfo info)
    {
      return info._looped;
    }

    /// <summary>
    /// Determines whether the chain is currently running.
    /// </summary>
    /// <param name="info">A reference to the chain info.</param>
    /// <returns><c>true</c> is the chain is running; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsRunning(this ref CBChainInfo info)
    {
      return info._running;
    }

    /// <summary>
    /// Determines whether the chain is unsafe.
    /// </summary>
    /// <param name="info">A reference to the chain info.</param>
    /// <returns><c>true</c> is the chain is unsafe; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnsafe(this ref CBChainInfo info)
    {
      return info._unsafe;
    }

    /// <summary>
    /// Gets the name of the chain.
    /// </summary>
    /// <param name="info">A reference to the chain info.</param>
    /// <returns>A string representing the name of the chain.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? Name(this ref CBChainInfo info)
    {
      return (string?)info._name;
    }
  }
}
