using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Fragcolor.Chainblocks
{
  public sealed class ExternalVariable : IDisposable
  {
    internal IntPtr _var;
    internal string _name;
    internal Chain _chain;
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

    public ExternalVariable(Chain chain, string name)
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

    private void Dispose(bool _)
    {
      if (Interlocked.CompareExchange(ref _disposeState, 1, 0) != 0) return;

      Native.Core.FreeExternalVariable(_chain, _name);
      _var = IntPtr.Zero;
    }
  }
}
