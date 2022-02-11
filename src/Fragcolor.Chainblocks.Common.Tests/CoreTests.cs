/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Diagnostics;

using NUnit.Framework;

namespace Fragcolor.Chainblocks.Tests
{
  /// <summary>
  /// Tests for core features of chainblocks.
  /// </summary>
  [TestFixture]
  internal sealed class CoreTests
  {
    /// <summary>
    /// Tests LISP code evaluation.
    /// </summary>
    [Test]
    public void TestEval()
    {
      using var env = new LispEnv();
      using var chain = new Variable();
      Assert.Throws(typeof(ArgumentNullException), () => env.Eval(null!, chain.Ptr));
      Assert.IsFalse(env.Eval("", chain.Ptr));
      Assert.IsFalse(env.Eval("", IntPtr.Zero));
      Assert.IsTrue(env.Eval(@"(println ""Hello"")", IntPtr.Zero));
      Assert.IsTrue(env.Eval(@"(println ""Hello"")", chain.Ptr));
      Assert.AreEqual(IntPtr.Zero, chain.Value.chain._ref);
      Assert.IsTrue(env.Eval(@"(Chain ""Hello"")", IntPtr.Zero));
      Assert.IsTrue(env.Eval(@"(Chain ""Hello"")", chain.Ptr));
      Assert.AreNotEqual(IntPtr.Zero, chain.Value.chain._ref);
    }

    /// <summary>
    /// Tests initialization of a LISP environment.
    /// </summary>
    [Test]
    public void TestLispEnv()
    {
      using var env = new LispEnv();
      Assert.AreNotEqual(IntPtr.Zero, env._env);
    }

    /// <summary>
    /// Tests the destructor of <see cref="LispEnv"/>.
    /// </summary>
    /// <remarks>
    /// This is for completion purpose.
    /// Consummer code is expected to properly dispose of it so that the finalizer doesn't have to be called.
    /// </remarks>
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

    /// <summary>
    /// Tests the sleep feature.
    /// </summary>
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
