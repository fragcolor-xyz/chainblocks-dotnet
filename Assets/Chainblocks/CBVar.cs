using System;
using System.Runtime.InteropServices;

namespace Chainblocks
{
  [StructLayout(LayoutKind.Explicit, Size = 32)]
  public struct CBVar
  {
    //! Native struct, don't edit
    [FieldOffset(0)]
    public double @float;

    [FieldOffset(0)]
    public Float2 float2;

    [FieldOffset(0)]
    public Float3 float3;

    [FieldOffset(0)]
    public Float4 float4;

    [FieldOffset(0)]
    public long @int;

    [FieldOffset(0)]
    public Int2 int2;

    [FieldOffset(0)]
    public Int3 int3;

    [FieldOffset(0)]
    public Int4 int4;

    [FieldOffset(0)]
    public IntPtr chainRef;

    [FieldOffset(16)]
    public CBType type;

    [FieldOffset(18)]
    public ushort flags;
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
}