using System;
using System.Runtime.InteropServices;

namespace Chainblocks
{
  public static class CBCoreExtensions
  {
    public static IntPtr Alloc(this ref CBCore core, UInt32 size)
    {
      var allocDelegate = Marshal.GetDelegateForFunctionPointer<AllocDelegate>(core._alloc);
      return allocDelegate(size);
    }

    static public void Free(this ref CBCore core, IntPtr ptr)
    {
      var freeDelegate = Marshal.GetDelegateForFunctionPointer<FreeDelegate>(core._free);
      freeDelegate(ptr);
    }

    static public byte Suspend(this ref CBCore core, IntPtr context, double duration)
    {
      var suspendDelegate = Marshal.GetDelegateForFunctionPointer<SuspendDelegate>(core._suspend);
      return suspendDelegate(context, duration);
    }

    static public IntPtr AllocExternalVariable(this ref CBCore core, IntPtr chain, string name)
    {
      var allocExternalVariableDelegate = Marshal.GetDelegateForFunctionPointer<AllocExternalVariableDelegate>(core._allocExternalVariable);
      return allocExternalVariableDelegate(chain, name);
    }

    static public void FreeExternalVariable(this ref CBCore core, IntPtr chain, string name)
    {
      var freeExternalVariableDelegate = Marshal.GetDelegateForFunctionPointer<FreeExternalVariableDelegate>(core._freeExternalVariable);
      freeExternalVariableDelegate(chain, name);
    }

    static public IntPtr CreateNode(this ref CBCore core)
    {
      var createNodeDelegate = Marshal.GetDelegateForFunctionPointer<CreateNodeDelegate>(core._createNode);
      return createNodeDelegate();
    }

    static public byte Tick(this ref CBCore core, IntPtr nodeRef)
    {
      var tickDelegate = Marshal.GetDelegateForFunctionPointer<TickDelegate>(core._tick);
      return tickDelegate(nodeRef);
    }

    static public void Schedule(this ref CBCore core, IntPtr nodeRef, IntPtr chainRef)
    {
      var scheduleDelegate = Marshal.GetDelegateForFunctionPointer<ScheduleDelegate>(core._schedule);
      scheduleDelegate(nodeRef, chainRef);
    }

    static public void Unschedule(this ref CBCore core, IntPtr nodeRef, IntPtr chainRef)
    {
      var scheduleDelegate = Marshal.GetDelegateForFunctionPointer<ScheduleDelegate>(core._unschedule);
      scheduleDelegate(nodeRef, chainRef);
    }

    static public void DestroyVar(this ref CBCore core, IntPtr varRef)
    {
      var destroyVarDelegate = Marshal.GetDelegateForFunctionPointer<DestroyVarDelegate>(core._destroyVar);
      destroyVarDelegate(varRef);
    }
  }
}
