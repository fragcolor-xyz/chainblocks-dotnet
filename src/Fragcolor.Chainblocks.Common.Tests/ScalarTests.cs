/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;

using NUnit.Framework;

namespace Fragcolor.Chainblocks.Tests
{
  [TestFixture]
  internal sealed class ScalarTests : TestBase
  {
#pragma warning disable CS8618
    private ExternalVariable _inputVar;
    private ExternalVariable _outputVar;
#pragma warning restore CS8618

    public ref CBVar InVar => ref _inputVar.Value;

    public ref CBVar OutVar => ref _outputVar.Value;

    [SetUp]
    public void Setup()
    {
      var name = TestContext.CurrentContext.Test.Name;
      _chain = new Variable();
      var ok = Env.Eval(@$"(Chain ""{name}"" :Looped .input (Log) > .output)", _chain.Ptr);
      Assert.IsTrue(ok);

      _inputVar = new ExternalVariable(Chain, "input");
      _outputVar = new ExternalVariable(Chain, "output");
    }

    [TearDown]
    public void TearDown()
    {
      UnscheduleChain();
      _inputVar.Dispose();
      _outputVar.Dispose();
      _chain.Dispose();
    }

    [Test]
    public void TestBool()
    {
      InVar.type = CBType.Bool;
      OutVar.type = CBType.Bool;

      InVar.@bool = false;
      ScheduleChain();

      Tick();
      Assert.AreEqual(InVar.@bool, OutVar.@bool);
      Assert.AreEqual(InVar, OutVar);

      InVar.@bool = true;
      Assert.AreNotEqual(InVar, OutVar);

      Tick();
      Assert.AreEqual(InVar.@bool, OutVar.@bool);
      Assert.AreEqual(InVar, OutVar);
    }

    [Test]
    public void TestInt()
    {
      InVar.type = CBType.Int;
      OutVar.type = CBType.Int;

      InVar.@int = 21;
      ScheduleChain();

      Tick();
      Assert.AreEqual(InVar.@int, OutVar.@int);
      Assert.AreEqual(InVar, OutVar);

      InVar.@int = 42;
      Assert.AreNotEqual(InVar, OutVar);

      Tick();
      Assert.AreEqual(InVar.@int, OutVar.@int);
      Assert.AreEqual(InVar, OutVar);
    }

    [Test]
    public void TestInt2()
    {
      InVar.type = CBType.Int2;
      OutVar.type = CBType.Int2;

      InVar.int2 = new() { x = 1, y = 2 };
      ScheduleChain();

      Tick();
      Assert.AreEqual(InVar.int2, OutVar.int2);
      Assert.AreEqual(InVar, OutVar);

      InVar.int2.y = 4;
      Assert.AreNotEqual(InVar, OutVar);

      Tick();
      Assert.AreEqual(InVar.int2, OutVar.int2);
      Assert.AreEqual(InVar, OutVar);
    }

    [Test]
    public void TestInt3()
    {
      InVar.type = CBType.Int3;
      OutVar.type = CBType.Int3;

      InVar.int3 = new() { x = 1, y = 2, z = 3 };
      ScheduleChain();

      Tick();
      Assert.AreEqual(InVar.int3, OutVar.int3);
      Assert.AreEqual(InVar, OutVar);

      InVar.int3.z = 6;
      Assert.AreNotEqual(InVar, OutVar);

      Tick();
      Assert.AreEqual(InVar.int3, OutVar.int3);
      Assert.AreEqual(InVar, OutVar);
    }

    [Test]
    public void TestInt4()
    {
      InVar.type = CBType.Int4;
      OutVar.type = CBType.Int4;

      InVar.int4 = new() { x = 1, y = 2, z = 3, w = 4 };
      ScheduleChain();

      Tick();
      Assert.AreEqual(InVar.int4, OutVar.int4);
      Assert.AreEqual(InVar, OutVar);

      InVar.int4.w = 8;
      Assert.AreNotEqual(InVar, OutVar);

      Tick();
      Assert.AreEqual(InVar.int4, OutVar.int4);
      Assert.AreEqual(InVar, OutVar);
    }

    [Test]
    public void TestInt8()
    {
      InVar.type = CBType.Int8;
      OutVar.type = CBType.Int8;

      InVar.int8 = new()
      {
        x1 = 1,
        y1 = 2,
        z1 = 3,
        w1 = 4,
        x2 = 5,
        y2 = 6,
        z2 = 7,
        w2 = 8
      };
      ScheduleChain();

      Tick();
      Assert.AreEqual(InVar.int8, OutVar.int8);
      Assert.AreEqual(InVar, OutVar);

      InVar.int8.z1 = 6;
      InVar.int8.w2 = -16;
      Assert.AreNotEqual(InVar, OutVar);

      Tick();
      Assert.AreEqual(InVar.int8, OutVar.int8);
      Assert.AreEqual(InVar, OutVar);
    }

    [Test]
    public void TestInt16()
    {
      InVar.type = CBType.Int16;
      OutVar.type = CBType.Int16;

      InVar.int16 = new()
      {
        x1 = 1,
        y1 = 2,
        z1 = 3,
        w1 = 4,
        x2 = 5,
        y2 = 6,
        z2 = 7,
        w2 = 8,
        x3 = 9,
        y3 = 10,
        z3 = 11,
        w3 = 12,
        x4 = 13,
        y4 = 14,
        z4 = 15,
        w4 = 16,
      };
      ScheduleChain();

      Tick();
      Assert.AreEqual(InVar.int16, OutVar.int16);
      Assert.AreEqual(InVar, OutVar);

      InVar.int16.x1 = 2;
      InVar.int16.y2 = 12;
      InVar.int16.z3 = 22;
      InVar.int16.w4 = 32;
      Assert.AreNotEqual(InVar, OutVar);

      Tick();
      Assert.AreEqual(InVar.int16, OutVar.int16);
      Assert.AreEqual(InVar, OutVar);
    }

    [Test]
    public void TestFloat()
    {
      InVar.type = CBType.Float;
      OutVar.type = CBType.Float;

      InVar.@float = Math.PI;
      ScheduleChain();

      Tick();
      Assert.AreEqual(InVar.@float, OutVar.@float);
      Assert.AreEqual(InVar, OutVar);

      InVar.@float = Math.Sqrt(2);
      Assert.AreNotEqual(InVar, OutVar);

      Tick();
      Assert.AreEqual(InVar.@float, OutVar.@float);
      Assert.AreEqual(InVar, OutVar);
    }

    [Test]
    public void TestFloat2()
    {
      InVar.type = CBType.Float2;
      OutVar.type = CBType.Float2;

      InVar.float2 = new() { x = Math.Acos(0), y = Math.Asin(-1)};
      ScheduleChain();

      Tick();
      Assert.AreEqual(InVar.float2, OutVar.float2);
      Assert.AreEqual(InVar, OutVar);

      InVar.float2.x = Math.Atan(1);
      Assert.AreNotEqual(InVar, OutVar);

      Tick();
      Assert.AreEqual(InVar.float2, OutVar.float2);
      Assert.AreEqual(InVar, OutVar);
    }

    [Test]
    public void TestFloat3()
    {
      InVar.type = CBType.Float3;
      OutVar.type = CBType.Float3;

      InVar.float3 = new() { x = (float)Math.Acos(0), y = (float)Math.Asin(-1) };
      ScheduleChain();

      Tick();
      Assert.AreEqual(InVar.float2, OutVar.float2);
      Assert.AreEqual(InVar, OutVar);

      InVar.float3.z = (float)Math.Atan(1);
      Assert.AreNotEqual(InVar, OutVar);

      Tick();
      Assert.AreEqual(InVar.float2, OutVar.float2);
      Assert.AreEqual(InVar, OutVar);
    }

    [Test]
    public void TestFloat4()
    {
      InVar.type = CBType.Float4;
      OutVar.type = CBType.Float4;

      InVar.float4 = new() { x = (float)Math.Acos(0), y = (float)Math.Asin(-1) };
      ScheduleChain();

      Tick();
      Assert.AreEqual(InVar.float2, OutVar.float2);
      Assert.AreEqual(InVar, OutVar);

      InVar.float4.z = (float)Math.Atan(1);
      InVar.float4.w = (float)Math.Atan(2);
      Assert.AreNotEqual(InVar, OutVar);

      Tick();
      Assert.AreEqual(InVar.float2, OutVar.float2);
      Assert.AreEqual(InVar, OutVar);
    }

    [Test]
    public void TestColor()
    {
      InVar.type = CBType.Color;
      OutVar.type = CBType.Color;

      InVar.color = new() { r = 33, g = 33, b = 33, a = 255 };
      ScheduleChain();

      Tick();
      Assert.AreEqual(InVar.color, OutVar.color);
      Assert.AreEqual(InVar, OutVar);

      InVar.color = new() { r = 212, g = 175, b = 55, a = 255 };
      Assert.AreNotEqual(InVar, OutVar);

      Tick();
      Assert.AreEqual(InVar.color, OutVar.color);
      Assert.AreEqual(InVar, OutVar);
    }
  }
}
