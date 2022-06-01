/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;
using Fragcolor.Shards.Collections;

namespace Fragcolor.Shards
{
  /// <summary>
  /// Represents information about the parameter of a <seealso cref="Shard"/>.
  /// </summary>
  /// <remarks>
  /// See <see cref="SHParameterInfoExtensions"/> for available methods on this struct.
  /// </remarks>
  /// <seealso cref="SHParametersInfo"/>
  /// <seealso cref="ShardExtensions.Parameters(ref Shard)"/>
  [StructLayout(LayoutKind.Sequential)]
  public struct SHParameterInfo
  {
    //! Native struct, don't edit
    internal SHString _name;
    internal SHOptionalString _help;
    internal SHTypesInfo _types;
  }
}
