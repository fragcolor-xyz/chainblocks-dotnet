/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Diagnostics;
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
      var cbstr = (CBString)message;
      try
      {
        logDelegate(cbstr);
      }
      finally
      {
        cbstr.Dispose();
      }
    }

    public static void Log(this ref CBCore core, string message, CBLogLevel level)
    {
      var logLevelDelegate = Marshal.GetDelegateForFunctionPointer<LogLevelDelegate>(core._logLevel);
      var cbstr = (CBString)message;
      try
      {
        logLevelDelegate((int)level, cbstr);
      }
      finally
      {
        cbstr.Dispose();
      }
    }

    public static CBlockPtr CreateBlock(this ref CBCore core, string name)
    {
      var createBlockDelegate = Marshal.GetDelegateForFunctionPointer<CreateBlockDelegate>(core._createBlock);
      var cbstr = (CBString)name;
      try
      {
        return createBlockDelegate(cbstr);
      }
      finally
      {
        cbstr.Dispose();
      }
    }

    public static CBChainInfo GetChainInfo(this ref CBCore core, CBChainRef chainRef)
    {
      var getChainInfoDelegate = Marshal.GetDelegateForFunctionPointer<GetChainInfoDelegate>(core._getChainInfo);
      return getChainInfoDelegate(chainRef);
    }

    public static CBNodeRef CreateNode(this ref CBCore core)
    {
      var createNodeDelegate = Marshal.GetDelegateForFunctionPointer<CreateNodeDelegate>(core._createNode);
      var ptr = createNodeDelegate();
      Debug.Assert(ptr.IsValid());
      return ptr;
    }

    public static void DestroyNode(this ref CBCore core, CBNodeRef nodeRef)
    {
      var createNodeDelegate = Marshal.GetDelegateForFunctionPointer<DestroyNodeDelegate>(core._destroyNode);
      createNodeDelegate(nodeRef);
    }

    public static void Schedule(this ref CBCore core, CBNodeRef nodeRef, CBChainRef chainRef)
    {
      Debug.Assert(nodeRef.IsValid());
      Debug.Assert(chainRef.IsValid());
      var scheduleDelegate = Marshal.GetDelegateForFunctionPointer<ScheduleDelegate>(core._schedule);
      scheduleDelegate(nodeRef, chainRef);
    }

    public static void Unschedule(this ref CBCore core, CBNodeRef nodeRef, CBChainRef chainRef)
    {
      Debug.Assert(nodeRef.IsValid());
      Debug.Assert(chainRef.IsValid());
      var unscheduleDelegate = Marshal.GetDelegateForFunctionPointer<UnscheduleDelegate>(core._unschedule);
      unscheduleDelegate(nodeRef, chainRef);
    }

    public static bool Tick(this ref CBCore core, CBNodeRef nodeRef)
    {
      Debug.Assert(nodeRef.IsValid());
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
      Debug.Assert(chainRef.IsValid());
      var allocExternalVariableDelegate = Marshal.GetDelegateForFunctionPointer<AllocExternalVariableDelegate>(core._allocExternalVariable);
      var cbstr = (CBString)name;
      try
      {
        return allocExternalVariableDelegate(chainRef, cbstr);
      }
      finally
      {
        cbstr.Dispose();
      }
    }

    public static void FreeExternalVariable(this ref CBCore core, CBChainRef chainRef, string name)
    {
      Debug.Assert(chainRef.IsValid());
      var freeExternalVariableDelegate = Marshal.GetDelegateForFunctionPointer<FreeExternalVariableDelegate>(core._freeExternalVariable);
      var cbstr = (CBString)name;
      try
      {
        freeExternalVariableDelegate(chainRef, cbstr);
      }
      finally
      {
        cbstr.Dispose();
      }
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

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate IntPtr AllocDelegate(uint size);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void FreeDelegate(IntPtr ptr);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void LogDelegate(CBString message);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void LogLevelDelegate(int level, CBString message);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBlockPtr CreateBlockDelegate(CBString name);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBChainInfo GetChainInfoDelegate(CBChainRef chainRef);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBNodeRef CreateNodeDelegate();

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void DestroyNodeDelegate(CBNodeRef nodeRef);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void ScheduleDelegate(CBNodeRef nodeRef, CBChainRef chainRef);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void UnscheduleDelegate(CBNodeRef nodeRef, CBChainRef chainRef);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBBool TickDelegate(CBNodeRef nodeRef);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void SleepDelegate(double seconds, CBBool runCallbacks);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate IntPtr AllocExternalVariableDelegate(CBChainRef chainRef, CBString name);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void FreeExternalVariableDelegate(CBChainRef chainRef, CBString name);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void CloneVarDelegate(ref CBVar target, ref CBVar source);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void DestroyVarDelegate(IntPtr varRef);
}
