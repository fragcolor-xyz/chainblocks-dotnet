/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.CompilerServices;
using Fragcolor.Shards.Collections;

namespace Fragcolor.Shards
{
  /// <summary>
  /// Extension methods for <see cref="SHWireInfo"/>.
  /// </summary>
  public static class SHWireInfoExtensions
  {
    /// <summary>
    /// Returns the shards referenced in the wire.
    /// </summary>
    /// <param name="info">A reference to the wire info.</param>
    /// <returns>A collection of the shards referenced in the wire.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collections.Shards Shards(this ref SHWireInfo info)
    {
      return info._shards;
    }

    /// <summary>
    /// Determines whether the wire has failed.
    /// </summary>
    /// <param name="info">A reference to the wire info.</param>
    /// <returns><c>true</c> is the wire has failed; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasFailed(this ref SHWireInfo info)
    {
      return info._failed;
    }

    /// <summary>
    /// Determines whether the wire is looped.
    /// </summary>
    /// <param name="info">A reference to the wire info.</param>
    /// <returns><c>true</c> is the wire is looped; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLooped(this ref SHWireInfo info)
    {
      return info._looped;
    }

    /// <summary>
    /// Determines whether the wire is currently running.
    /// </summary>
    /// <param name="info">A reference to the wire info.</param>
    /// <returns><c>true</c> is the wire is running; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsRunning(this ref SHWireInfo info)
    {
      return info._running;
    }

    /// <summary>
    /// Determines whether the wire is unsafe.
    /// </summary>
    /// <param name="info">A reference to the wire info.</param>
    /// <returns><c>true</c> is the wire is unsafe; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnsafe(this ref SHWireInfo info)
    {
      return info._unsafe;
    }

    /// <summary>
    /// Gets the name of the wire.
    /// </summary>
    /// <param name="info">A reference to the wire info.</param>
    /// <returns>A string representing the name of the wire.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? Name(this ref SHWireInfo info)
    {
      return (string?)info._name;
    }
  }
}
