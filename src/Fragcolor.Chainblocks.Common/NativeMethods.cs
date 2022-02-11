/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  /// <summary>
  /// Native methods defined externally on the libcbl library.
  /// </summary>
  /// <remarks>
  /// Names are case sensitive and must match the exported symbols.
  /// </remarks>
  internal static class NativeMethods
  {
    private const string Dll = "libcbl";
    internal const CallingConvention CallingConv = CallingConvention.Cdecl;

    /// <summary>
    /// Initializes and returns the core struct.
    /// </summary>
    /// <param name="version"></param>
    /// <returns>A pointer to the core struct.</returns>
    /// <remarks>
    /// The env must be initialized first with <see cref="cbLispCreate(CBString)"/>
    /// </remarks>
    [DllImport(Dll, CallingConvention = CallingConv)]
    internal static extern IntPtr chainblocksInterface(int version);

    /// <summary>
    /// Initializes the LISP environment.
    /// </summary>
    /// <param name="path"></param>
    /// <returns>A pointer to the LISP environment.</returns>
    /// <remarks>
    /// Must be released after use by calling <see cref="cbLispDestroy(IntPtr)"/>
    /// </remarks>
    [DllImport(Dll, CallingConvention = CallingConv)]
    internal static extern IntPtr cbLispCreate(CBString path);

    /// <summary>
    /// Destroys a LISP environment, previously created with <see cref="cbLispCreate(CBString)"/>.
    /// </summary>
    /// <param name="lisp">A pointer to the LISP environment.</param>
    [DllImport(Dll, CallingConvention = CallingConv)]
    internal static extern void cbLispDestroy(IntPtr lisp);

    /// <summary>
    /// Evaluates code on the provided LISP environment.
    /// </summary>
    /// <param name="lisp">A pointer to the LISP environment.</param>
    /// <param name="code">The code to evaluate.</param>
    /// <param name="output">A pointer to store the output, if any.</param>
    /// <returns>A byte indicating whether the evaluation was successful (<c>1</c>), or failed (<c>0</c>).</returns>
    [DllImport(Dll, CallingConvention = CallingConv)]
    internal static extern CBBool cbLispEval(IntPtr lisp, CBString code, IntPtr output);
  }
}
