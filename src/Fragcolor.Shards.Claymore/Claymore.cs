/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fragcolor.Shards.Claymore
{
  public static class Claymore
  {
    private const int DefaultWaitMillis = 10;

    /// <summary>
    /// Requests data corresponding to the specified <paramref name="hash"/>.
    /// </summary>
    /// <param name="hash">The hash of the requested data.</param>
    /// <param name="waitMillis"></param>
    /// <param name="mesh">An optional mesh to use to schedule the undelying wire.
    /// It will use a shared one if none are provided.</param>
    /// <returns>A variable containing the requested data.</returns>
    public static Variable RequestData(string hash, int waitMillis = DefaultWaitMillis, Mesh? mesh = default)
    {
      using var request = new GetRequest(hash, mesh);
      do
      {
        request.Tick();
        Thread.Sleep(waitMillis <= 1 ? 1 : waitMillis);
      } while (!request.IsCompleted);

      return request.GetResult();
    }

    public static async Task<Variable> RequestDataAsync(string hash, int waitMillis = DefaultWaitMillis, Mesh? mesh = default, CancellationToken token = default)
    {
      using var request = new GetRequest(hash, mesh);
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

    public static void Upload(byte[] bytes, string type, int waitMillis = DefaultWaitMillis, Mesh? mesh = default)
    {
      using var request = new UploadRequest(bytes, type, mesh);
      do
      {
        request.Tick();
        Thread.Sleep(waitMillis <= 1 ? 1 : waitMillis);
      } while (!request.IsCompleted);
    }

    public static async Task UploadAsync(byte[] bytes, string type, int waitMillis = DefaultWaitMillis, Mesh? mesh = default, CancellationToken token = default)
    {
      using var request = new UploadRequest(bytes, type, mesh);
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
    }
  }
}
