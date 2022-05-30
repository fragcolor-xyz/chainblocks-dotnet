/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

namespace Fragcolor.Shards.Claymore
{
  [StructLayout(LayoutKind.Sequential)]
  public struct ClUploadRequest
  {
    internal SHVar _shard;
    internal SHVar _node;
    internal SHVar _signerKey;
    internal SHVar _authKey;
    internal SHVar _prototype;
    internal SHVar _data;
  }
}
