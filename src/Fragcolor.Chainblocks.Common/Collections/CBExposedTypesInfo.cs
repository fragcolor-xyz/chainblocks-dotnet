/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks.Collections
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBExposedTypesInfo
  {
    //! Native struct, don't edit
    internal IntPtr _elements;
    internal uint _length;
    internal uint _capacity;

    public uint Count => _length;

    public ref CBExposedTypeInfo this[uint i]
    {
      get
      {
        if (i >= _length) throw new IndexOutOfRangeException();

        unsafe
        {
          var ptr = (CBExposedTypeInfo*)_elements.ToPointer();
          return ref Unsafe.AsRef(ptr[i]);
        }
      }
    }
  }
}
