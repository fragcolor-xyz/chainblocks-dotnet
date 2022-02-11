/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.CompilerServices;

using Fragcolor.Chainblocks.Collections;

namespace Fragcolor.Chainblocks
{
  /// <summary>
  /// Extension methods for <see cref="CBParameterInfo"/>.
  /// </summary>
  public static class CBParameterInfoExtensions
  {
    /// <summary>
    /// Gets the name of the parameter.
    /// </summary>
    /// <param name="info">A reference to the parameter info.</param>
    /// <returns>A string representing the name of the parameter.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? Name(this ref CBParameterInfo info)
    {
      return (string?)info._name;
    }

    /// <summary>
    /// Gets the supported types of the parameter.
    /// </summary>
    /// <param name="info">A reference to the parameter info.</param>
    /// <returns>A collection of all the possible types supported for the parameter.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref CBTypesInfo Types(this ref CBParameterInfo info)
    {
      return ref info._types;
    }
  }
}
