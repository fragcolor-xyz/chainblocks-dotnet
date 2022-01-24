/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

namespace System.Diagnostics.CodeAnalysis
{
  [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
  internal class NotNullWhenAttribute : Attribute
  {
    public bool ReturnValue { get; }

    public NotNullWhenAttribute(bool returnValue)
    {
      ReturnValue = returnValue;
    }
  }
}
