/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  /// <summary>
  /// Represents type information about an exposed or required variable of a <see cref="CBlock"/>.
  /// </summary>
  /// <seealso cref="Collections.CBExposedTypesInfo"/>
  /// <seealso cref="CBlockExtensions.ExposedVariables(ref CBlock)"/>
  /// <seealso cref="CBlockExtensions.RequiredVariables(ref CBlock)"/>
  [StructLayout(LayoutKind.Sequential)]
  public struct CBExposedTypeInfo
  {
    //! Native struct, don't edit
    internal CBString _name;
    internal CBOptionalString _help;
    internal CBTypeInfo _exposedType;
    internal CBBool _mutable;
    internal CBBool _protected;
    internal CBBool _tableEntry;
    internal CBBool _global;
    internal CBChainRef _scope;
  }
}
