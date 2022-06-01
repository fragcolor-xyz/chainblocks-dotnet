/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Diagnostics;
using System.Threading;

namespace Fragcolor.Shards.Claymore;

public sealed class GetRequest : RequestBase
{
  private readonly ClGetDataRequestPtr _requestPtr;

  public GetRequest(string hash, Mesh? mesh = default)
    : this(Util.HexToBytes(hash), mesh)
  {
  }

  public GetRequest(byte[] fragmentHash, Mesh? mesh = default)
    : base(mesh)
  {
    _requestPtr = NativeMethods.clmrGetDataStart(fragmentHash);
    Debug.Assert(_requestPtr.IsValid());

    _mesh.Schedule(Wire);
  }

  protected override SHWireRef Wire => _requestPtr.AsRef().Wire();

  public Variable GetResult()
  {
    var variable = new Variable();
    if (Volatile.Read(ref _completedState) != 1 || !_lastPoll.IsValid()) return variable;

    ref var poll = ref _lastPoll.AsRef();
    switch (poll._tag)
    {
      case ClPollStateTag.Failed:
        // TODO: save the error as string in the variable?
        break;

      case ClPollStateTag.Finished:
        Native.Core.CloneVar(ref variable.Value, ref _requestPtr.AsRef()._result);
        break;
    }
    return variable;
  }

  protected override bool Dispose(bool disposing)
  {
    if (base.Dispose(disposing)) return true;

    NativeMethods.clmrGetDataFree(_requestPtr);
    return false;
  }
}
