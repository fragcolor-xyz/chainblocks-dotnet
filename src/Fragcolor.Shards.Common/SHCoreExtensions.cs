/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Fragcolor.Shards
{
  /// <summary>
  /// Extension methods for <see cref="SHCore"/>.
  /// </summary>
  public static class SHCoreExtensions
  {
    /// <summary>
    /// Allocates a wire of memory of the specified <paramref name="size"/>.
    /// This memory must be released with <see cref="Free(ref SHCore, IntPtr)"/>.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="size">The amount of memory to be allocated.</param>
    /// <returns>A pointer to the allocated memory.</returns>
    public static IntPtr Alloc(this ref SHCore core, uint size)
    {
      var allocDelegate = Marshal.GetDelegateForFunctionPointer<AllocDelegate>(core._alloc);
      return allocDelegate(size);
    }

    /// <summary>
    /// Frees a wire of memory previously allocated with <see cref="Alloc(ref SHCore, uint)"/>.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="ptr">A pointer to the allocated memory.</param>
    public static void Free(this ref SHCore core, IntPtr ptr)
    {
      var freeDelegate = Marshal.GetDelegateForFunctionPointer<FreeDelegate>(core._free);
      freeDelegate(ptr);
    }

    /// <summary>
    /// Logs a message at the default level (<see cref="SHLogLevel.Info"/>).
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="message">The message to log.</param>
    /// <seealso cref="Log(ref SHCore, string, SHLogLevel)"/>
    public static void Log(this ref SHCore core, string message)
    {
      var logDelegate = Marshal.GetDelegateForFunctionPointer<LogDelegate>(core._log);
      var cbstr = (SHString)message;
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
    /// <seealso cref="Log(ref SHCore, string)"/>
    public static void Log(this ref SHCore core, string message, SHLogLevel level)
    {
      var logLevelDelegate = Marshal.GetDelegateForFunctionPointer<LogLevelDelegate>(core._logLevel);
      var cbstr = (SHString)message;
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
    /// Creates a new wire with the specified <paramref name="name"/>.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="name">The name of the wire to create.</param>
    /// <returns>A pointer to the newly created wire; or <c>null</c>, if no such wire exists with the specified name.</returns>
    public static ShardPtr CreateShard(this ref SHCore core, string name)
    {
      var createShardDelegate = Marshal.GetDelegateForFunctionPointer<CreateShardDelegate>(core._createShard);
      var cbstr = (SHString)name;
      try
      {
        return createShardDelegate(cbstr);
      }
      finally
      {
        cbstr.Dispose();
      }
    }

    /// <summary>
    /// Stops a running wire.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="wireRef">A reference to the wire.</param>
    /// <returns>A variable with the output of the wire if it had already completed; otherwise, an empty variable.</returns>
    public static SHVar StopWire(this ref SHCore core, SHWireRef wireRef)
    {
      var stopWireDelegate = Marshal.GetDelegateForFunctionPointer<StopWireDelegate>(core._stopWire);
      return stopWireDelegate(wireRef);
    }

    /// <summary>
    /// Retrieves information about an existing wire.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="wireRef">A reference to the wire.</param>
    /// <returns>A struct containing information about the wire.</returns>
    public static SHWireInfo GetWireInfo(this ref SHCore core, SHWireRef wireRef)
    {
      var getWireInfoDelegate = Marshal.GetDelegateForFunctionPointer<GetWireInfoDelegate>(core._getWireInfo);
      return getWireInfoDelegate(wireRef);
    }

    /// <summary>
    /// Creates a new mesh that can be used to scheduled a wire, as well as tick the execution.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <returns>A reference to the mesh. It must be released with <see cref="DestroyMesh"/>.</returns>
    /// <seealso cref="Schedule(ref SHCore, SHMeshRef, SHWireRef)"/>
    /// <seealso cref="Tick(ref SHCore, SHMeshRef)"/>
    public static SHMeshRef CreateMesh(this ref SHCore core)
    {
      var createMeshDelegate = Marshal.GetDelegateForFunctionPointer<CreateMeshDelegate>(core._createMesh);
      var ptr = createMeshDelegate();
      Debug.Assert(ptr.IsValid());
      return ptr;
    }

    /// <summary>
    /// Destroys a mesh, previously created with <see cref="CreateMesh"/>.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="meshRef">A reference to the mesh.</param>
    public static void DestroyMesh(this ref SHCore core, SHMeshRef meshRef)
    {
      var createMeshDelegate = Marshal.GetDelegateForFunctionPointer<DestroyMeshDelegate>(core._destroyMesh);
      createMeshDelegate(meshRef);
    }

    /// <summary>
    /// Schedules a wire to be executed on a specified mesh.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="meshRef">A reference to the mesh.</param>
    /// <param name="wireRef">A reference to the wire.</param>
    /// <seealso cref="Unschedule(ref SHCore, SHMeshRef, SHWireRef)"/>
    public static void Schedule(this ref SHCore core, SHMeshRef meshRef, SHWireRef wireRef)
    {
      Debug.Assert(meshRef.IsValid());
      Debug.Assert(wireRef.IsValid());
      var scheduleDelegate = Marshal.GetDelegateForFunctionPointer<ScheduleDelegate>(core._schedule);
      scheduleDelegate(meshRef, wireRef);
    }

    /// <summary>
    /// Unschedules a wire from a specified mesh.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="meshRef">A reference to the mesh.</param>
    /// <param name="wireRef">A reference to the wire.</param>
    /// <seealso cref="Schedule(ref SHCore, SHMeshRef, SHWireRef)"/>
    public static void Unschedule(this ref SHCore core, SHMeshRef meshRef, SHWireRef wireRef)
    {
      Debug.Assert(meshRef.IsValid());
      Debug.Assert(wireRef.IsValid());
      var unscheduleDelegate = Marshal.GetDelegateForFunctionPointer<UnscheduleDelegate>(core._unschedule);
      unscheduleDelegate(meshRef, wireRef);
    }

    /// <summary>
    /// Ticks a mesh to advance any wire that is currently scheduled on it.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="meshRef">A reference to the mesh.</param>
    /// <returns><c>true</c> if mesh was ticked successfully; otherwise <c>false</c>.</returns>
    public static bool Tick(this ref SHCore core, SHMeshRef meshRef)
    {
      Debug.Assert(meshRef.IsValid());
      var tickDelegate = Marshal.GetDelegateForFunctionPointer<TickDelegate>(core._tick);
      return tickDelegate(meshRef);
    }

    /// <summary>
    /// Suspends the current thread for the specified amount of time.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="seconds">The amount of time for which the thread is suspended.</param>
    /// <param name="runCallbacks">TBD</param>
    public static void Sleep(this ref SHCore core, double seconds, bool runCallbacks)
    {
      var sleepDelegate = Marshal.GetDelegateForFunctionPointer<SleepDelegate>(core._sleep);
      sleepDelegate(seconds, runCallbacks);
    }

    /// <summary>
    /// Allocates an etxernal variable on a wire with the specified name.
    /// </summary>
    /// <param name="core">A reference to the core struct.</param>
    /// <param name="wireRef">A reference to the wire.</param>
    /// <param name="name">The name of the external variable.</param>
    /// <returns>A pointer to the allocated variable.</returns>
    /// <seealso cref="ExternalVariable"/>
    public static IntPtr AllocExternalVariable(this ref SHCore core, SHWireRef wireRef, string name)
    {
      Debug.Assert(wireRef.IsValid());
      var allocExternalVariableDelegate = Marshal.GetDelegateForFunctionPointer<AllocExternalVariableDelegate>(core._allocExternalVariable);
      var cbstr = (SHString)name;
      try
      {
        return allocExternalVariableDelegate(wireRef, cbstr);
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
    /// <param name="wireRef">A reference to the wire.</param>
    /// <param name="name">The name of the external variable.</param>
    public static void FreeExternalVariable(this ref SHCore core, SHWireRef wireRef, string name)
    {
      Debug.Assert(wireRef.IsValid());
      var freeExternalVariableDelegate = Marshal.GetDelegateForFunctionPointer<FreeExternalVariableDelegate>(core._freeExternalVariable);
      var cbstr = (SHString)name;
      try
      {
        freeExternalVariableDelegate(wireRef, cbstr);
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
    public static void CloneVar(this ref SHCore core, ref SHVar target, ref SHVar source)
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
    /// This is usually called before freeing the variable itself with <see cref="Free(ref SHCore, IntPtr)"/>.
    /// </remarks>
    /// <seealso cref="Variable.Dispose()"/>
    public static void DestroyVar(this ref SHCore core, IntPtr var)
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
  internal delegate void LogDelegate(SHString message);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void LogLevelDelegate(int level, SHString message);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate ShardPtr CreateShardDelegate(SHString name);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate SHVar StopWireDelegate(SHWireRef wireRef);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate SHWireInfo GetWireInfoDelegate(SHWireRef wireRef);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate SHMeshRef CreateMeshDelegate();

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void DestroyMeshDelegate(SHMeshRef meshRef);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void ScheduleDelegate(SHMeshRef meshRef, SHWireRef wireRef);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void UnscheduleDelegate(SHMeshRef meshRef, SHWireRef wireRef);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate SHBool TickDelegate(SHMeshRef meshRef);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void SleepDelegate(double seconds, SHBool runCallbacks);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate IntPtr AllocExternalVariableDelegate(SHWireRef wireRef, SHString name);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void FreeExternalVariableDelegate(SHWireRef wireRef, SHString name);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void CloneVarDelegate(ref SHVar target, ref SHVar source);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate void DestroyVarDelegate(IntPtr varRef);
}
