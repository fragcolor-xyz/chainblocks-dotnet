/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Diagnostics;
using Fragcolor.Shards.Collections;

namespace Fragcolor.Shards.Claymore
{
  public sealed class UploadRequest : RequestBase
  {
    private readonly ClUploadRequestPtr _requestPtr;
    private readonly Variable _data;

    public UploadRequest(byte[] bytes, string type, Mesh? mesh = default)
      : base(mesh)
    {
      _data = new Variable();
      _data.Value.type = SHType.Table;
      _data.Value.table = SHTable.New();
      _data.Value.table.At("container").SetString(type);
      _data.Value.table.At("data").SetBytes(bytes);

      _requestPtr = NativeMethods.clmrUpload(ref _data.Value);
      Debug.Assert(_requestPtr.IsValid());

      _mesh.Schedule(Wire);
    }

    protected override SHWireRef Wire => _requestPtr.AsRef().Shard();

    protected override bool Dispose(bool disposing)
    {
      if (base.Dispose(disposing)) return true;

      if (disposing)
      {
        _data.Dispose();
      }

      NativeMethods.clmrUploadFree(_requestPtr);
      return false;
    }
  }
}
