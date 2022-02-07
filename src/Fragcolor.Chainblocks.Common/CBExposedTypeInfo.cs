/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBExposedTypeInfo
  {
    internal IntPtr _name;
    internal CBOptionalString _help;
    internal CBTypeInfo _exposedType;
    internal CBBool _mutable;
    internal CBBool _protected;
    internal CBBool _tableEntry;
    internal CBBool _global;
    internal CBChainRef _scope;
  }
}
