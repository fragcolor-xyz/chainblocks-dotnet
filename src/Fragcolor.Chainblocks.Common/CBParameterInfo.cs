/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

using Fragcolor.Chainblocks.Collections;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBParameterInfo
  {
    //! Native struct, don't edit
    internal IntPtr _name;
    internal CBOptionalString _help;
    internal CBTypesInfo _types;
  }
}
