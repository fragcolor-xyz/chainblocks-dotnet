/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBColor
  {
    public byte r;
    public byte g;
    public byte b;
    public byte a;

    public static implicit operator CBColor((byte r, byte g, byte b, byte a) t)
    {
      return new() { r = t.r, g = t.g, b = t.b, a = t.a };
    }

    public void Deconstruct(out byte r, out byte g, out byte b, out byte a)
    {
      r = this.r;
      g = this.g;
      b = this.b;
      a = this.a;
    }
  }
}
