using System;
using System.Threading;

using Unity.Collections.LowLevel.Unsafe;

namespace Chainblocks
{
  public sealed class ExternalVariable : IDisposable
  {
    IntPtr _var;
    string _name;
    IntPtr _chain;
    private int _disposeState;

    public ref CBVar Value
    {
      get
      {
        unsafe
        {
          return ref UnsafeUtility.AsRef<CBVar>(_var.ToPointer());
        }
      }
    }

    public ExternalVariable(IntPtr chain, string name)
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

      Native.Core.FreeExternalVariable(_chain, _name);
      _var = IntPtr.Zero;
    }
  }
}