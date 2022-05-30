/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Threading;

namespace Fragcolor.Shards
{
  /// <summary>
  /// Represents the scripting environment.
  /// </summary>
  public sealed class ScriptingEnv : IDisposable
  {
    private int _disposeState;

    internal IntPtr _env;

    /// <summary>
    /// Initializes a new instance of <see cref="ScriptingEnv"/> with the default (empty) path.
    /// </summary>
    public ScriptingEnv()
      : this(string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ScriptingEnv"/> with the given path.
    /// </summary>
    /// <param name="path">The path that will be used as the working directory.</param>
    public ScriptingEnv(string path)
    {
      var cbstr = (SHString)path;
      try
      {
        _env = NativeMethods.shLispCreate(cbstr);
      }
      finally
      {
        cbstr.Dispose();
      }
    }

    /// <summary>
    /// Destructs the instance.
    /// </summary>
    /// <remarks>
    /// Consumers should call <see cref="Dispose()"/> instead to ensure realiable destruction.
    /// </remarks>
    ~ScriptingEnv()
    {
      Dispose(false);
    }

    /// <summary>
    /// Diposes this instance.
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Evaluates code and store its eventual output.
    /// </summary>
    /// <param name="code">The code to evaluate.</param>
    /// <param name="output">The adresse to a memory location where the output will be stored.</param>
    /// <returns><c>true</c> if the evaluation succeeded; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public bool Eval(string code, IntPtr output)
    {
      if (code is null) throw new ArgumentNullException(nameof(code));

      var cbstr = (SHString)code;
      try
      {
        return NativeMethods.shLispEval(_env, cbstr, output);
      }
      finally
      {
        cbstr.Dispose();
      }
    }

    private void Dispose(bool _)
    {
      if (Interlocked.CompareExchange(ref _disposeState, 1, 0) != 0) return;

      NativeMethods.shLispDestroy(_env);
      _env = IntPtr.Zero;
    }
  }
}
