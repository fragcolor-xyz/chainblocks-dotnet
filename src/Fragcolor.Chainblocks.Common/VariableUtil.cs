/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

namespace Fragcolor.Chainblocks
{
  public static class VariableUtil
  {
    public static Variable NewBool(bool value, bool destroy = true)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    public static Variable NewInt(int value, bool destroy = true)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    public static Variable NewInt2(Int2 value, bool destroy = true)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    public static Variable NewInt3(Int3 value, bool destroy = true)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    public static Variable NewInt4(Int4 value, bool destroy = true)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    public static Variable NewInt8(Int8 value, bool destroy = true)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    public static Variable NewInt16(Int16 value, bool destroy = true)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    public static Variable NewFloat(float value, bool destroy = true)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    public static Variable NewFloat2(Float2 value, bool destroy = true)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    public static Variable NewFloat3(Float3 value, bool destroy = true)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    public static Variable NewFloat4(Float4 value, bool destroy = true)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    public static Variable NewColor(CBColor value, bool destroy = true)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }
  }
}
