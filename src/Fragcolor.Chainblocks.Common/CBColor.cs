/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  /// <summary>
  /// Represents a RGBA color.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct CBColor
  {
    //! Native struct, don't edit
    /// <summary>
    /// Red component.
    /// </summary>
    public byte r;
    /// <summary>
    /// Green component.
    /// </summary>
    public byte g;
    /// <summary>
    /// Blue component.
    /// </summary>
    public byte b;
    /// <summary>
    /// Alpha component.
    /// </summary>
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
