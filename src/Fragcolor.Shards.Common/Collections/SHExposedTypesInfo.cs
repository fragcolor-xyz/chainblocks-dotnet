/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fragcolor.Shards.Collections
{
  /// <summary>
  /// Represents a collection of <see cref="SHExposedTypeInfo"/>.
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct SHExposedTypesInfo
  {
    //! Native struct, don't edit
    internal IntPtr _elements;
    internal uint _length;
    internal uint _capacity;

    /// <summary>
    /// The number of elements contained in the collection.
    /// </summary>
    public uint Count => _length;

    /// <summary>
    /// Gets a reference to the <see cref="SHExposedTypeInfo"/> at the specified index.
    /// </summary>
    /// <param name="i">The element index.</param>
    /// <returns>A reference to the element at the specified index.</returns>
    /// <exception cref="IndexOutOfRangeException"><paramref name="i"/> is equal to or greater than <see cref="Count"/>.</exception>
    public ref SHExposedTypeInfo this[uint i]
    {
      get
      {
        if (i >= _length) throw new IndexOutOfRangeException();

        unsafe
        {
          var ptr = (SHExposedTypeInfo*)_elements.ToPointer();
          return ref Unsafe.AsRef(ptr[i]);
        }
      }
    }
  }
}
