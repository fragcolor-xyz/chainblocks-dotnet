/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBTypesInfo
  {
    internal IntPtr _elements;
    internal uint _length;
    internal uint _capacity;

    public ref CBTypeInfo this[uint i]
    {
      get
      {
        if (i >= _length) throw new IndexOutOfRangeException();

        unsafe
        {
          var ptr = (CBTypeInfo*)_elements.ToPointer();
          return ref Unsafe.AsRef(ptr[i]);
        }
      }
    }
  }
}
