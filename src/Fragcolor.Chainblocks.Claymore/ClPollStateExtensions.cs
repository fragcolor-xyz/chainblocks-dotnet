/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Fragcolor.Chainblocks.Claymore
{
  /// <summary>
  /// Extension methods for <see cref="ClPollState"/>.
  /// </summary>
  public static class ClPollStateExtensions
  {
    /// <summary>
    /// Reinterprets the <paramref name="ptr"/> as a reference to a <see cref="ClPollState"/>.
    /// </summary>
    /// <param name="ptr">The pointer whose value to reference.</param>
    /// <returns>A reference to the state.</returns>
    public static ref ClPollState AsRef(this ClPollStatePtr ptr)
    {
      Debug.Assert(ptr.IsValid());
      unsafe
      {
        return ref Unsafe.AsRef<ClPollState>(ptr._ptr.ToPointer());
      }
    }
  }
}
