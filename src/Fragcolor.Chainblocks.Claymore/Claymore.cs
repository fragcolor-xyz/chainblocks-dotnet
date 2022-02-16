/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fragcolor.Chainblocks.Claymore
{
 public static class Claymore
  {
    private static readonly Lazy<Node> _node = new();

    /// <summary>
    /// Requests data corresponding to the specified <paramref name="hash"/>.
    /// </summary>
    /// <param name="hash"></param>
    /// <returns>A variable containing the requested data.</returns>
    public static Variable RequestData(string hash)
    {
      var node = _node.Value;
      var bytes = new byte[32];
      HexToBytes(hash, bytes);
      ref var request = ref NativeMethods.clmrGetDataStart(bytes).AsRef();
      PollStatePtr pollPtr = default;
      try
      {
        node.Schedule(request._chain.chain);
        do
        {
          Native.Core.Tick(node);
          Thread.Sleep(25);
        } while (!NativeMethods.clmrPoll(request.Chain(), out pollPtr));

        var var = new Variable();
        if (pollPtr.IsValid())
        {
          ref var poll = ref pollPtr.AsRef();
          switch (poll._tag)
          {
            case PollStateTag.Failed:
              // TODO
              break;

            case PollStateTag.Finished:
              Native.Core.CloneVar(ref var.Value, ref request._result);
              break;
          }
        }
        node.Unschedule(request._chain.chain);

        return var;
      }
      finally
      {
        if (pollPtr.IsValid()) NativeMethods.clmrPollFree(pollPtr);
        NativeMethods.clmrGetDataFree(request.AsPointer());
      }
    }

    public static async Task<Variable> RequestDataAsync(string hash)
    {
      var node = _node.Value;
      var bytes = new byte[32];
      HexToBytes(hash, bytes);
      var requestPtr = NativeMethods.clmrGetDataStart(bytes);
      PollStatePtr pollPtr = default;
      try
      {
        node.Schedule(requestPtr.AsRef()._chain.chain);
        do
        {
          Native.Core.Tick(node);
          await Task.Delay(25);
        } while (!NativeMethods.clmrPoll(requestPtr.AsRef().Chain(), out pollPtr));

        var var = new Variable();
        if (pollPtr.IsValid())
        {
          switch (pollPtr.AsRef()._tag)
          {
            case PollStateTag.Failed:
              // TODO
              break;

            case PollStateTag.Finished:
              Native.Core.CloneVar(ref var.Value, ref requestPtr.AsRef()._result);
              break;
          }
        }
        node.Unschedule(requestPtr.AsRef().Chain());

        return var;
      }
      finally
      {
        if (pollPtr.IsValid()) NativeMethods.clmrPollFree(pollPtr);
        NativeMethods.clmrGetDataFree(requestPtr);
      }
    }

    private static void HexToBytes(string src, IList<byte> bytes)
    {
      var offset = 0;
      if (src[0] == '0' && (src[1] == 'x' || src[1] == 'X')) offset = 2;

      for (var i = 0; offset + i < src.Length && i < bytes.Count * 2; i += 2)
      {
        bytes[i / 2] = (byte) (Char2Int(src[offset + i]) * 16 + Char2Int(src[offset + i + 1]));
      }

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
