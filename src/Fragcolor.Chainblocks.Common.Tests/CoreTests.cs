/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Diagnostics;

using NUnit.Framework;

namespace Fragcolor.Chainblocks.Tests
{
  [TestFixture]
  internal sealed class CoreTests
  {
    [Test]
    public void TestLispEnv()
    {
      using var env = new LispEnv();
      Assert.AreNotEqual(IntPtr.Zero, env._env);
    }

    [Test]
    public void TestLispEnvDestructor()
    {
      {
        _ = new LispEnv();
      }
      // destructor eventually called when out of scope
      GC.Collect();
      GC.WaitForPendingFinalizers();
    }

    [Test]
    public void TestSleep()
    {
      using var env = new LispEnv();

      const double seconds = 2.0;
      Native.Core.Log($"Sleeping for {seconds} s", CBLogLevel.Warn);

      var watch = Stopwatch.StartNew();
      Native.Core.Sleep(seconds, false);
      watch.Stop();

      Native.Core.Log($"Woke up after {watch.Elapsed.TotalSeconds} s", CBLogLevel.Info);
      Assert.AreEqual(seconds, Math.Round(watch.Elapsed.TotalSeconds));
    }
  }
}
