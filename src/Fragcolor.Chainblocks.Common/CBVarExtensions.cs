/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

namespace Fragcolor.Chainblocks
{
  public static class CBVarExtensions
  {
    public static bool IsAny(this ref CBVar var)
    {
      return var.type == CBType.Any;
    }

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
        _ => false,
      };
    }

    public static bool IsNone(this ref CBVar var)
    {
      return var.type == CBType.None;
    }
  }
}
