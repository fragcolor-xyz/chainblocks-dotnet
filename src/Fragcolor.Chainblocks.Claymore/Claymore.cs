/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Tar;

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

    public static async Task<Variable> RequestDataAsync(string hash, int waitMillis = DefaultWaitMillis, Node? node = default,
      CancellationToken token = default)
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

    public static void Upload(byte[] bytes, string type, int waitMillis = DefaultWaitMillis, Node? node = default)
    {
      using var request = new UploadRequest(bytes, type, node);
      do
      {
        request.Tick();
        Thread.Sleep(waitMillis <= 1 ? 1 : waitMillis);
      } while (!request.IsCompleted);
    }

    public static async Task UploadAsync(byte[] bytes, string type, int waitMillis = DefaultWaitMillis, Node? node = default, CancellationToken token = default)
    {
      using var request = new UploadRequest(bytes, type, node);
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

    public static void UploadPath(string path, int waitMillis = DefaultWaitMillis, Node? node = default)
    {
      Upload(GetTarArchiveBytes(path), "archive", waitMillis, node);
    }

    public static Task UploadPathAsync(string path, int waitMillis = DefaultWaitMillis, Node? node = default, CancellationToken token = default)
    {
      return UploadAsync(GetTarArchiveBytes(path), "archive", waitMillis, node, token);
    }

    internal static byte[] GetTarArchiveBytes(string path)
    {
      if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

      var isDirectory = Directory.Exists(path);
      if (!isDirectory && !File.Exists(path)) throw new DirectoryNotFoundException();

      using var outStream = new MemoryStream();
      using var tarArchive = TarArchive.CreateOutputTarArchive(outStream, Encoding.UTF8);
      tarArchive.RootPath = isDirectory ? path : Path.GetDirectoryName(path) ?? string.Empty;
      if (isDirectory)
      {
        foreach (var filename in Directory.GetFiles(path))
          tarArchive.WriteEntry(TarEntry.CreateEntryFromFile(filename), false);
        foreach (var directory in Directory.GetDirectories(path))
          tarArchive.WriteEntry(TarEntry.CreateEntryFromFile(directory), true);
      }
      else
      {
        tarArchive.WriteEntry(TarEntry.CreateEntryFromFile(path), false);
      }

      tarArchive.Close();

      return outStream.ToArray();
    }
  }
}
