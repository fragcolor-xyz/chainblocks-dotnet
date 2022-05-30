/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using NUnit.Framework;

namespace Fragcolor.Shards.Tests
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
      UnscheduleWire();
      _wire?.Dispose();
    }

    [Test]
    public void TestDefaultString()
    {
      var cbstr = default(SHString);
      Assert.AreEqual(IntPtr.Zero, cbstr._str);
      Assert.IsTrue(string.IsNullOrEmpty((string?)cbstr));

      var str = default(string);
      Assert.IsNull(str);
      Assert.AreEqual(cbstr, (SHString)str);
    }

    /// <summary>
    /// Tests setting string values.
    /// </summary>
    [Test]
    public void TestSetString()
    {
      _wire = new Variable();
      var ok = Env.Eval(@$"(Wire ""{nameof(TestSetString)}"" :Looped .msg (Log) (String.ToUpper) > .result)", _wire.Ptr);
      Assert.IsTrue(ok);
      Assert.IsTrue(Wire.IsValid());

      using var message = new ExternalVariable(Wire, "msg");
      Assert.IsTrue(message.Value.IsNone());
      Assert.DoesNotThrow(() => message.Value.SetString(null));
      Assert.IsTrue(string.IsNullOrEmpty(message.Value.GetString()));
      message.Value.SetString("Hello");
      Assert.AreEqual(SHType.String, message.Value.type);

      using var result = new ExternalVariable(Wire, "result", SHType.String);
      Assert.AreEqual(SHType.String, result.Value.type);
      ScheduleWire();
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
      _wire = new Variable();
      var ok = Env.Eval(@$"(Wire ""{nameof(TestGetString)}"" ""こんにちは"" > .msg (Log) (Pause) ""世界"" > .msg (Log))", _wire.Ptr);
      Assert.IsTrue(ok);
      Assert.IsTrue(Wire.IsValid());

      using var message = new ExternalVariable(Wire, "msg", SHType.String);
      Assert.AreEqual(SHType.String, message.Value.type);

      ScheduleWire();

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
