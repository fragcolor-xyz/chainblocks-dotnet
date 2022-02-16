/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using NUnit.Framework;

namespace Fragcolor.Chainblocks.Tests
{
  /// <summary>
  /// Tests for string manipulations.
  /// </summary>
  [TestFixture]
  internal sealed class StringTests : TestBase
  {
    [TearDown]
    public void TearDown()
    {
      UnscheduleChain();
      _chain?.Dispose();
    }

    [Test]
    public void TestDefaultString()
    {
      var cbstr = default(CBString);
      Assert.AreEqual(IntPtr.Zero, cbstr._str);
      Assert.IsTrue(string.IsNullOrEmpty((string?)cbstr));

      var str = default(string);
      Assert.IsNull(str);
      Assert.AreEqual(cbstr, (CBString)str);
    }

    /// <summary>
    /// Tests setting string values.
    /// </summary>
    [Test]
    public void TestSetString()
    {
      _chain = new Variable();
      var ok = Env.Eval(@$"(Chain ""{nameof(TestSetString)}"" :Looped .msg (Log) (String.ToUpper) > .result)", _chain.Ptr);
      Assert.IsTrue(ok);
      Assert.IsTrue(Chain.IsValid());

      using var message = new ExternalVariable(Chain, "msg");
      Assert.IsTrue(message.Value.IsNone());
      Assert.DoesNotThrow(() => message.Value.SetString(null));
      Assert.IsTrue(string.IsNullOrEmpty(message.Value.GetString()));
      message.Value.SetString("Hello");
      Assert.AreEqual(CBType.String, message.Value.type);

      using var result = new ExternalVariable(Chain, "result", CBType.String);
      Assert.AreEqual(CBType.String, result.Value.type);
      ScheduleChain();
      Tick();
      Assert.AreEqual("HELLO", result.Value.GetString());

      message.Value.SetString("World");
      Tick();
      Assert.AreEqual("WORLD", result.Value.GetString());
    }

    /// <summary>
    /// Tests getting string values.
    /// </summary>
    [Test]
    public void TestGetString()
    {
      _chain = new Variable();
      var ok = Env.Eval(@$"(Chain ""{nameof(TestGetString)}"" ""こんにちは"" > .msg (Log) (Pause) ""世界"" > .msg (Log))", _chain.Ptr);
      Assert.IsTrue(ok);
      Assert.IsTrue(Chain.IsValid());

      using var message = new ExternalVariable(Chain, "msg", CBType.String);
      Assert.AreEqual(CBType.String, message.Value.type);

      ScheduleChain();

      Tick();
      Assert.AreEqual("こんにちは", message.Value.GetString());

      Tick();
      Assert.AreEqual("世界", message.Value.GetString());

      using var var = new Variable();
      var.Value.SetValue(42);
      Assert.AreEqual(null, var.Value.GetString());
    }
  }
}
