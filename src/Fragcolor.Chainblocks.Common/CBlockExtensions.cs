/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Fragcolor.Chainblocks.Collections;

namespace Fragcolor.Chainblocks
{
  public static class CBlockExtensions
  {
    public static CBlockPtr AsPointer(this ref CBlock block)
    {
      unsafe
      {
        var ptr = Unsafe.AsPointer(ref block);
        return new() { _ref = (IntPtr)ptr };
      }
    }

    public static ref CBlock AsRef(this CBlockPtr ptr)
    {
      unsafe
      {
        return ref Unsafe.AsRef<CBlock>(ptr._ref.ToPointer());
      }
    }

    public static CBExposedTypesInfo ExposedVariables(this ref CBlock block)
    {
      var exposedVariablesDelegate = Marshal.GetDelegateForFunctionPointer<BlockExposedVariablesDelegate>(block._exposedVariables);
      return exposedVariablesDelegate(ref block);
    }

    public static uint Hash(this ref CBlock block)
    {
      var hashDelegate = Marshal.GetDelegateForFunctionPointer<BlockHashDelegate>(block._hash);
      return hashDelegate(ref block);
    }

    public static CBOptionalString Help(this ref CBlock block)
    {
      var helpDelegate = Marshal.GetDelegateForFunctionPointer<BlockHelpDelegate>(block._help);
      return helpDelegate(ref block);
    }

    public static CBOptionalString InputHelp(this ref CBlock block)
    {
      var helpDelegate = Marshal.GetDelegateForFunctionPointer<BlockHelpDelegate>(block._inputHelp);
      return helpDelegate(ref block);
    }

    public static CBTypesInfo InputTypes(this ref CBlock block)
    {
      var inputTypesDelegate = Marshal.GetDelegateForFunctionPointer<BlockInputOutputTypesDelegate>(block._inputTypes);
      return inputTypesDelegate(ref block);
    }

    public static string? Name(this ref CBlock block)
    {
      var nameDelegate = Marshal.GetDelegateForFunctionPointer<BlockNameDelegate>(block._name);
      return (string?)nameDelegate(ref block);
    }

    public static CBOptionalString OutputHelp(this ref CBlock block)
    {
      var helpDelegate = Marshal.GetDelegateForFunctionPointer<BlockHelpDelegate>(block._outputHelp);
      return helpDelegate(ref block);
    }

    public static CBTypesInfo OutputTypes(this ref CBlock block)
    {
      var outputTypesDelegate = Marshal.GetDelegateForFunctionPointer<BlockInputOutputTypesDelegate>(block._outputTypes);
      return outputTypesDelegate(ref block);
    }

    public static CBParametersInfo Parameters(this ref CBlock block)
    {
      var parametersVariablesDelegate = Marshal.GetDelegateForFunctionPointer<BlockParametersDelegate>(block._parameters);
      return parametersVariablesDelegate(ref block);
    }

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
