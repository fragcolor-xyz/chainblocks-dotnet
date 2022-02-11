/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  internal static class NativeMethods
  {
    private const string Dll = "libcbl";
    internal const CallingConvention CallingConv = CallingConvention.Cdecl;

    [DllImport(Dll, CallingConvention = CallingConv)]
    internal static extern IntPtr chainblocksInterface(int version);

    [DllImport(Dll, CallingConvention = CallingConv)]
    internal static extern IntPtr cbLispCreate(CBString path);

    [DllImport(Dll, CallingConvention = CallingConv)]
    internal static extern void cbLispDestroy(IntPtr lisp);

    [DllImport(Dll, CallingConvention = CallingConv)]
    internal static extern CBBool cbLispEval(IntPtr lisp, CBString code, IntPtr output);
  }
}
