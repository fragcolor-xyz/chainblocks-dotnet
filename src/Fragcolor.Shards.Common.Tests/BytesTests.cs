/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Fragcolor.Shards.Tests
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
      UnscheduleWire();
      _wire?.Dispose();
    }

    [Test]
    public void TestSetBytes()
    {
      var name = TestContext.CurrentContext.Test.Name;
      _wire = new Variable();
      var ok = Env.Eval(@$"(Wire ""{name}"" .input (Log) (ToBase64) (Log) > .output)", _wire.Ptr);
      Assert.IsTrue(ok);
      Assert.IsTrue(Wire.IsValid());

      const string str = "Hello World!";
      var bytes = Encoding.ASCII.GetBytes(str);

      using var inputVar = new ExternalVariable(Wire, "input");
      Assert.IsTrue(inputVar.Value.IsNone());
      Assert.Throws(typeof(ArgumentNullException), () => inputVar.Value.SetBytes(null!));
      Assert.IsNull(inputVar.Value.GetBytes());
      inputVar.Value.SetBytes(bytes);
      using var outputVar = new ExternalVariable(Wire, "output", SHType.String);

      ScheduleWire();
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
      _wire = new Variable();
      var ok = Env.Eval(@$"(Wire ""{name}"" .input (Log) (FromBase64) (Log) > .output)", _wire.Ptr);
      Assert.IsTrue(ok);
      Assert.IsTrue(Wire.IsValid());

      const string str = "Hello World!";
      var bytes = Encoding.ASCII.GetBytes(str);

      using var inputVar = new ExternalVariable(Wire, "input");
      inputVar.Value.SetString(Convert.ToBase64String(bytes));
      using var outputVar = new ExternalVariable(Wire, "output", SHType.Bytes);

      ScheduleWire();
      Tick();

      var someBytes = outputVar.Value.GetBytes()!;
      Assert.IsNotNull(someBytes);
      Assert.IsTrue(bytes.SequenceEqual(someBytes));
      Assert.AreEqual(str, Encoding.ASCII.GetString(someBytes));
    }
  }
}
