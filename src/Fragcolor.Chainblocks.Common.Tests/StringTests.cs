/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using NUnit.Framework;

namespace Fragcolor.Chainblocks.Tests
{
  [TestFixture]
  internal sealed class StringTests : TestBase
  {
    [TearDown]
    public void TearDown()
    {
      UnscheduleChain();
      _chain.Dispose();
    }

    [Test]
    public void TestSetString()
    {
      _chain = new Variable();
      var ok = Env.Eval(@$"(Chain ""{nameof(TestSetString)}"" :Looped .msg (Log) (String.ToUpper) > .result)", _chain.Ptr);
      Assert.IsTrue(ok);

      using var message = new ExternalVariable(Chain, "msg");
      Assert.IsTrue(message.Value.IsNone());
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

    [Test]
    public void TestGetString()
    {
      _chain = new Variable();
      var ok = Env.Eval(@$"(Chain ""{nameof(TestGetString)}"" ""Hello"" > .msg (Log) (Pause) ""World"" > .msg (Log))", _chain.Ptr);
      Assert.IsTrue(ok);

      using var message = new ExternalVariable(Chain, "msg", CBType.String);
      Assert.AreEqual(CBType.String, message.Value.type);

      ScheduleChain();

      Tick();
      Assert.AreEqual("Hello", message.Value.GetString());

      Tick();
      Assert.AreEqual("World", message.Value.GetString());
    }
  }
}
