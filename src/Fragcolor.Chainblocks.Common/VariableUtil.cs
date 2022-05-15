/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

namespace Fragcolor.Chainblocks
{
  public static class VariableUtil
  {
    /// <summary>
    /// Creates a new variable of type <see cref="CBType.Bool"/> with the provided <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The initial value of the variable.</param>
    /// <param name="destroy">
    /// <c>true</c> if the value must be destroyed when the variable is disposed;
    /// otherwise, <c>false</c>. The default is <c>false</c> for scalars.
    /// </param>
    /// <returns>The new variable.</returns>
    public static Variable NewBool(bool value, bool destroy = false)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    /// <summary>
    /// Creates a new variable of type <see cref="CBType.Int"/> with the provided <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The initial value of the variable.</param>
    /// <param name="destroy">
    /// <c>true</c> if the value must be destroyed when the variable is disposed;
    /// otherwise, <c>false</c>. The default is <c>false</c> for scalars.
    /// </param>
    /// <returns>The new variable.</returns>
    public static Variable NewInt(int value, bool destroy = false)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    /// <summary>
    /// Creates a new variable of type <see cref="CBType.Int2"/> with the provided <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The initial value of the variable.</param>
    /// <param name="destroy">
    /// <c>true</c> if the value must be destroyed when the variable is disposed;
    /// otherwise, <c>false</c>. The default is <c>false</c> for scalars.
    /// </param>
    /// <returns>The new variable.</returns>
    public static Variable NewInt2(Int2 value, bool destroy = false)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    /// <summary>
    /// Creates a new variable of type <see cref="CBType.Int3"/> with the provided <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The initial value of the variable.</param>
    /// <param name="destroy">
    /// <c>true</c> if the value must be destroyed when the variable is disposed;
    /// otherwise, <c>false</c>. The default is <c>false</c> for scalars.
    /// </param>
    /// <returns>The new variable.</returns>
    public static Variable NewInt3(Int3 value, bool destroy = false)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    /// <summary>
    /// Creates a new variable of type <see cref="CBType.Int4"/> with the provided <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The initial value of the variable.</param>
    /// <param name="destroy">
    /// <c>true</c> if the value must be destroyed when the variable is disposed;
    /// otherwise, <c>false</c>. The default is <c>false</c> for scalars.
    /// </param>
    /// <returns>The new variable.</returns>
    public static Variable NewInt4(Int4 value, bool destroy = false)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    /// <summary>
    /// Creates a new variable of type <see cref="CBType.Int8"/> with the provided <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The initial value of the variable.</param>
    /// <param name="destroy">
    /// <c>true</c> if the value must be destroyed when the variable is disposed;
    /// otherwise, <c>false</c>. The default is <c>false</c> for scalars.
    /// </param>
    /// <returns>The new variable.</returns>
    public static Variable NewInt8(Int8 value, bool destroy = false)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    /// <summary>
    /// Creates a new variable of type <see cref="CBType.Int16"/> with the provided <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The initial value of the variable.</param>
    /// <param name="destroy">
    /// <c>true</c> if the value must be destroyed when the variable is disposed;
    /// otherwise, <c>false</c>. The default is <c>false</c> for scalars.
    /// </param>
    /// <returns>The new variable.</returns>
    public static Variable NewInt16(Int16 value, bool destroy = false)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    /// <summary>
    /// Creates a new variable of type <see cref="CBType.Float"/> with the provided <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The initial value of the variable.</param>
    /// <param name="destroy">
    /// <c>true</c> if the value must be destroyed when the variable is disposed;
    /// otherwise, <c>false</c>. The default is <c>false</c> for scalars.
    /// </param>
    /// <returns>The new variable.</returns>
    public static Variable NewFloat(double value, bool destroy = false)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    /// <summary>
    /// Creates a new variable of type <see cref="CBType.Float2"/> with the provided <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The initial value of the variable.</param>
    /// <param name="destroy">
    /// <c>true</c> if the value must be destroyed when the variable is disposed;
    /// otherwise, <c>false</c>. The default is <c>false</c> for scalars.
    /// </param>
    /// <returns>The new variable.</returns>
    public static Variable NewFloat2(Float2 value, bool destroy = false)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    /// <summary>
    /// Creates a new variable of type <see cref="CBType.Float3"/> with the provided <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The initial value of the variable.</param>
    /// <param name="destroy">
    /// <c>true</c> if the value must be destroyed when the variable is disposed;
    /// otherwise, <c>false</c>. The default is <c>false</c> for scalars.
    /// </param>
    /// <returns>The new variable.</returns>
    public static Variable NewFloat3(Float3 value, bool destroy = false)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    /// <summary>
    /// Creates a new variable of type <see cref="CBType.Float4"/> with the provided <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The initial value of the variable.</param>
    /// <param name="destroy">
    /// <c>true</c> if the value must be destroyed when the variable is disposed;
    /// otherwise, <c>false</c>. The default is <c>false</c> for scalars.
    /// </param>
    /// <returns>The new variable.</returns>
    public static Variable NewFloat4(Float4 value, bool destroy = false)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    /// <summary>
    /// Creates a new variable of type <see cref="CBType.Color"/> with the provided <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The initial value of the variable.</param>
    /// <param name="destroy">
    /// <c>true</c> if the value must be destroyed when the variable is disposed;
    /// otherwise, <c>false</c>. The default is <c>false</c> for scalars.
    /// </param>
    /// <returns>The new variable.</returns>
    public static Variable NewColor(CBColor value, bool destroy = false)
    {
      var variable = new Variable(destroy);
      variable.Value.SetValue(value);
      return variable;
    }

    public static Variable NewString(string? value, bool destroy = false)
    {
      var variable = new Variable(destroy);
      variable.Value.SetString(value);
      return variable;
    }
  }
}
