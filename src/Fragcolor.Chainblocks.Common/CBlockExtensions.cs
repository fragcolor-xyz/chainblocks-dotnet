/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Fragcolor.Chainblocks.Collections;

namespace Fragcolor.Chainblocks
{
  /// <summary>
  /// Extension methods for <see cref="CBlock"/>.
  /// </summary>
  public static class CBlockExtensions
  {
    /// <summary>
    /// Returns a pointer to this block.
    /// </summary>
    /// <param name="block">A reference to the block.</param>
    /// <returns>A pointer to the block.</returns>
    public static CBlockPtr AsPointer(this ref CBlock block)
    {
      unsafe
      {
        var ptr = Unsafe.AsPointer(ref block);
        return new() { _ref = (IntPtr)ptr };
      }
    }

    /// <summary>
    /// Reinterprets the <paramref name="ptr"/> as a reference to a <see cref="CBlock"/>.
    /// </summary>
    /// <param name="ptr">The pointer whose value to reference.</param>
    /// <returns>A reference to the block.</returns>
    public static ref CBlock AsRef(this CBlockPtr ptr)
    {
      unsafe
      {
        return ref Unsafe.AsRef<CBlock>(ptr._ref.ToPointer());
      }
    }

    /// <summary>
    /// Retrieves information about the variables exposed by the block.
    /// </summary>
    /// <param name="block">A reference to the block.</param>
    /// <returns>A struct containing information about the exposed variables.</returns>
    public static CBExposedTypesInfo ExposedVariables(this ref CBlock block)
    {
      var exposedVariablesDelegate = Marshal.GetDelegateForFunctionPointer<BlockExposedVariablesDelegate>(block._exposedVariables);
      return exposedVariablesDelegate(ref block);
    }

    /// <summary>
    /// Returns the hash of this block.
    /// </summary>
    /// <param name="block">A reference to the block.</param>
    /// <returns>The hash of the block.</returns>
    public static uint Hash(this ref CBlock block)
    {
      var hashDelegate = Marshal.GetDelegateForFunctionPointer<BlockHashDelegate>(block._hash);
      return hashDelegate(ref block);
    }

    /// <summary>
    /// Returns a description of the block.
    /// </summary>
    /// <param name="block">A reference to the block.</param>
    /// <returns>An optional string describing the block.</returns>
    public static CBOptionalString Help(this ref CBlock block)
    {
      var helpDelegate = Marshal.GetDelegateForFunctionPointer<BlockHelpDelegate>(block._help);
      return helpDelegate(ref block);
    }

    /// <summary>
    /// Returns a description of the inputs of the block.
    /// </summary>
    /// <param name="block">A reference to the block.</param>
    /// <returns>An optional string describing the inputs of the block.</returns>
    public static CBOptionalString InputHelp(this ref CBlock block)
    {
      var helpDelegate = Marshal.GetDelegateForFunctionPointer<BlockHelpDelegate>(block._inputHelp);
      return helpDelegate(ref block);
    }

    /// <summary>
    /// Retrieves information about the input types of the block.
    /// </summary>
    /// <param name="block">A reference to the block.</param>
    /// <returns>A struct containing information about the input types.</returns>
    public static CBTypesInfo InputTypes(this ref CBlock block)
    {
      var inputTypesDelegate = Marshal.GetDelegateForFunctionPointer<BlockInputOutputTypesDelegate>(block._inputTypes);
      return inputTypesDelegate(ref block);
    }

    /// <summary>
    /// Returns the name of this block.
    /// </summary>
    /// <param name="block">A reference to the block.</param>
    /// <returns>The name of the block.</returns>
    public static string? Name(this ref CBlock block)
    {
      var nameDelegate = Marshal.GetDelegateForFunctionPointer<BlockNameDelegate>(block._name);
      return (string?)nameDelegate(ref block);
    }

    /// <summary>
    /// Returns a description of the outputs of the block.
    /// </summary>
    /// <param name="block">A reference to the block.</param>
    /// <returns>An optional string describing the outputs of the block.</returns>
    public static CBOptionalString OutputHelp(this ref CBlock block)
    {
      var helpDelegate = Marshal.GetDelegateForFunctionPointer<BlockHelpDelegate>(block._outputHelp);
      return helpDelegate(ref block);
    }

    /// <summary>
    /// Retreives information about the output types of the block.
    /// </summary>
    /// <param name="block">A reference to the block.</param>
    /// <returns>A struct containing information about the output types.</returns>
    public static CBTypesInfo OutputTypes(this ref CBlock block)
    {
      var outputTypesDelegate = Marshal.GetDelegateForFunctionPointer<BlockInputOutputTypesDelegate>(block._outputTypes);
      return outputTypesDelegate(ref block);
    }

    /// <summary>
    /// Retrieves information about the parameters of the block.
    /// </summary>
    /// <param name="block">A reference to the block.</param>
    /// <returns>A struct containing information about the parameters.</returns>
    public static CBParametersInfo Parameters(this ref CBlock block)
    {
      var parametersVariablesDelegate = Marshal.GetDelegateForFunctionPointer<BlockParametersDelegate>(block._parameters);
      return parametersVariablesDelegate(ref block);
    }

    /// <summary>
    /// Retreives information about the properties of a block.
    /// </summary>
    /// <param name="block">A reference to the block.</param>
    /// <returns>A struct containing information about the properties.</returns>
    public static CBTable Properties(this ref CBlock block)
    {
      var propertiesDelegate = Marshal.GetDelegateForFunctionPointer<BlockPropertiesDelegate>(block._properties);
      var ptr = propertiesDelegate(ref block);
      if (ptr != IntPtr.Zero)
      {
        unsafe
        {
          return Unsafe.AsRef<CBTable>(ptr.ToPointer());
        }
      }
      return default;
    }

    /// <summary>
    /// Retreives information about the variables that are required for a block.
    /// </summary>
    /// <param name="block">A reference to the block.</param>
    /// <returns>A struct containing information about the required variables.</returns>
    public static CBExposedTypesInfo RequiredVariables(this ref CBlock block)
    {
      var requiredVariablesDelegate = Marshal.GetDelegateForFunctionPointer<BlockRequiredVariablesDelegate>(block._requiredVariables);
      return requiredVariablesDelegate(ref block);
    }
  }

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBExposedTypesInfo BlockExposedVariablesDelegate(ref CBlock block);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate uint BlockHashDelegate(ref CBlock block);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBOptionalString BlockHelpDelegate(ref CBlock block);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBTypesInfo BlockInputOutputTypesDelegate(ref CBlock block);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBString BlockNameDelegate(ref CBlock block);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBParametersInfo BlockParametersDelegate(ref CBlock block);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate IntPtr BlockPropertiesDelegate(ref CBlock block);

  [UnmanagedFunctionPointer(NativeMethods.CallingConv)]
  internal delegate CBExposedTypesInfo BlockRequiredVariablesDelegate(ref CBlock block);
}
