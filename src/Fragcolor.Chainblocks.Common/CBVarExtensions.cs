/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  /// <summary>
  /// Extension methods for <see cref="CBVar"/>.
  /// </summary>
  public static class CBVarExtensions
  {
    /// <summary>
    /// Determines whether the variable base type is a floating point number.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <returns><c>true</c> if the base type is a floating point number; otherwise, <c>false</c>.</returns>
    public static bool IsFloat(this ref CBVar var)
    {
      return var.type switch
      {
        CBType.Float or
        CBType.Float2 or
        CBType.Float3 or
        CBType.Float4 => true,
        _ => false,
      };
    }

    /// <summary>
    /// Determines whether the variable base type is an integer.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <returns><c>true</c> if the base type is an integer; otherwise, <c>false</c>.</returns>
    public static bool IsInteger(this ref CBVar var)
    {
      return var.type switch
      {
        CBType.Int or
        CBType.Int2 or
        CBType.Int3 or
        CBType.Int4 or
        CBType.Int8 or
        CBType.Int16 => true,
        CBType.Color => true,
        _ => false,
      };
    }

    /// <summary>
    /// Determines whether the variable type is <see cref="CBType.None"/>
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <returns><c>true</c> if the type is <see cref="CBType.None"/>; otherwise, <c>false</c>.</returns>
    public static bool IsNone(this ref CBVar var)
    {
      return var.type == CBType.None;
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref CBVar var, bool value)
    {
      var.@bool = value;
      var.type = CBType.Bool;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref CBVar var, int value)
    {
      var.@int = value;
      var.type = CBType.Int;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref CBVar var, Int2 value)
    {
      var.int2 = value;
      var.type = CBType.Int2;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref CBVar var, Int3 value)
    {
      var.int3 = value;
      var.type = CBType.Int3;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref CBVar var, Int4 value)
    {
      var.int4 = value;
      var.type = CBType.Int4;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref CBVar var, Int8 value)
    {
      var.int8 = value;
      var.type = CBType.Int8;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref CBVar var, Int16 value)
    {
      var.int16 = value;
      var.type = CBType.Int16;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref CBVar var, double value)
    {
      var.@float = value;
      var.type = CBType.Float;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref CBVar var, Float2 value)
    {
      var.float2 = value;
      var.type = CBType.Float2;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref CBVar var, Float3 value)
    {
      var.float3 = value;
      var.type = CBType.Float3;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref CBVar var, Float4 value)
    {
      var.float4 = value;
      var.type = CBType.Float4;
      // TODO: should we clone the var?
    }

    /// <summary>
    /// Sets the variable's value to the provided <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetValue(this ref CBVar var, CBColor value)
    {
      var.color = value;
      var.type = CBType.Color;
      // TODO: should we clone the var?
    }

    public static byte[]? GetBytes(this ref CBVar var)
    {
      if (var.type != CBType.Bytes) return null;

      var bytes = new byte[var._arrayLength];
      Marshal.Copy(var.array._ptr, bytes, 0, bytes.Length);
      return bytes;
    }

    public static void SetBytes(this ref CBVar var, byte[] bytes)
    {
      if (bytes == null) throw new ArgumentNullException(nameof(bytes));

      // note: destroy = false since we dispose of the allocated array after cloning
      using var tmp = new Variable(false);
      var ptr = Marshal.AllocCoTaskMem(bytes.Length);
      try
      {
        tmp.Value.type = CBType.Bytes;
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
    /// <returns>A <see cref="string"/> representation of the variable of its type is <see cref="CBType.String"/>; otherwise, <c>null</c>. </returns>
    public static string? GetString(this ref CBVar var)
    {
      if (var.type != CBType.String) return null;

      return (string?)var.@string;
    }

    /// <summary>
    /// Sets the variable's value to the provided <see cref="string"/> <paramref name="value"/>.
    /// Also sets the variable's type accordingly.
    /// </summary>
    /// <param name="var">A reference to the variable.</param>
    /// <param name="value">The value to set.</param>
    public static void SetString(this ref CBVar var, string? value)
    {
      // note: destroy = false since we dispose of the string at the end of the method
      using var tmp = new Variable(false);
      var cbstr = (CBString)value;
      try
      {
        tmp.Value.@string = cbstr;
        tmp.Value.type = CBType.String;
        Native.Core.CloneVar(ref var, ref tmp.Value);
      }
      finally
      {
        cbstr.Dispose();
      }
    }
  }
}
