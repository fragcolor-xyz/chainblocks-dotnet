/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;
using Fragcolor.Shards.Collections;

namespace Fragcolor.Shards
{
  /// <summary>
  /// Represents a variable in shards.
  /// </summary>
  /// <remarks>
  /// See <see cref="SHVarExtensions"/> for available methods on this struct.
  /// </remarks>
  [StructLayout(LayoutKind.Explicit, Size = 32)]
  public struct SHVar
  {
    //! Native struct, don't edit
    [FieldOffset(0)]
    public SHBool @bool;

    [FieldOffset(0)]
    public SHObject @object;

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
    public SHSeq seq;

    [FieldOffset(0)]
    public SHTable table;

    [FieldOffset(0)]
    public SHSet set;

    [FieldOffset(0)]
    public SHString @string;

    [FieldOffset(8)]
    internal uint _stringLength;

    [FieldOffset(12)]
    internal uint _stringCapacity;

    [FieldOffset(0)]
    public SHColor color;

    [FieldOffset(0)]
    public SHImage image;

    [FieldOffset(0)]
    public SHAudio audio;

    [FieldOffset(0)]
    public SHWireRef wire;

    [FieldOffset(0)]
    public ShardPtr shard;

    [FieldOffset(0)]
    public SHEnum @enum;

    [FieldOffset(0)]
    public SHArray array;

    [FieldOffset(8)]
    internal uint _arrayLength;

    [FieldOffset(12)]
    internal uint _arrayCapacity;

    [FieldOffset(16)]
    public SHType type;

    [FieldOffset(17)]
    internal SHType _innerType;

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
