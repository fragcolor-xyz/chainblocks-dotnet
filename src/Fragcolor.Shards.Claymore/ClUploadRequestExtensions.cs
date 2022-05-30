/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Fragcolor.Shards.Claymore
{
  /// <summary>
  /// Extension methods for <see cref="ClUploadRequest"/>.
  /// </summary>
  public static class ClUploadRequestExtensions
  {
    /// <summary>
    /// Returns a pointer to this request.
    /// </summary>
    /// <param name="request">A reference to the request.</param>
    /// <returns>A pointer to the request.</returns>
    public static ClUploadRequestPtr AsPointer(this ref ClUploadRequest request)
    {
      unsafe
      {
        var ptr = Unsafe.AsPointer(ref request);
        return new() {_ptr = (IntPtr) ptr};
      }
    }

    /// <summary>
    /// Reinterprets the <paramref name="ptr"/> as a reference to a <see cref="ClUploadRequest"/>.
    /// </summary>
    /// <param name="ptr">The pointer whose value to reference.</param>
    /// <returns>A reference to the request.</returns>
    public static ref ClUploadRequest AsRef(this ClUploadRequestPtr ptr)
    {
      Debug.Assert(ptr.IsValid());
      unsafe
      {
        return ref Unsafe.AsRef<ClUploadRequest>(ptr._ptr.ToPointer());
      }
    }

    public static SHWireRef Shard(this ref ClUploadRequest request)
    {
      Debug.Assert(request._shard.type == SHType.Wire);
      return request._shard.wire;
    }
  }
}
