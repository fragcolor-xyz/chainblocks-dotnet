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
    private const int DefaultWaitMillis = 10;

    /// <summary>
    /// Requests data corresponding to the specified <paramref name="hash"/>.
    /// </summary>
    /// <param name="hash">The hash of the requested data.</param>
    /// <param name="waitMillis"></param>
    /// <param name="node">An optional node to use to schedule the undelying chain.
    /// It will use a shared one if none are provided.</param>
    /// <returns>A variable containing the requested data.</returns>
    public static Variable RequestData(string hash, int waitMillis = DefaultWaitMillis, Node? node = default)
    {
      using var request = new GetRequest(hash, node);
      do
      {
        request.Tick();
        Thread.Sleep(waitMillis <= 1 ? 1 : waitMillis);
      } while (!request.IsCompleted);

      return request.GetResult();
    }

    public static async Task<Variable> RequestDataAsync(string hash, int waitMillis = DefaultWaitMillis, Node? node = default, CancellationToken token = default)
    {
      using var request = new GetRequest(hash, node);
      try
      {
        do
        {
          request.Tick();
          if (waitMillis <= 1)
            await Task.Yield();
          else
            await Task.Delay(waitMillis, token);
          if (token.IsCancellationRequested)
            request.Cancel();
        } while (!request.IsCompleted);
      }
      catch (OperationCanceledException)
      {
        request.Cancel();
      }

      return request.GetResult();
    }
  }
}
