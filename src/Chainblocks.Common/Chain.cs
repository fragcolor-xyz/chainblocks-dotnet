using System;
using System.Runtime.InteropServices;

namespace Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct Chain
  {
    internal IntPtr _ref;
  }
}
