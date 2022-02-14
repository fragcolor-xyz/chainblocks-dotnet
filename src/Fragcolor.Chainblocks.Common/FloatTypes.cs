/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
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
