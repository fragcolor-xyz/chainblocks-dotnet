/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Fragcolor.Chainblocks.Claymore
{
  /// <summary>
  /// Extension methods for <see cref="ClGetDataRequest"/>.
  /// </summary>
  public static class ClGetDataRequestExtensions
  {
    /// <summary>
    /// Returns a pointer to this request.
    /// </summary>
    /// <param name="request">A reference to the request.</param>
    /// <returns>A pointer to the request.</returns>
    public static ClGetDataRequestPtr AsPointer(this ref ClGetDataRequest request)
    {
      unsafe
      {
        var ptr = Unsafe.AsPointer(ref request);
        return new() { _ptr = (IntPtr)ptr };
      }
    }

    /// <summary>
    /// Reinterprets the <paramref name="ptr"/> as a reference to a <see cref="ClGetDataRequest"/>.
    /// </summary>
    /// <param name="ptr">The pointer whose value to reference.</param>
    /// <returns>A reference to the request.</returns>
    public static ref ClGetDataRequest AsRef(this ClGetDataRequestPtr ptr)
    {
      Debug.Assert(ptr.IsValid());
      unsafe
      {
        return ref Unsafe.AsRef<ClGetDataRequest>(ptr._ptr.ToPointer());
      }
    }

    public static CBChainRef Chain(this ref ClGetDataRequest request)
    {
      Debug.Assert(request._chain.type == CBType.Chain);
      return request._chain.chain;
    }
  }
}
