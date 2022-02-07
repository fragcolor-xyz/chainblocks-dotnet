/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBParameterInfo
  {
    internal IntPtr _name;
    internal CBOptionalString _help;
    internal CBTypesInfo _types;
  }
}
