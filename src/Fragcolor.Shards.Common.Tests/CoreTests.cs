/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Diagnostics;

using NUnit.Framework;

namespace Fragcolor.Shards.Tests
{
  /// <summary>
  /// Tests for core features of shards.
  /// </summary>
  [TestFixture]
  internal sealed class CoreTests
  {
    /// <summary>
    /// Tests scripting code evaluation.
    /// </summary>
    [Test]
    public void TestEval()
    {
      using var env = new ScriptingEnv();
      using var wire = new Variable();
      Assert.Throws(typeof(ArgumentNullException), () => env.Eval(null!, wire.Ptr));
      Assert.IsFalse(env.Eval("", wire.Ptr));
      Assert.IsFalse(env.Eval("", IntPtr.Zero));
      Assert.IsTrue(env.Eval(@"(println ""Hello"")", IntPtr.Zero));
      Assert.IsTrue(env.Eval(@"(println ""Hello"")", wire.Ptr));
      Assert.AreEqual(IntPtr.Zero, wire.Value.wire._ref);
      Assert.IsTrue(env.Eval(@"(Wire ""Hello"")", IntPtr.Zero));
      Assert.IsTrue(env.Eval(@"(Wire ""Hello"")", wire.Ptr));
      Assert.AreNotEqual(IntPtr.Zero, wire.Value.wire._ref);
    }

    /// <summary>
    /// Tests initialization of a scripting environment.
    /// </summary>
    [Test]
    public void TestScriptingEnv()
    {
      using var env = new ScriptingEnv();
      Assert.AreNotEqual(IntPtr.Zero, env._env);
    }

    /// <summary>
    /// Tests the destructor of <see cref="ScriptingEnv"/>.
    /// </summary>
    /// <remarks>
    /// This is for completion purpose.
    /// Consummer code is expected to properly dispose of it so that the finalizer doesn't have to be called.
    /// </remarks>
    [Test]
    public void TestScriptingEnvDestructor()
    {
      {
        _ = new ScriptingEnv();
      }
      // destructor eventually called when out of scope
      GC.Collect();
      GC.WaitForPendingFinalizers();
    }

    /// <summary>
    /// Tests the destructor of <see cref="Mesh"/>.
    /// </summary>
    /// <remarks>
    /// This is for completion purpose.
    /// Consummer code is expected to properly dispose of it so that the finalizer doesn't have to be called.
    /// </remarks>
    [Test]
    public void TestMeshDestructor()
    {
      {
        _ = new Mesh();
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
      using var env = new ScriptingEnv();

      const double seconds = 2.0;
      Native.Core.Log($"Sleeping for {seconds} s", SHLogLevel.Warn);

      var watch = Stopwatch.StartNew();
      Native.Core.Sleep(seconds, false);
      watch.Stop();

      Native.Core.Log($"Woke up after {watch.Elapsed.TotalSeconds} s", SHLogLevel.Info);
      Assert.AreEqual(seconds, Math.Round(watch.Elapsed.TotalSeconds));
    }
  }
}
