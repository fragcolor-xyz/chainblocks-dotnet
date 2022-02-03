/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Fragcolor.Chainblocks
{
  public sealed class ExternalVariable : IDisposable
  {
    private CBChainRef _chain;
    private readonly string _name;
    private IntPtr _var;

    private int _disposeState;

    public ref CBVar Value
    {
      get
      {
        unsafe
        {
          return ref Unsafe.AsRef<CBVar>(_var.ToPointer());
        }
      }
    }

    public ExternalVariable(CBChainRef chain, string name)
    {
      _chain = chain;
      _name = name;
      _var = Native.Core.AllocExternalVariable(chain, _name);
    }

    ~ExternalVariable()
    {
      Dispose(false);
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
      if (Interlocked.CompareExchange(ref _disposeState, 1, 0) != 0) return;

      // Finalization order cannot be guaranteed
      if (disposing)
      {
        Native.Core.FreeExternalVariable(_chain, _name);
      }

      _chain._ref = IntPtr.Zero;
      _var = IntPtr.Zero;
    }
  }
}
