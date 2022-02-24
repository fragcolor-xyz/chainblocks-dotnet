/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Claymore
{
  [StructLayout(LayoutKind.Sequential)]
  public struct ClUploadRequest
  {
    internal CBVar _chain;
    internal CBVar _node;
    internal CBVar _signerKey;
    internal CBVar _authKey;
    internal CBVar _prototype;
    internal CBVar _data;
  }
}
