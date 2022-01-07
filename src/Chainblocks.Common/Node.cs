using System;
using System.Runtime.InteropServices;

namespace Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct Node
  {
    internal IntPtr _ref;
  }
}
