/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Fragcolor.Chainblocks
{
  public sealed class Variable : IDisposable
  {
    private IntPtr _mem;
    private readonly bool _destroy;
    private int _disposeState;

    public ref CBVar Value
    {
      get
      {
        unsafe
        {
          return ref Unsafe.AsRef<CBVar>(_mem.ToPointer());
        }
      }
    }

    public IntPtr Ptr
    {
      get { return _mem; }
    }

    public Variable(bool destroy = true)
    {
      _destroy = destroy;
      _mem = Native.Core.Alloc(32);
    }

    ~Variable()
    {
      Dispose(false);
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    private void Dispose(bool _)
    {
      if (Interlocked.CompareExchange(ref _disposeState, 1, 0) != 0) return;

      if (_destroy)
      {
        Native.Core.DestroyVar(_mem);
      }
      Native.Core.Free(_mem);
      _mem = IntPtr.Zero;
    }
  }
}
