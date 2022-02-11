/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Fragcolor.Chainblocks
{
  /// <summary>
  /// Main entry point to native chainblocks.
  /// </summary>
  public static class Native
  {
    private static CBCore _core;
    private static IntPtr _corePtr;

    private static readonly object _syncLock = new();

    /// <summary>
    /// Gets the core struct.
    /// </summary>
    /// <remarks>
    /// Before accessing this property, the env must be initialized (see <see cref="LispEnv"/>).
    /// </remarks>
    public static ref CBCore Core
    {
      get
      {
        if (Volatile.Read(ref _corePtr) != IntPtr.Zero)
        {
          return ref _core;
        }

        lock (_syncLock)
        {
          if (Volatile.Read(ref _corePtr) == IntPtr.Zero)
          {
            var ptr = NativeMethods.chainblocksInterface(0x20200101);
            _core = Marshal.PtrToStructure<CBCore>(ptr);
            Volatile.Write(ref _corePtr, ptr);
          }
        }

        return ref _core;
      }
    }
  }
}
