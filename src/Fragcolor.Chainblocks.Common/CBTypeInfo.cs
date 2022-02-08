/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.InteropServices;

using Fragcolor.Chainblocks.Collections;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBTypeInfo
  {
    //! Native struct, don't edit
    internal CBType _basicType;
    internal Details _details;
    internal uint _fixedSize;
    internal CBType _innerType;
    internal CBBool _recursiveSelf;

    [StructLayout(LayoutKind.Explicit, Size = 32)]
    internal struct Details
    {
      [FieldOffset(0)]
      internal Object _object;

      [FieldOffset(0)]
      internal Enum _enum;

      [FieldOffset(0)]
      internal CBTypesInfo _seqTypes;

      [FieldOffset(0)]
      internal CBTypesInfo _setTypes;

      [FieldOffset(0)]
      internal Table _table;

      [FieldOffset(0)]
      internal CBTypesInfo _contextVarTypes;

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
        internal CBStrings _keys;
        internal CBTypesInfo _types;
      }

      [StructLayout(LayoutKind.Sequential)]
      internal struct Path
      {
        internal CBStrings _extensions;
        internal CBBool _file;
        internal CBBool _existing;
        internal CBBool _relative;
      }

      [StructLayout(LayoutKind.Sequential)]
      internal struct Integers
      {
        internal long _min;
        internal long _max;
        internal CBBool _valid;
      }

      [StructLayout(LayoutKind.Sequential)]
      internal struct Real
      {
        internal long _min;
        internal long _max;
        internal CBBool _valid;
      }
    }
  }
}
