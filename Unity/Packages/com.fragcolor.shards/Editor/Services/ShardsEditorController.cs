/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using System.Threading;

using UnityEditor;

namespace Fragcolor.Shards.UnityEditor.Services
{
  /// <summary>
  /// Implements a controller running in the editor.
  /// </summary>
  internal static class ShardsEditorController
  {
    private static ScriptingEnv? _env;
    private static Mesh? _mesh;
    private static int _initializedState;
    private static bool _isRunning;

    /// <summary>
    /// Gets whether the controller has been initialized.
    /// </summary>
    internal static bool IsInitialized
    {
      get { return Volatile.Read(ref _initializedState) != 0; }
    }

    internal static ScriptingEnv Env
    {
      get
      {
        EnsureInitialize();
        return _env!;
      }
    }

    internal static Mesh Mesh
    {
      get
      {
        EnsureInitialize();
        return _mesh!;
      }
    }

    internal static void EnsureInitialize()
    {
      if (Interlocked.CompareExchange(ref _initializedState, 1, 0) != 0)
        return;

      _env = new ScriptingEnv();
      _mesh = new Mesh();
    }

    /// <summary>
    /// Starts processing updates and ticking the node.
    /// </summary>
    internal static void Start()
    {
      if (_isRunning) return;

      EnsureInitialize();

      EditorApplication.update += OnUpdate;
      _isRunning = true;
    }

    /// <summary>
    /// Stops processing updates and ticking the node.
    /// </summary>
    internal static void Pause()
    {
      if (!_isRunning) return;

      EditorApplication.update -= OnUpdate;
      _isRunning = false;
    }

    /// <summary>
    /// Shutdowns the underlying shards environment.
    /// </summary>
    internal static void Shutdown()
    {
      Pause();

      if (Interlocked.CompareExchange(ref _initializedState, 0, 1) != 1)
        return;

      _mesh?.Dispose();
      _env?.Dispose();
      _env = null;
    }

    private static void OnUpdate()
    {
      Mesh.Tick();
    }
  }
}
