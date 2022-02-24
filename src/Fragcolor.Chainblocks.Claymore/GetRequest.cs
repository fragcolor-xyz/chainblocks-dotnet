/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Diagnostics;
using System.Threading;

namespace Fragcolor.Chainblocks.Claymore;

public sealed class GetRequest : RequestBase
{
  private readonly ClGetDataRequestPtr _requestPtr;

  public GetRequest(string hash, Node? node = default)
    : this(Util.HexToBytes(hash), node)
  {
  }

  public GetRequest(byte[] fragmentHash, Node? node = default)
    : base(node)
  {
    _requestPtr = NativeMethods.clmrGetDataStart(fragmentHash);
    Debug.Assert(_requestPtr.IsValid());

    _node.Schedule(Chain);
  }

  protected override CBChainRef Chain => _requestPtr.AsRef().Chain();

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
