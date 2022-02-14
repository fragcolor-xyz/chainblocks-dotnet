/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

using Fragcolor.Chainblocks.Collections;

namespace Fragcolor.Chainblocks
{
  /// <summary>
  /// Represents a variable in chainblocks.
  /// </summary>
  /// <remarks>
  /// See <see cref="CBVarExtensions"/> for available methods on this struct.
  /// </remarks>
  [StructLayout(LayoutKind.Explicit, Size = 32)]
  public struct CBVar
  {
    //! Native struct, don't edit
    [FieldOffset(0)]
    public CBBool @bool;

    [FieldOffset(0)]
    public CBObject @object;

    [FieldOffset(0)]
    public long @int;

    [FieldOffset(0)]
    public Int2 int2;

    [FieldOffset(0)]
    public Int3 int3;

    [FieldOffset(0)]
    public Int4 int4;

    [FieldOffset(0)]
    public Int8 int8;

    [FieldOffset(0)]
    public Int16 int16;

    [FieldOffset(0)]
    public double @float;

    [FieldOffset(0)]
    public Float2 float2;

    [FieldOffset(0)]
    public Float3 float3;

    [FieldOffset(0)]
    public Float4 float4;

    [FieldOffset(0)]
    public CBSeq seq;

    [FieldOffset(0)]
    public CBTable table;

    [FieldOffset(0)]
    public CBSet set;

    [FieldOffset(0)]
    public CBString @string;

    [FieldOffset(8)]
    internal uint _stringLength;

    [FieldOffset(12)]
    internal uint _stringCapacity;

    [FieldOffset(0)]
    public CBColor color;

    [FieldOffset(0)]
    public CBImage image;

    [FieldOffset(0)]
    public CBAudio audio;

    [FieldOffset(0)]
    public CBChainRef chain;

    [FieldOffset(0)]
    public CBlockPtr block;

    [FieldOffset(0)]
    public CBEnum @enum;

    [FieldOffset(0)]
    public CBArray array;

    [FieldOffset(8)]
    internal uint _arrayLength;

    [FieldOffset(12)]
    internal uint _arrayCapacity;

    [FieldOffset(16)]
    public CBType type;

    [FieldOffset(17)]
    internal CBType _innerType;

    [FieldOffset(18)]
    public CBVarFlags flags;

    [FieldOffset(20)]
    internal uint _refCount;

    [FieldOffset(24)]
    internal IntPtr _objectInfo;
  }

  [Flags]
  public enum CBVarFlags : ushort
  {
    None = 0,
    UsesObjectInfo = 1 << 0,
    RefCounted = 1 << 1,
    External = 1 << 2,
  }
}
