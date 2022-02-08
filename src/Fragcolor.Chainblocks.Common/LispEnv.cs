/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Fragcolor.Chainblocks
{
  public sealed class LispEnv : IDisposable
  {
    private int _disposeState;

    internal IntPtr _env;

    public LispEnv()
      : this(string.Empty)
    {
    }

    public LispEnv(string path)
    {
      _env = NativeMethods.cbLispCreate(path);
    }

    ~LispEnv()
    {
      Dispose(false);
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    public bool Eval(string code, IntPtr output)
    {
      if (code is null) throw new ArgumentNullException(nameof(code));

      var cbstr = (CBString)code;
      try
      {
        return NativeMethods.cbLispEval(_env, cbstr, output) != 0;
      }
      finally
      {
        cbstr.Dispose();
      }
    }

    private void Dispose(bool _)
    {
      if (Interlocked.CompareExchange(ref _disposeState, 1, 0) != 0) return;

      NativeMethods.cbLispDestroy(_env);
      _env = IntPtr.Zero;
    }
  }
}
