/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Fragcolor.Chainblocks.Claymore;

public class Request : IDisposable
{
  internal static readonly Lazy<Node> Node = new();

  private readonly Node _node;
  private readonly GetDataRequestPtr _requestPtr;
  private PollStatePtr _lastPoll;

  private int _completedState;
  private int _disposeState;

  public Request(string hash, Node? node = default)
    : this(Util.HexToBytes(hash), node)
  {
  }

  public Request(byte[] fragmentHash, Node? node = default)
  {
    _requestPtr = NativeMethods.clmrGetDataStart(fragmentHash);
    Debug.Assert(_requestPtr.IsValid());

    _node = node ?? Node.Value;
    _node.Schedule(_requestPtr.AsRef().Chain());
  }

  ~Request()
  {
    Dispose(false);
  }

  public bool IsCompleted => Volatile.Read(ref _completedState) != 0;

  public void Cancel()
  {
    if (Interlocked.CompareExchange(ref _completedState, 2, 0) != 0) return;

    var output = Native.Core.StopChain(_requestPtr.AsRef().Chain());
    // TODO: we could salvage the result in case the chain had successfully completed
    unsafe
    {
      var ptr = (IntPtr)Unsafe.AsPointer(ref output);
      Native.Core.DestroyVar(ptr);
    }
  }

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  public Variable GetData()
  {
    var variable = new Variable();
    if (Volatile.Read(ref _completedState) != 1 || !_lastPoll.IsValid()) return variable;

    ref var poll = ref _lastPoll.AsRef();
    switch (poll._tag)
    {
      case PollStateTag.Failed:
        // TODO: save the error as string in the variable?
        break;

      case PollStateTag.Finished:
        Native.Core.CloneVar(ref variable.Value, ref _requestPtr.AsRef()._result);
        break;
    }
    return variable;
  }

  public void Tick()
  {
    if (Volatile.Read(ref _completedState) != 0) return;

    _node.Tick();
    if (!NativeMethods.clmrPoll(_requestPtr.AsRef().Chain(), out _lastPoll)) return;

    Volatile.Write(ref _completedState, 1);
    _node.Unschedule(_requestPtr.AsRef().Chain());
  }

  protected virtual void Dispose(bool disposing)
  {
    if (Interlocked.CompareExchange(ref _disposeState, 1, 0) != 0) return;

    if (disposing)
    {
      if (!Node.IsValueCreated || !ReferenceEquals(Node.Value, _node))
        _node.Dispose();
    }

    if (_lastPoll.IsValid()) NativeMethods.clmrPollFree(_lastPoll);
    NativeMethods.clmrGetDataFree(_requestPtr);
  }
}
