/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

using Fragcolor.Chainblocks.Collections;

namespace Fragcolor.Chainblocks
{
  /// <summary>
  /// Represents information about the parameter of a <seealso cref="CBlock"/>.
  /// </summary>
  /// <remarks>
  /// See <see cref="CBParameterInfoExtensions"/> for available methods on this struct.
  /// </remarks>
  /// <seealso cref="CBParametersInfo"/>
  /// <seealso cref="CBlockExtensions.Parameters(ref CBlock)"/>
  [StructLayout(LayoutKind.Sequential)]
  public struct CBParameterInfo
  {
    //! Native struct, don't edit
    internal CBString _name;
    internal CBOptionalString _help;
    internal CBTypesInfo _types;
  }
}
