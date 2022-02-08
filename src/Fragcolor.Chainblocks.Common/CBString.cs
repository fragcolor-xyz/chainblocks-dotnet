/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBString
  {
    //! Native struct, don't edit
    internal IntPtr _str;

    public static explicit operator string?(CBString str)
    {
#if NETCOREAPP
      return Marshal.PtrToStringUTF8(str._str);
#else
      if (str._str == IntPtr.Zero) return null;
      unsafe
      {
        byte* ptr = (byte*)str._str;
        int length = 0;
        for (byte* i = ptr; *i != 0; i++, length++) ;

        return Encoding.UTF8.GetString(ptr, length);
      }
#endif
    }

    // Dangerous API: allocated memory for the string might later leak if not released
    public static explicit operator CBString(string? str)
    {
#if NETCOREAPP
      var ptr = Marshal.StringToCoTaskMemUTF8(str);
#else
      var bytes = str != null ? Encoding.UTF8.GetBytes(str) : null;
      IntPtr ptr;
      if (bytes != null)
      {
        ptr = Marshal.AllocCoTaskMem(bytes.Length + 1);
        Marshal.Copy(bytes, 0, ptr, bytes.Length);
        Marshal.WriteByte(ptr, bytes.Length, 0);
      }
      else
      {
        ptr = IntPtr.Zero;
      }
#endif

      return new() { _str = ptr };
    }

    internal void Dispose()
    {
#if NETCOREAPP
      Marshal.ZeroFreeCoTaskMemUTF8(_str);
#else
      Marshal.FreeCoTaskMem(_str);
#endif
    }
  }
}
