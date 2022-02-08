/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Fragcolor.Chainblocks
{
  public static class CBVarExtensions
  {
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

    public static bool IsNone(this ref CBVar var)
    {
      return var.type == CBType.None;
    }

    public static void SetValue(this ref CBVar var, bool value)
    {
      var.@bool = value;
      var.type = CBType.Bool;
    }

    public static void SetValue(this ref CBVar var, int value)
    {
      var.@int = value;
      var.type = CBType.Int;
    }

    public static void SetValue(this ref CBVar var, Int2 value)
    {
      var.int2 = value;
      var.type = CBType.Int2;
    }

    public static void SetValue(this ref CBVar var, Int3 value)
    {
      var.int3 = value;
      var.type = CBType.Int3;
    }

    public static void SetValue(this ref CBVar var, Int4 value)
    {
      var.int4 = value;
      var.type = CBType.Int4;
    }

    public static void SetValue(this ref CBVar var, Int8 value)
    {
      var.int8 = value;
      var.type = CBType.Int8;
    }

    public static void SetValue(this ref CBVar var, Int16 value)
    {
      var.int16 = value;
      var.type = CBType.Int16;
    }

    public static void SetValue(this ref CBVar var, double value)
    {
      var.@float = value;
      var.type = CBType.Float;
    }

    public static void SetValue(this ref CBVar var, Float2 value)
    {
      var.float2 = value;
      var.type = CBType.Float2;
    }

    public static void SetValue(this ref CBVar var, Float3 value)
    {
      var.float3 = value;
      var.type = CBType.Float3;
    }

    public static void SetValue(this ref CBVar var, Float4 value)
    {
      var.float4 = value;
      var.type = CBType.Float4;
    }

    public static void SetValue(this ref CBVar var, CBColor value)
    {
      var.color = value;
      var.type = CBType.Color;
    }

    public static string? GetString(this ref CBVar var)
    {
      if (var.type != CBType.String) return null;

      return (string?)var.@string;
    }

    // TODO: add doc about not destroying the variable, depending on the situation it might leak memory
    public static void SetString(this ref CBVar var, string? value)
    {
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
