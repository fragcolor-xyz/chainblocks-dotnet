using System;
using System.Runtime.InteropServices;

namespace Chainblocks
{
  public static class Native
  {
    private static IntPtr _core;
    private static CBCore _coreCopy;

    public static ref CBCore Core
    {
      get
      {
        if (_core == IntPtr.Zero)
        {
          _core = NativeMethods.chainblocksInterface(0x20200101);
          _coreCopy = Marshal.PtrToStructure<CBCore>(_core);
        }
        return ref _coreCopy;
      }
    }
  }
}
