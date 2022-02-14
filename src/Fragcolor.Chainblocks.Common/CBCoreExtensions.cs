/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  /// <summary>
  /// Extension methods for <see cref="CBCore"/>.
  /// </summary>
  public static class CBCoreExtensions
  {
    /// <summary>
    /// Allocates a block of memory of the specified <paramref name="size"/>.
    /// This memory must be released with <see cref="Free(ref CBCore, IntPtr)"/>.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="size">The amount of memory to be allocated.</param>
    /// <returns>A pointer to the allocated memory.</returns>
    public static IntPtr Alloc(this ref CBCore core, uint size)
    {
      var allocDelegate = Marshal.GetDelegateForFunctionPointer<AllocDelegate>(core._alloc);
      return allocDelegate(size);
    }

    /// <summary>
    /// Frees a block of memory previously allocated with <see cref="Alloc(ref CBCore, uint)"/>.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="ptr">A pointer to the allocated memory.</param>
    public static void Free(this ref CBCore core, IntPtr ptr)
    {
      var freeDelegate = Marshal.GetDelegateForFunctionPointer<FreeDelegate>(core._free);
      freeDelegate(ptr);
    }

    /// <summary>
    /// Logs a message at the default level (<see cref="CBLogLevel.Info"/>).
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="message">The message to log.</param>
    /// <seealso cref="Log(ref CBCore, string, CBLogLevel)"/>
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

    /// <summary>
    /// Logs a message at the specified level.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="level">The log level to use.</param>
    /// <seealso cref="Log(ref CBCore, string)"/>
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

    /// <summary>
    /// Creates a new block with the specified <paramref name="name"/>.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="name"></param>
    /// <returns>A pointer to the newly created block; or <c>null</c>, if no such block exists with the specified name.</returns>
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

    /// <summary>
    /// Retrieves information about an existing chain.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="chainRef">A reference to the chain.</param>
    /// <returns>A struct containing information about the chain.</returns>
    public static CBChainInfo GetChainInfo(this ref CBCore core, CBChainRef chainRef)
    {
      var getChainInfoDelegate = Marshal.GetDelegateForFunctionPointer<GetChainInfoDelegate>(core._getChainInfo);
      return getChainInfoDelegate(chainRef);
    }

    /// <summary>
    /// Creates a new node that can be used to scheduled a chain, as well as tick the execution.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <returns>A reference to the node. It must be released with <see cref="DestroyNode(ref CBCore, CBNodeRef)"/>.</returns>
    /// <seealso cref="Schedule(ref CBCore, CBNodeRef, CBChainRef)"/>
    /// <seealso cref="Tick(ref CBCore, CBNodeRef)"/>
    public static CBNodeRef CreateNode(this ref CBCore core)
    {
      var createNodeDelegate = Marshal.GetDelegateForFunctionPointer<CreateNodeDelegate>(core._createNode);
      var ptr = createNodeDelegate();
      Debug.Assert(ptr.IsValid());
      return ptr;
    }

    /// <summary>
    /// Destroys a node, previously created with <see cref="CreateNode(ref CBCore)"/>.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="nodeRef">A reference to the node.</param>
    public static void DestroyNode(this ref CBCore core, CBNodeRef nodeRef)
    {
      var createNodeDelegate = Marshal.GetDelegateForFunctionPointer<DestroyNodeDelegate>(core._destroyNode);
      createNodeDelegate(nodeRef);
    }

    /// <summary>
    /// Schedules a chain to be executed on a specified node.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="nodeRef">A reference to the node.</param>
    /// <param name="chainRef">A reference to the chain.</param>
    /// <seealso cref="Unschedule(ref CBCore, CBNodeRef, CBChainRef)"/>
    public static void Schedule(this ref CBCore core, CBNodeRef nodeRef, CBChainRef chainRef)
    {
      Debug.Assert(nodeRef.IsValid());
      Debug.Assert(chainRef.IsValid());
      var scheduleDelegate = Marshal.GetDelegateForFunctionPointer<ScheduleDelegate>(core._schedule);
      scheduleDelegate(nodeRef, chainRef);
    }

    /// <summary>
    /// Unschedules a chain from a specified node.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="nodeRef">A reference to the node.</param>
    /// <param name="chainRef">A reference to the chain.</param>
    /// <seealso cref="Schedule(ref CBCore, CBNodeRef, CBChainRef)"/>
    public static void Unschedule(this ref CBCore core, CBNodeRef nodeRef, CBChainRef chainRef)
    {
      Debug.Assert(nodeRef.IsValid());
      Debug.Assert(chainRef.IsValid());
      var unscheduleDelegate = Marshal.GetDelegateForFunctionPointer<UnscheduleDelegate>(core._unschedule);
      unscheduleDelegate(nodeRef, chainRef);
    }

    /// <summary>
    /// Ticks a node to advance any chain that is currently scheduled on it.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="nodeRef">A reference to the node.</param>
    /// <returns><c>true</c> if node was ticked successfully; otherwise <c>false</c>.</returns>
    public static bool Tick(this ref CBCore core, CBNodeRef nodeRef)
    {
      Debug.Assert(nodeRef.IsValid());
      var tickDelegate = Marshal.GetDelegateForFunctionPointer<TickDelegate>(core._tick);
      return tickDelegate(nodeRef);
    }

    /// <summary>
    /// Suspends the current thread for the specified amount of time.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="seconds">The amount of time for which the thread is suspended.</param>
    /// <param name="runCallbacks">TBD</param>
    public static void Sleep(this ref CBCore core, double seconds, bool runCallbacks)
    {
      var sleepDelegate = Marshal.GetDelegateForFunctionPointer<SleepDelegate>(core._sleep);
      sleepDelegate(seconds, runCallbacks);
    }

    /// <summary>
    /// Allocates an etxernal variable on a chain with the specified name.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="chainRef">A reference to the chain.</param>
    /// <param name="name">The name of the external variable.</param>
    /// <returns>A pointer to the allocated variable.</returns>
    /// <seealso cref="ExternalVariable"/>
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

    /// <summary>
    /// Frees an external variable with the specified name.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="chainRef">A reference to the chain.</param>
    /// <param name="name">The name of the external variable.</param>
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

    /// <summary>
    /// Clones a <paramref name="source"/> variable to a <paramref name="target"/> variable.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="target">A reference to the target variable.</param>
    /// <param name="source">A reference to the source variable.</param>
    /// <seealso cref="ExternalVariable.Clone()"/>
    /// <seealso cref="Variable.Clone()"/>
    public static void CloneVar(this ref CBCore core, ref CBVar target, ref CBVar source)
    {
      var cloneVarDelegate = Marshal.GetDelegateForFunctionPointer<CloneVarDelegate>(core._cloneVar);
      cloneVarDelegate(ref target, ref source);
    }

    /// <summary>
    /// Destroys the payload of a variable.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="var">A pointer to the variable.</param>
    /// <remarks>
    /// This is usually called before freeing the variable itself with <see cref="Free(ref CBCore, IntPtr)"/>.
    /// </remarks>
    /// <seealso cref="Variable.Dispose()"/>
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
