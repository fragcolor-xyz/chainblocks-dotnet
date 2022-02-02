/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Explicit, Size = 32)]
  public struct CBVar
  {
    //! Native struct, don't edit
    [FieldOffset(0)]
    public bool @bool;

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
    public CBColor color;

    [FieldOffset(0)]
    public CBImage image;

    [FieldOffset(0)]
    public CBAudio audio;

    [FieldOffset(0)]
    public Chain chain;

    [FieldOffset(0)]
    public Block block;

    [FieldOffset(0)]
    public CBEnum @enum;

    [FieldOffset(16)]
    public CBType type;

    [FieldOffset(17)]
    internal CBType _innerType;

    [FieldOffset(18)]
    public ushort flags;

    [FieldOffset(20)]
    internal uint _refCount;

    [FieldOffset(24)]
    internal IntPtr _objectInfo;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct Int2
  {
    public long x;
    public long y;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct Int3
  {
    public int x;
    public int y;
    public int z;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct Int4
  {
    public int x;
    public int y;
    public int z;
    public int w;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct Int8
  {
    public short x1;
    public short y1;
    public short z1;
    public short w1;
    public short x2;
    public short y2;
    public short z2;
    public short w2;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct Int16
  {
    public byte x1;
    public byte y1;
    public byte z1;
    public byte w1;
    public byte x2;
    public byte y2;
    public byte z2;
    public byte w2;
    public byte x3;
    public byte y3;
    public byte z3;
    public byte w3;
    public byte x4;
    public byte y4;
    public byte z4;
    public byte w4;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct Float2
  {
    public double x;
    public double y;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct Float3
  {
    public float x;
    public float y;
    public float z;
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct Float4
  {
    public float x;
    public float y;
    public float z;
    public float w;
  }
}
