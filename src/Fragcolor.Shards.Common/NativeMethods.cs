/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Shards
{
  /// <summary>
  /// Native methods defined externally in the libcbl library.
  /// </summary>
  /// <remarks>
  /// Names are case sensitive and must match the exported symbols.
  /// </remarks>
  internal static class NativeMethods
  {
    private const string Dll = "libshards";
    internal const CallingConvention CallingConv = CallingConvention.Cdecl;

    /// <summary>
    /// Initializes and returns the core struct.
    /// </summary>
    /// <param name="version"></param>
    /// <returns>A pointer to the core struct.</returns>
    /// <remarks>
    /// The env must be initialized first with <see cref="shLispCreate"/>
    /// </remarks>
    [DllImport(Dll, CallingConvention = CallingConv)]
    internal static extern IntPtr shardsInterface(int version);

    /// <summary>
    /// Initializes the scripting environment.
    /// </summary>
    /// <param name="path"></param>
    /// <returns>A pointer to the scripting environment.</returns>
    /// <remarks>
    /// Must be released after use by calling <see cref="shLispDestroy"/>
    /// </remarks>
    [DllImport(Dll, CallingConvention = CallingConv)]
    internal static extern IntPtr shLispCreate(SHString path);

    /// <summary>
    /// Destroys a scripting environment, previously created with <see cref="shLispCreate"/>.
    /// </summary>
    /// <param name="env">A pointer to the scripting environment.</param>
    [DllImport(Dll, CallingConvention = CallingConv)]
    internal static extern void shLispDestroy(IntPtr env);

    /// <summary>
    /// Evaluates code on the provided scripting environment.
    /// </summary>
    /// <param name="env">A pointer to the scripting environment.</param>
    /// <param name="code">The code to evaluate.</param>
    /// <param name="output">A pointer to store the output, if any.</param>
    /// <returns>A byte indicating whether the evaluation was successful (<c>1</c>), or failed (<c>0</c>).</returns>
    [DllImport(Dll, CallingConvention = CallingConv)]
    internal static extern SHBool shLispEval(IntPtr env, SHString code, IntPtr output);
  }
}
