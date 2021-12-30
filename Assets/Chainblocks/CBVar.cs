using System;
using System.Runtime.InteropServices;

using UnityEngine;

namespace Chainblocks
{
  [StructLayout(LayoutKind.Explicit, Size = 32)]
  public struct CBVar
  {
    //! Native struct, don't edit
    [FieldOffset(0)]
    public Vector3 vector3;

    [FieldOffset(0)]
    public IntPtr chainRef;

    [FieldOffset(16)]
    public CBType type;

    [FieldOffset(18)]
    public ushort flags;

    static CBVar From(Vector3 v)
    {
      var ret = new CBVar();
      ret.vector3 = v;
      ret.type = CBType.Float3;
      return ret;
    }
  }
}