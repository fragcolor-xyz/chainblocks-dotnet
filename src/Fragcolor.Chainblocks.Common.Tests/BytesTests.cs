/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Fragcolor.Chainblocks.Tests
{
  /// <summary>
  /// Tests for bytes manipulations.
  /// </summary>
  [TestFixture]
  internal sealed class BytesTests : TestBase
  {
    [TearDown]
    public void TearDown()
    {
      UnscheduleChain();
      _chain?.Dispose();
    }

    [Test]
    public void TestSetBytes()
    {
      var name = TestContext.CurrentContext.Test.Name;
      _chain = new Variable();
      var ok = Env.Eval(@$"(Chain ""{name}"" .input (Log) (ToBase64) (Log) > .output)", _chain.Ptr);
      Assert.IsTrue(ok);
      Assert.IsTrue(Chain.IsValid());

      const string str = "Hello World!";
      var bytes = Encoding.ASCII.GetBytes(str);

      using var inputVar = new ExternalVariable(Chain, "input");
      Assert.IsTrue(inputVar.Value.IsNone());
      Assert.Throws(typeof(ArgumentNullException), () => inputVar.Value.SetBytes(null!));
      Assert.IsNull(inputVar.Value.GetBytes());
      inputVar.Value.SetBytes(bytes);
      using var outputVar = new ExternalVariable(Chain, "output", CBType.String);

      ScheduleChain();
      Tick();

      var base64 = outputVar.Value.GetString()!;
      Assert.IsNotNull(base64);
      var someBytes = Convert.FromBase64String(base64);
      Assert.IsTrue(bytes.SequenceEqual(someBytes));
      Assert.AreEqual(str, Encoding.ASCII.GetString(someBytes));
    }

    [Test]
    public void TestGetBytes()
    {
      var name = TestContext.CurrentContext.Test.Name;
      _chain = new Variable();
      var ok = Env.Eval(@$"(Chain ""{name}"" .input (Log) (FromBase64) (Log) > .output)", _chain.Ptr);
      Assert.IsTrue(ok);
      Assert.IsTrue(Chain.IsValid());

      const string str = "Hello World!";
      var bytes = Encoding.ASCII.GetBytes(str);

      using var inputVar = new ExternalVariable(Chain, "input");
      inputVar.Value.SetString(Convert.ToBase64String(bytes));
      using var outputVar = new ExternalVariable(Chain, "output", CBType.Bytes);

      ScheduleChain();
      Tick();

      var someBytes = outputVar.Value.GetBytes()!;
      Assert.IsNotNull(someBytes);
      Assert.IsTrue(bytes.SequenceEqual(someBytes));
      Assert.AreEqual(str, Encoding.ASCII.GetString(someBytes));
    }
  }
}
