/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Fragcolor.Shards.Collections;

namespace Fragcolor.Shards
{
  /// <summary>
  /// Extension methods for <see cref="Shard"/>.
  /// </summary>
  public static class ShardExtensions
  {
    /// <summary>
    /// Returns a pointer to this wire.
    /// </summary>
    /// <param name="shard">A reference to the wire.</param>
    /// <returns>A pointer to the wire.</returns>
    public static ShardPtr AsPointer(this ref Shard shard)
    {
      unsafe
      {
        var ptr = Unsafe.AsPointer(ref shard);
        return new() { _ref = (IntPtr)ptr };
      }
    }

    /// <summary>
    /// Reinterprets the <paramref name="ptr"/> as a reference to a <see cref="Shard"/>.
    /// </summary>
    /// <param name="ptr">The pointer whose value to reference.</param>
    /// <returns>A reference to the wire.</returns>
    public static ref Shard AsRef(this ShardPtr ptr)
    {
      unsafe
      {
        return ref Unsafe.AsRef<Shard>(ptr._ref.ToPointer());
      }
    }

    /// <summary>
    /// Retrieves information about the variables exposed by the wire.
    /// </summary>
    /// <param name="shard">A reference to the wire.</param>
    /// <returns>A struct containing information about the exposed variables.</returns>
    public static SHExposedTypesInfo ExposedVariables(this ref Shard shard)
    {
      var exposedVariablesDelegate = Marshal.GetDelegateForFunctionPointer<ShardExposedVariablesDelegate>(shard._exposedVariables);
      return exposedVariablesDelegate(ref shard);
    }

    /// <summary>
    /// Returns the hash of this wire.
    /// </summary>
    /// <param name="shard">A reference to the wire.</param>
    /// <returns>The hash of the wire.</returns>
    public static uint Hash(this ref Shard shard)
    {
      var hashDelegate = Marshal.GetDelegateForFunctionPointer<ShardHashDelegate>(shard._hash);
      return hashDelegate(ref shard);
    }

    /// <summary>
    /// Returns a description of the wire.
    /// </summary>
    /// <param name="shard">A reference to the wire.</param>
    /// <returns>An optional string describing the wire.</returns>
    public static SHOptionalString Help(this ref Shard shard)
    {
      var helpDelegate = Marshal.GetDelegateForFunctionPointer<ShardHelpDelegate>(shard._help);
      return helpDelegate(ref shard);
    }

    /// <summary>
    /// Returns a description of the inputs of the wire.
    /// </summary>
    /// <param name="shard">A reference to the wire.</param>
    /// <returns>An optional string describing the inputs of the wire.</returns>
    public static SHOptionalString InputHelp(this ref Shard shard)
    {
      var helpDelegate = Marshal.GetDelegateForFunctionPointer<ShardHelpDelegate>(shard._inputHelp);
      return helpDelegate(ref shard);
    }

    /// <summary>
    /// Retrieves information about the input types of the wire.
    /// </summary>
    /// <param name="shard">A reference to the wire.</param>
    /// <returns>A struct containing information about the input types.</returns>
    public static SHTypesInfo InputTypes(this ref Shard shard)
    {
      var inputTypesDelegate = Marshal.GetDelegateForFunctionPointer<ShardInputOutputTypesDelegate>(shard._inputTypes);
      return inputTypesDelegate(ref shard);
    }

    /// <summary>
    /// Returns the name of this wire.
    /// </summary>
    /// <param name="shard">A reference to the wire.</param>
    /// <returns>The name of the wire.</returns>
    public static string? Name(this ref Shard shard)
    {
      var nameDelegate = Marshal.GetDelegateForFunctionPointer<ShardNameDelegate>(shard._name);
      return (string?)nameDelegate(ref shard);
    }

    /// <summary>
    /// Returns a description of the outputs of the wire.
    /// </summary>
    /// <param name="shard">A reference to the wire.</param>
    /// <returns>An optional string describing the outputs of the wire.</returns>
    public static SHOptionalString OutputHelp(this ref Shard shard)
    {
      var helpDelegate = Marshal.GetDelegateForFunctionPointer<ShardHelpDelegate>(shard._outputHelp);
      return helpDelegate(ref shard);
    }

    /// <summary>
    /// Retreives information about the output types of the wire.
    /// </summary>
    /// <param name="shard">A reference to the wire.</param>
    /// <returns>A struct containing information about the output types.</returns>
    public static SHTypesInfo OutputTypes(this ref Shard shard)
    {
      var outputTypesDelegate = Marshal.GetDelegateForFunctionPointer<ShardInputOutputTypesDelegate>(shard._outputTypes);
      return outputTypesDelegate(ref shard);
    }

    /// <summary>
    /// Retrieves information about the parameters of the wire.
    /// </summary>
    /// <param name="shard">A reference to the wire.</param>
    /// <returns>A struct containing information about the parameters.</returns>
    public static SHParametersInfo Parameters(this ref Shard shard)
    {
      var parametersVariablesDelegate = Marshal.GetDelegateForFunctionPointer<ShardParametersDelegate>(shard._parameters);
      return parametersVariablesDelegate(ref shard);
    }

    /// <summary>
    /// Retreives information about the properties of a wire.
    /// </summary>
    /// <param name="shard">A reference to the wire.</param>
    /// <returns>A struct containing information about the properties.</returns>
    public static SHTable Properties(this ref Shard shard)
    {
      var propertiesDelegate = Marshal.GetDelegateForFunctionPointer<ShardPropertiesDelegate>(shard._properties);
      var ptr = propertiesDelegate(ref shard);
      if (ptr != IntPtr.Zero)
      {
        unsafe
        {
          return Unsafe.AsRef<SHTable>(ptr.ToPointer());
        }
      }
      return default;
    }

    /// <summary>
    /// Retreives information about the variables that are required for a wire.
    /// </summary>
    /// <param name="shard">A reference to the wire.</param>
    /// <returns>A struct containing information about the required variables.</returns>
    public static SHExposedTypesInfo RequiredVariables(this ref Shard shard)
    {
      var requiredVariablesDelegate = Marshal.GetDelegateForFunctionPointer<ShardRequiredVariablesDelegate>(shard._requiredVariables);
      return requiredVariablesDelegate(ref shard);
    }
  }

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate SHExposedTypesInfo ShardExposedVariablesDelegate(ref Shard shard);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate uint ShardHashDelegate(ref Shard shard);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate SHOptionalString ShardHelpDelegate(ref Shard shard);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate SHTypesInfo ShardInputOutputTypesDelegate(ref Shard shard);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate SHString ShardNameDelegate(ref Shard shard);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate SHParametersInfo ShardParametersDelegate(ref Shard shard);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate IntPtr ShardPropertiesDelegate(ref Shard shard);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate SHExposedTypesInfo ShardRequiredVariablesDelegate(ref Shard shard);
}
