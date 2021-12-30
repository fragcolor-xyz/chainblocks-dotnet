using System;
using System.Threading;

using Unity.Collections.LowLevel.Unsafe;

namespace Chainblocks
{
  public sealed class Variable : IDisposable
  {
    private IntPtr _mem;
    private bool _destroy;
    private int _disposeState;

    public ref CBVar Value
    {
      get
      {
        unsafe
        {
          return ref UnsafeUtility.AsRef<CBVar>(_mem.ToPointer());
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

    private void Dispose(bool disposing)
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