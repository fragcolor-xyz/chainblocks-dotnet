/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Fragcolor.Shards.Claymore
{
  public abstract class RequestBase : IDisposable
  {
    internal static readonly Lazy<Mesh> Mesh = new();

    protected readonly Mesh _mesh;
    protected ClPollStatePtr _lastPoll;

    protected int _completedState;
    private int _disposeState;

    protected RequestBase(Mesh? mesh = default)
    {
      _mesh =  mesh ?? Mesh.Value;
    }

    ~RequestBase()
    {
      Dispose(false);
    }

    public bool IsCompleted => Volatile.Read(ref _completedState) != 0;

    protected abstract SHWireRef Wire { get; }

    public void Cancel()
    {
      if (Interlocked.CompareExchange(ref _completedState, 2, 0) != 0) return;

      var output = Native.Core.StopWire(Wire);
      // TODO: we could salvage the result in case the wire had successfully completed
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

      _mesh.Tick();
      if (!NativeMethods.clmrPoll(Wire, out _lastPoll)) return;

      Volatile.Write(ref _completedState, 1);
      _mesh.Unschedule(Wire);
    }

    protected virtual bool Dispose(bool disposing)
    {
      if (Interlocked.CompareExchange(ref _disposeState, 1, 0) != 0) return true;

      if (disposing)
      {
        if (!Mesh.IsValueCreated || !ReferenceEquals(Mesh.Value, _mesh))
          _mesh.Dispose();
      }

      if (_lastPoll.IsValid()) NativeMethods.clmrPollFree(_lastPoll);

      return false;
    }
  }
}
