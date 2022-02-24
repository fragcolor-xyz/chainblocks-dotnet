/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Fragcolor.Chainblocks.Claymore
{
  public abstract class RequestBase : IDisposable
  {
    internal static readonly Lazy<Node> Node = new();

    protected readonly Node _node;
    protected ClPollStatePtr _lastPoll;

    protected int _completedState;
    private int _disposeState;

    protected RequestBase(Node? node = default)
    {
      _node =  node ?? Node.Value;
    }

    ~RequestBase()
    {
      Dispose(false);
    }

    public bool IsCompleted => Volatile.Read(ref _completedState) != 0;

    protected abstract CBChainRef Chain { get; }

    public void Cancel()
    {
      if (Interlocked.CompareExchange(ref _completedState, 2, 0) != 0) return;

      var output = Native.Core.StopChain(Chain);
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

    public void Tick()
    {
      if (Volatile.Read(ref _completedState) != 0) return;

      _node.Tick();
      if (!NativeMethods.clmrPoll(Chain, out _lastPoll)) return;

      Volatile.Write(ref _completedState, 1);
      _node.Unschedule(Chain);
    }

    protected virtual bool Dispose(bool disposing)
    {
      if (Interlocked.CompareExchange(ref _disposeState, 1, 0) != 0) return true;

      if (disposing)
      {
        if (!Node.IsValueCreated || !ReferenceEquals(Node.Value, _node))
          _node.Dispose();
      }

      if (_lastPoll.IsValid()) NativeMethods.clmrPollFree(_lastPoll);

      return false;
    }
  }
}
