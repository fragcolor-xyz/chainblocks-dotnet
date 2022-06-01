/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;
using Fragcolor.Shards.Collections;

namespace Fragcolor.Shards
{
  [StructLayout(LayoutKind.Sequential)]
  public struct SHTypeInfo
  {
    //! Native struct, don't edit
    internal SHType _basicType;
    internal Details _details;
    internal uint _fixedSize;
    internal SHType _innerType;
    internal SHBool _recursiveSelf;

    [StructLayout(LayoutKind.Explicit, Size = 32)]
    internal struct Details
    {
      [FieldOffset(0)]
      internal Object _object;

      [FieldOffset(0)]
      internal Enum _enum;

      [FieldOffset(0)]
      internal SHTypesInfo _seqTypes;

      [FieldOffset(0)]
      internal SHTypesInfo _setTypes;

      [FieldOffset(0)]
      internal Table _table;

      [FieldOffset(0)]
      internal SHTypesInfo _contextVarTypes;

      [FieldOffset(0)]
      internal Path _path;

      [FieldOffset(0)]
      internal Integers _integers;

      [FieldOffset(0)]
      internal Real _real;

      [StructLayout(LayoutKind.Sequential)]
      internal struct Object
      {
        internal int _vendorId;
        internal int _typeId;
      }

      [StructLayout(LayoutKind.Sequential)]
      internal struct Enum
      {
        internal int _vendorId;
        internal int _typeId;
      }

      [StructLayout(LayoutKind.Sequential)]
      internal struct Table
      {
        internal SHStrings _keys;
        internal SHTypesInfo _types;
      }

      [StructLayout(LayoutKind.Sequential)]
      internal struct Path
      {
        internal SHStrings _extensions;
        internal SHBool _file;
        internal SHBool _existing;
        internal SHBool _relative;
      }

      [StructLayout(LayoutKind.Sequential)]
      internal struct Integers
      {
        internal long _min;
        internal long _max;
        internal SHBool _valid;
      }

      [StructLayout(LayoutKind.Sequential)]
      internal struct Real
      {
        internal long _min;
        internal long _max;
        internal SHBool _valid;
      }
    }
  }
}
