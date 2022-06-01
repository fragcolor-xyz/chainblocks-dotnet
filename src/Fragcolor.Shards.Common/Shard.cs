/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Shards
{
  /// <summary>
  /// Wire struct.
  /// </summary>
  /// <remarks>
  /// See <see cref="ShardExtensions"/> for available methods on this struct.
  /// </remarks>
  [StructLayout(LayoutKind.Sequential)]
  public struct Shard
  {
    //! Native struct, don't edit
    internal uint _inlineShardId;
    internal SHBool _owned;
    internal IntPtr _name;
    internal IntPtr _hash;
    internal IntPtr _help;
    internal IntPtr _inputHelp;
    internal IntPtr _outputHelp;
    internal IntPtr _properties;
    internal IntPtr _setup;
    internal IntPtr _destroy;
    internal IntPtr _inputTypes;
    internal IntPtr _outputTypes;
    internal IntPtr _exposedVariables;
    internal IntPtr _requiredVariables;
    internal IntPtr _compose;
    internal IntPtr _composed;
    internal IntPtr _parameters;
    internal IntPtr _setParam;
    internal IntPtr _getParam;
    internal IntPtr _warmup;
    internal IntPtr _activate;
    internal IntPtr _cleanup;
    internal IntPtr _nextFrame;
    internal IntPtr _mutate;
    internal IntPtr _crossover;
    internal IntPtr _getState;
    internal IntPtr _setState;
    internal IntPtr _resetState;
  }
}
