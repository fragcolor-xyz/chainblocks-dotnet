/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Diagnostics;
using Fragcolor.Chainblocks.Collections;

namespace Fragcolor.Chainblocks.Claymore
{
  public sealed class UploadRequest : RequestBase
  {
    private readonly ClUploadRequestPtr _requestPtr;
    private readonly Variable _data;

    public UploadRequest(byte[] bytes, string type, Node? node = default)
      : base(node)
    {
      _data = new Variable();
      _data.Value.type = CBType.Table;
      _data.Value.table = CBTable.New();
      _data.Value.table.At("container").SetString(type);
      _data.Value.table.At("data").SetBytes(bytes);

      _requestPtr = NativeMethods.clmrUpload(ref _data.Value);
      Debug.Assert(_requestPtr.IsValid());

      _node.Schedule(Chain);
    }

    protected override CBChainRef Chain => _requestPtr.AsRef().Chain();

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
