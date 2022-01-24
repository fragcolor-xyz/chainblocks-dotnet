/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using System.Threading;

using UnityEditor;

namespace Fragcolor.Chainblocks.UnityEditor.Settings
{
  /// <summary>
  /// Main class for initializing Chainblocks integration in the Unity editor.
  /// </summary>
  [InitializeOnLoad]
  internal static class GlobalInitialization
  {
    /// <summary>
    /// Initialization state:
    /// 0 = unintialized
    /// 1 = initialized
    /// </summary>
    private static int _initializedState;

    static GlobalInitialization()
    {
      InitializeGlobalState();
    }

    /// <summary>
    /// Indicates whether the initialization has been done.
    /// </summary>
    internal static bool IsInitialized
    {
      get { return Volatile.Read(ref _initializedState) != 0; }
    }

    /// <summary>
    /// Initializes the global state.
    /// </summary>
    /// <remarks>
    /// If <see cref="IsInitialized"/> is already <c>true</c>, this method does nothing.
    /// </remarks>
    /// <seealso cref="ShutdownGlobalState"/>
    internal static void InitializeGlobalState()
    {
      if (Interlocked.CompareExchange(ref _initializedState, 1, 0) != 0)
        return;

      ChainblocksManager.InitializeGlobalState();
    }

    /// <summary>
    /// Uninitializes the global state.
    /// </summary>
    /// <remarks>
    /// If <see cref="IsInitialized"/> is already <c>false</c>, this method does nothing.
    /// </remarks>
    /// <seealso cref="InitializeGlobalState"/>
    internal static void ShutdownGlobalState()
    {
      if (Interlocked.CompareExchange(ref _initializedState, 0, 1) != 1)
        return;

      ChainblocksManager.ShutdownGlobalState();
    }
  }
}
