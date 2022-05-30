/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Shards
{
  /// <summary>
  /// Extension methods for <see cref="SHVar"/>.
  /// </summary>
  public static class SHVarExtensions
  {
    /// <summary>
    /// Determines whether the variable base type is a floating point number.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <returns><c>true</c> if the base type is a floating point number; otherwise, <c>false</c>.</returns>
    public static bool IsFloat(this ref SHVar var)
    {
      return var.type switch
      {
        SHType.Float or
        SHType.Float2 or
        SHType.Float3 or
        SHType.Float4 => true,
        _ => false,
      };
    }

    /// <summary>
    /// Determines whether the variable base type is an integer.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <returns><c>true</c> if the base type is an integer; otherwise, <c>false</c>.</returns>
    public static bool IsInteger(this ref SHVar var)
    {
      return var.type switch
      {
        SHType.Int or
        SHType.Int2 or
        SHType.Int3 or
        SHType.Int4 or
        SHType.Int8 or
        SHType.Int16 => true,
        SHType.Color => true,
        _ => false,
      };
    }

    /// <summary>
    /// Determines whether the variable type is <see cref="SHType.None"/>
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <returns><c>true</c> if the type is <see cref="SHType.None"/>; otherwise, <c>false</c>.</returns>
    public static bool IsNone(this ref SHVar var)
    {
      return var.type == SHType.None;
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref SHVar var, bool value)
    {
      var.@bool = value;
      var.type = SHType.Bool;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref SHVar var, int value)
    {
      var.@int = value;
      var.type = SHType.Int;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref SHVar var, Int2 value)
    {
      var.int2 = value;
      var.type = SHType.Int2;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref SHVar var, Int3 value)
    {
      var.int3 = value;
      var.type = SHType.Int3;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref SHVar var, Int4 value)
    {
      var.int4 = value;
      var.type = SHType.Int4;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref SHVar var, Int8 value)
    {
      var.int8 = value;
      var.type = SHType.Int8;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref SHVar var, Int16 value)
    {
      var.int16 = value;
      var.type = SHType.Int16;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref SHVar var, double value)
    {
      var.@float = value;
      var.type = SHType.Float;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref SHVar var, Float2 value)
    {
      var.float2 = value;
      var.type = SHType.Float2;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref SHVar var, Float3 value)
    {
      var.float3 = value;
      var.type = SHType.Float3;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref SHVar var, Float4 value)
    {
      var.float4 = value;
      var.type = SHType.Float4;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref SHVar var, SHColor value)
    {
      var.color = value;
      var.type = SHType.Color;
      // TODO: should we clone the var?
    }

    public static byte[]? GetBytes(this ref SHVar var)
    {
      if (var.type != SHType.Bytes) return null;

      var bytes = new byte[var._arrayLength];
      Marshal.Copy(var.array._ptr, bytes, 0, bytes.Length);
      return bytes;
    }

    public static void SetBytes(this ref SHVar var, byte[] bytes)
    {
      if (bytes == null) throw new ArgumentNullException(nameof(bytes));

      // note: destroy = false since we dispose of the allocated array after cloning
      using var tmp = new Variable(false);
      var ptr = Marshal.AllocCoTaskMem(bytes.Length);
      try
      {
        tmp.Value.type = SHType.Bytes;
        tmp.Value._arrayCapacity = (uint)bytes.Length;
        tmp.Value._arrayLength = (uint)bytes.Length;
        Marshal.Copy(bytes, 0, ptr, bytes.Length);
        tmp.Value.array = new() { _ptr = ptr };
        Native.Core.CloneVar(ref var, ref tmp.Value);
      }
      finally
      {
        Marshal.FreeCoTaskMem(ptr);
      }
    }

    /// <summary>
    /// Gets a <see cref="string"/> corresponding to the variable value, or <c>null</c>.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <returns>A <see cref="string"/> representation of the variable of its type is <see cref="SHType.String"/>; otherwise, <c>null</c>. </returns>
    public static string? GetString(this ref SHVar var)
    {
      if (var.type != SHType.String) return null;

      return (string?)var.@string;
    }

    /// <summary>
    /// Sets the variable's value to the provided <see cref="string"/> <paramref name="value"/>. 
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetString(this ref SHVar var, string? value)
    {
      // note: destroy = false since we dispose of the string at the end of the method
      using var tmp = new Variable(false);
      var cbstr = (SHString)value;
      try
      {
        tmp.Value.@string = cbstr;
        tmp.Value.type = SHType.String;
        Native.Core.CloneVar(ref var, ref tmp.Value);
      }
      finally
      {
        cbstr.Dispose();
      }
    }
  }
}
