/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  public static class CBCoreExtensions
  {
    public static IntPtr Alloc(this ref CBCore core, uint size)
    {
      var allocDelegate = Marshal.GetDelegateForFunctionPointer<AllocDelegate>(core._alloc);
      return allocDelegate(size);
    }

    public static void Free(this ref CBCore core, IntPtr ptr)
    {
      var freeDelegate = Marshal.GetDelegateForFunctionPointer<FreeDelegate>(core._free);
      freeDelegate(ptr);
    }

    public static void Log(this ref CBCore core, string message)
    {
      var logDelegate = Marshal.GetDelegateForFunctionPointer<LogDelegate>(core._log);
      logDelegate(message);
    }

    public static void Log(this ref CBCore core, string message, CBLogLevel level)
    {
      var logLevelDelegate = Marshal.GetDelegateForFunctionPointer<LogLevelDelegate>(core._logLevel);
      logLevelDelegate((int)level, message);
    }

    public static CBNodeRef CreateNode(this ref CBCore core)
    {
      var createNodeDelegate = Marshal.GetDelegateForFunctionPointer<CreateNodeDelegate>(core._createNode);
      return createNodeDelegate();
    }

    public static void Schedule(this ref CBCore core, CBNodeRef nodeRef, CBChainRef chainRef)
    {
      var scheduleDelegate = Marshal.GetDelegateForFunctionPointer<ScheduleDelegate>(core._schedule);
      scheduleDelegate(nodeRef, chainRef);
    }

    public static void Unschedule(this ref CBCore core, CBNodeRef nodeRef, CBChainRef chainRef)
    {
      var unscheduleDelegate = Marshal.GetDelegateForFunctionPointer<UnscheduleDelegate>(core._unschedule);
      unscheduleDelegate(nodeRef, chainRef);
    }

    public static bool Tick(this ref CBCore core, CBNodeRef nodeRef)
    {
      var tickDelegate = Marshal.GetDelegateForFunctionPointer<TickDelegate>(core._tick);
      return tickDelegate(nodeRef);
    }

    public static void Sleep(this ref CBCore core, double seconds, bool runCallbacks)
    {
      var sleepDelegate = Marshal.GetDelegateForFunctionPointer<SleepDelegate>(core._sleep);
      sleepDelegate(seconds, runCallbacks);
    }

    public static IntPtr AllocExternalVariable(this ref CBCore core, CBChainRef chainRef, string name)
    {
      var allocExternalVariableDelegate = Marshal.GetDelegateForFunctionPointer<AllocExternalVariableDelegate>(core._allocExternalVariable);
      return allocExternalVariableDelegate(chainRef, name);
    }

    public static void FreeExternalVariable(this ref CBCore core, CBChainRef chainRef, string name)
    {
      var freeExternalVariableDelegate = Marshal.GetDelegateForFunctionPointer<FreeExternalVariableDelegate>(core._freeExternalVariable);
      freeExternalVariableDelegate(chainRef, name);
    }

    public static byte Suspend(this ref CBCore core, IntPtr context, double duration)
    {
      var suspendDelegate = Marshal.GetDelegateForFunctionPointer<SuspendDelegate>(core._suspend);
      return suspendDelegate(context, duration);
    }

    public static void CloneVar(this ref CBCore core, ref CBVar target, ref CBVar source)
    {
      var cloneVarDelegate = Marshal.GetDelegateForFunctionPointer<CloneVarDelegate>(core._cloneVar);
      cloneVarDelegate(ref target, ref source);
    }

    public static void DestroyVar(this ref CBCore core, IntPtr var)
    {
      var destroyVarDelegate = Marshal.GetDelegateForFunctionPointer<DestroyVarDelegate>(core._destroyVar);
      destroyVarDelegate(var);
    }
  }

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate IntPtr AllocDelegate(uint size);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void FreeDelegate(IntPtr ptr);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void LogDelegate(string message);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void LogLevelDelegate(int level, string message);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate CBNodeRef CreateNodeDelegate();

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void ScheduleDelegate(CBNodeRef nodeRef, CBChainRef chainRef);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void UnscheduleDelegate(CBNodeRef nodeRef, CBChainRef chainRef);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate CBBool TickDelegate(CBNodeRef nodeRef);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void SleepDelegate(double seconds, CBBool runCallbacks);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate IntPtr AllocExternalVariableDelegate(CBChainRef chainRef, string name);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void FreeExternalVariableDelegate(CBChainRef chainRef, string name);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate byte SuspendDelegate(IntPtr context, double duration);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void CloneVarDelegate(ref CBVar target, ref CBVar source);

  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  internal delegate void DestroyVarDelegate(IntPtr varRef);
}
