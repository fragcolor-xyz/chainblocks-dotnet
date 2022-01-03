using System;
using System.Runtime.InteropServices;

namespace Chainblocks
{
  public static class Native
  {
    private static IntPtr _core;
    private static CBCore _coreCopy;

    private const string Dll = "libcbl";
    private const CallingConvention Conv = CallingConvention.Cdecl;

    [DllImport(Native.Dll, CallingConvention = Native.Conv)]
    public static extern IntPtr chainblocksInterface(Int32 version);

    [DllImport(Native.Dll, CallingConvention = Native.Conv)]
    public static extern IntPtr cbLispCreate(String path);

    [DllImport(Native.Dll, CallingConvention = Native.Conv)]
    public static extern void cbLispDestroy(IntPtr lisp);

    [DllImport(Native.Dll, CallingConvention = Native.Conv)]
    public static extern byte cbLispEval(IntPtr lisp, String code, IntPtr output);

    public static ref CBCore Core
    {
      get
      {
        if (_core == IntPtr.Zero)
        {
          _core = chainblocksInterface(0x20200101);
          _coreCopy = Marshal.PtrToStructure<CBCore>(_core);
        }
        return ref _coreCopy;
      }
    }
  }    
}