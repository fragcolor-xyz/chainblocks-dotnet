/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

namespace Fragcolor.Shards
{
  /// <summary>
  /// Represents type information about an exposed or required variable of a <see cref="Shard"/>.
  /// </summary>
  /// <seealso cref="Collections.SHExposedTypesInfo"/>
  /// <seealso cref="ShardExtensions.ExposedVariables(ref Shard)"/>
  /// <seealso cref="ShardExtensions.RequiredVariables(ref Shard)"/>
  [StructLayout(LayoutKind.Sequential)]
  public struct SHExposedTypeInfo
  {
    //! Native struct, don't edit
    internal SHString _name;
    internal SHOptionalString _help;
    internal SHTypeInfo _exposedType;
    internal SHBool _mutable;
    internal SHBool _protected;
    internal SHBool _tableEntry;
    internal SHBool _global;
    internal SHBool _pushTable;
  }
}
