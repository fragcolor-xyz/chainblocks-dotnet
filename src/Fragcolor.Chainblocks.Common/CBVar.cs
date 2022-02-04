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

    public static implicit operator Int2((long x, long y) t) => new() { x = t.x, y = t.y };

    public void Deconstruct(out long x, out long y)
    {
      x = this.x;
      y = this.y;
    }
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct Int3
  {
    public int x;
    public int y;
    public int z;

    public static implicit operator Int3((int x, int y, int z) t)
    {
      return new() { x = t.x, y = t.y, z = t.z };
    }

    public void Deconstruct(out int x, out int y, out int z)
    {
      x = this.x;
      y = this.y;
      z = this.z;
    }
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct Int4
  {
    public int x;
    public int y;
    public int z;
    public int w;

    public static implicit operator Int4((int x, int y, int z, int w) t)
    {
      return new() { x = t.x, y = t.y, z = t.z, w = t.w };
    }

    public void Deconstruct(out int x, out int y, out int z, out int w)
    {
      x = this.x;
      y = this.y;
      z = this.z;
      w = this.w;
    }
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

    public static implicit operator Int8((
      (short x, short y, short z, short w) v1,
      (short x, short y, short z, short w) v2) t)
    {
      return new()
      {
        x1 = t.v1.x,
        y1 = t.v1.y,
        z1 = t.v1.z,
        w1 = t.v1.w,
        x2 = t.v2.x,
        y2 = t.v2.y,
        z2 = t.v2.z,
        w2 = t.v2.w
      };
    }

    public void Deconstruct(
      out (short x, short y, short z, short w) v1,
      out (short x, short y, short z, short w) v2)
    {
      v1.x = x1;
      v1.y = y1;
      v1.z = z1;
      v1.w = w1;
      v2.x = x2;
      v2.y = y2;
      v2.z = z2;
      v2.w = w2;
    }
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

    public static implicit operator Int16((
      (byte x, byte y, byte z, byte w) v1,
      (byte x, byte y, byte z, byte w) v2,
      (byte x, byte y, byte z, byte w) v3,
      (byte x, byte y, byte z, byte w) v4) t)
    {
      return new()
      {
        x1 = t.v1.x,
        y1 = t.v1.y,
        z1 = t.v1.z,
        w1 = t.v1.w,
        x2 = t.v2.x,
        y2 = t.v2.y,
        z2 = t.v2.z,
        w2 = t.v2.w,
        x3 = t.v3.x,
        y3 = t.v3.y,
        z3 = t.v3.z,
        w3 = t.v3.w,
        x4 = t.v4.x,
        y4 = t.v4.y,
        z4 = t.v4.z,
        w4 = t.v4.w,
      };
    }

    public void Deconstruct(
      out (byte x, byte y, byte z, byte w) v1,
      out (byte x, byte y, byte z, byte w) v2,
      out (byte x, byte y, byte z, byte w) v3,
      out (byte x, byte y, byte z, byte w) v4)
    {
      v1.x = x1;
      v1.y = y1;
      v1.z = z1;
      v1.w = w1;
      v2.x = x2;
      v2.y = y2;
      v2.z = z2;
      v2.w = w2;
      v3.x = x3;
      v3.y = y3;
      v3.z = z3;
      v3.w = w3;
      v4.x = x4;
      v4.y = y4;
      v4.z = z4;
      v4.w = w4;
    }
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct Float2
  {
    public double x;
    public double y;

    public static implicit operator Float2((double x, double y) t) => new() { x = t.x, y = t.y };

    public void Deconstruct(out double x, out double y)
    {
      x = this.x;
      y = this.y;
    }
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct Float3
  {
    public float x;
    public float y;
    public float z;

    public static implicit operator Float3((float x, float y, float z) t)
    {
      return new() { x = t.x, y = t.y, z = t.z };
    }

    public void Deconstruct(out float x, out float y, out float z)
    {
      x = this.x;
      y = this.y;
      z = this.z;
    }
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct Float4
  {
    public float x;
    public float y;
    public float z;
    public float w;

    public static implicit operator Float4((float x, float y, float z, float w) t)
    {
      return new() { x = t.x, y = t.y, z = t.z, w = t.w };
    }

    public void Deconstruct(out float x, out float y, out float z, out float w)
    {
      x = this.x;
      y = this.y;
      z = this.z;
      w = this.w;
    }
  }
}
