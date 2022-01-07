using System;
using System.Threading;

namespace Chainblocks
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

    public byte Eval(string code, IntPtr output)
    {
      return NativeMethods.cbLispEval(_env, code, output);
    }

    private void Dispose(bool _)
    {
      if (Interlocked.CompareExchange(ref _disposeState, 1, 0) != 0) return;

      NativeMethods.cbLispDestroy(_env);
      _env = IntPtr.Zero;
    }
  }
}
