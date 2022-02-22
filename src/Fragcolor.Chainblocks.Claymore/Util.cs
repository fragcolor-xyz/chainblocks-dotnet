using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragcolor.Chainblocks.Claymore
{
  internal static class Util
  {
    internal static byte[] HexToBytes(string src)
    {
      if (src.Length < 2) return Array.Empty<byte>();

      var offset = 0;
      if (src[0] == '0' && (src[1] == 'x' || src[1] == 'X')) offset = 2;

      var bytes = new byte[(src.Length - offset)/2];
      for (var i = 0; offset + i < src.Length && i < bytes.Length * 2; i += 2)
      {
        bytes[i / 2] = (byte) (Char2Int(src[offset + i]) * 16 + Char2Int(src[offset + i + 1]));
      }

      return bytes;

      static int Char2Int(char input)
      {
        return input switch
        {
          >= '0' and <= '9' => input - '0',
          >= 'A' and <= 'F' => input - 'A' + 10,
          >= 'a' and <= 'f' => input - 'a' + 10,
          _ => throw new ArgumentException("Invalid input string"),
        };
      }
    }
  }
}
