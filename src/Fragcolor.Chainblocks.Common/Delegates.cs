using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate IntPtr AllocDelegate(uint size);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void FreeDelegate(IntPtr ptr);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate byte SuspendDelegate(IntPtr context, double duration);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate IntPtr AllocExternalVariableDelegate(IntPtr chain, string name);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void FreeExternalVariableDelegate(IntPtr chain, string name);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate IntPtr CreateNodeDelegate();

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate byte TickDelegate(IntPtr nodeRef);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void ScheduleDelegate(IntPtr nodeRef, IntPtr chainRef);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void DestroyVarDelegate(IntPtr varRef);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate CBVar ActivateUnmanagedDelegate(IntPtr context, IntPtr input);
}
