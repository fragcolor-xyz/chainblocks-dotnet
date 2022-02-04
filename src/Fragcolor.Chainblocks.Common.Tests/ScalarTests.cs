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
      var @bool = true;

      InVar.SetValue(@bool);
      Assert.IsFalse(InVar.IsFloat());
      Assert.IsFalse(InVar.IsInteger());
      Assert.AreEqual(CBType.Bool, InVar.type);
      Assert.AreEqual(@bool, (bool)InVar.@bool);

      OutVar.SetValue(default(bool));
      Assert.AreEqual(CBType.Bool, OutVar.type);
      Assert.AreNotEqual(InVar.@bool, OutVar.@bool);
      Assert.AreNotEqual(InVar, OutVar);

      ScheduleChain();

      Tick();
      Assert.AreEqual(InVar.@bool, OutVar.@bool);
      Assert.AreEqual(InVar, OutVar);

      InVar.@bool = false;
      Assert.AreNotEqual(InVar, OutVar);

      Tick();
      Assert.AreEqual(InVar.@bool, OutVar.@bool);
      Assert.AreEqual(InVar, OutVar);
    }

    [Test]
    public void TestInt()
    {
      var @int = 21;

      InVar.SetValue(@int);
      Assert.IsFalse(InVar.IsFloat());
      Assert.IsTrue(InVar.IsInteger());
      Assert.AreEqual(CBType.Int, InVar.type);
      Assert.AreEqual(@int, InVar.@int);

      OutVar.SetValue(default(int));
      Assert.AreEqual(CBType.Int, OutVar.type);
      Assert.AreNotEqual(InVar.@int, OutVar.@int);
      Assert.AreNotEqual(InVar, OutVar);

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
      var int2 = new Int2() { x = 1, y = 2 };
      var (x, y) = int2;
      Assert.AreEqual(1, x);
      Assert.AreEqual(2, y);

      InVar.SetValue(int2);
      Assert.IsFalse(InVar.IsFloat());
      Assert.IsTrue(InVar.IsInteger());
      Assert.AreEqual(CBType.Int2, InVar.type);
      Assert.AreEqual(int2, InVar.int2);

      OutVar.SetValue(default(Int2));
      Assert.AreEqual(CBType.Int2, OutVar.type);
      Assert.AreNotEqual(InVar.int2, OutVar.int2);
      Assert.AreNotEqual(InVar, OutVar);

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
      var int3 = new Int3() { x = 1, y = 2, z = 3 };
      var (x, y, z) = int3;
      Assert.AreEqual(1, x);
      Assert.AreEqual(2, y);
      Assert.AreEqual(3, z);

      InVar.SetValue(int3);
      Assert.IsFalse(InVar.IsFloat());
      Assert.IsTrue(InVar.IsInteger());
      Assert.AreEqual(CBType.Int3, InVar.type);
      Assert.AreEqual(int3, InVar.int3);

      OutVar.SetValue(default(Int3));
      Assert.AreEqual(CBType.Int3, OutVar.type);
      Assert.AreNotEqual(InVar.int3, OutVar.int3);
      Assert.AreNotEqual(InVar, OutVar);

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
      var int4 = new Int4() { x = 1, y = 2, z = 3, w = 4 };
      var (x, y, z, w) = int4;
      Assert.AreEqual(1, x);
      Assert.AreEqual(2, y);
      Assert.AreEqual(3, z);
      Assert.AreEqual(4, w);

      InVar.SetValue(int4);
      Assert.IsFalse(InVar.IsFloat());
      Assert.IsTrue(InVar.IsInteger());
      Assert.AreEqual(CBType.Int4, InVar.type);
      Assert.AreEqual(int4, InVar.int4);

      OutVar.SetValue(default(Int4));
      Assert.AreEqual(CBType.Int4, OutVar.type);
      Assert.AreNotEqual(InVar.int4, OutVar.int4);
      Assert.AreNotEqual(InVar, OutVar);

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
      var int8 = new Int8()
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
      var ((x1, y1, z1, w1), (x2, y2, z2, w2)) = int8;
      Assert.AreEqual(1, x1);
      Assert.AreEqual(2, y1);
      Assert.AreEqual(3, z1);
      Assert.AreEqual(4, w1);
      Assert.AreEqual(5, x2);
      Assert.AreEqual(6, y2);
      Assert.AreEqual(7, z2);
      Assert.AreEqual(8, w2);

      InVar.SetValue(int8);
      Assert.IsFalse(InVar.IsFloat());
      Assert.IsTrue(InVar.IsInteger());
      Assert.AreEqual(CBType.Int8, InVar.type);
      Assert.AreEqual(int8, InVar.int8);

      OutVar.SetValue(default(Int8));
      Assert.AreEqual(CBType.Int8, OutVar.type);
      Assert.AreNotEqual(InVar.int8, OutVar.int8);
      Assert.AreNotEqual(InVar, OutVar);

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
      var int16 = new Int16()
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
      var ((x1,y1,z1,w1), (x2, y2, z2, w2), (x3, y3, z3, w3), (x4, y4, z4, w4)) = int16;
      Assert.AreEqual(1, x1);
      Assert.AreEqual(2, y1);
      Assert.AreEqual(3, z1);
      Assert.AreEqual(4, w1);
      Assert.AreEqual(5, x2);
      Assert.AreEqual(6, y2);
      Assert.AreEqual(7, z2);
      Assert.AreEqual(8, w2);
      Assert.AreEqual(9, x3);
      Assert.AreEqual(10, y3);
      Assert.AreEqual(11, z3);
      Assert.AreEqual(12, w3);
      Assert.AreEqual(13, x4);
      Assert.AreEqual(14, y4);
      Assert.AreEqual(15, z4);
      Assert.AreEqual(16, w4);

      InVar.SetValue(int16);
      Assert.IsFalse(InVar.IsFloat());
      Assert.IsTrue(InVar.IsInteger());
      Assert.AreEqual(CBType.Int16, InVar.type);
      Assert.AreEqual(int16, InVar.int16);

      OutVar.SetValue(default(Int16));
      Assert.AreEqual(CBType.Int16, OutVar.type);
      Assert.AreNotEqual(InVar.int16, OutVar.int16);
      Assert.AreNotEqual(InVar, OutVar);

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
      var @float = Math.PI;

      InVar.SetValue(@float);
      Assert.IsTrue(InVar.IsFloat());
      Assert.IsFalse(InVar.IsInteger());
      Assert.AreEqual(CBType.Float, InVar.type);
      Assert.AreEqual(@float, InVar.@float);

      OutVar.SetValue(default(double));
      Assert.AreEqual(CBType.Float, OutVar.type);
      Assert.AreNotEqual(InVar.@float, OutVar.@float);
      Assert.AreNotEqual(InVar, OutVar);

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
      var float2 = new Float2() { x = Math.Acos(0), y = Math.Asin(-1) };
      var (x, y) = float2;
      Assert.AreEqual(Math.Acos(0), x);
      Assert.AreEqual(Math.Asin(-1), y);

      InVar.SetValue(float2);
      Assert.IsTrue(InVar.IsFloat());
      Assert.IsFalse(InVar.IsInteger());
      Assert.AreEqual(CBType.Float2, InVar.type);
      Assert.AreEqual(float2, InVar.float2);

      OutVar.SetValue(default(Float2));
      Assert.AreEqual(CBType.Float2, OutVar.type);
      Assert.AreNotEqual(InVar.float2, OutVar.float2);
      Assert.AreNotEqual(InVar, OutVar);

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
      var float3 = new Float3() { x = (float)Math.Acos(0), y = (float)Math.Asin(-1), z = 1.0f };
      var (x, y, z) = float3;
      Assert.AreEqual((float)Math.Acos(0), x);
      Assert.AreEqual((float)Math.Asin(-1), y);
      Assert.AreEqual(1.0f, z);

      InVar.SetValue(float3);
      Assert.IsTrue(InVar.IsFloat());
      Assert.IsFalse(InVar.IsInteger());
      Assert.AreEqual(CBType.Float3, InVar.type);
      Assert.AreEqual(float3, InVar.float3);

      OutVar.SetValue(default(Float3));
      Assert.AreEqual(CBType.Float3, OutVar.type);
      Assert.AreNotEqual(InVar.float3, OutVar.float3);
      Assert.AreNotEqual(InVar, OutVar);

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
      var float4 = new Float4() { x = (float)Math.Acos(0), y = (float)Math.Asin(-1), z = 1, w = -1 };
      var (x, y, z, w) = float4;
      Assert.AreEqual((float)Math.Acos(0), x);
      Assert.AreEqual((float)Math.Asin(-1), y);
      Assert.AreEqual(1, z);
      Assert.AreEqual(-1, w);

      InVar.SetValue(float4);
      Assert.IsTrue(InVar.IsFloat());
      Assert.IsFalse(InVar.IsInteger());
      Assert.AreEqual(CBType.Float4, InVar.type);
      Assert.AreEqual(float4, InVar.float4);

      OutVar.SetValue(default(Float4));
      Assert.AreEqual(CBType.Float4, OutVar.type);
      Assert.AreNotEqual(InVar.float4, OutVar.float4);
      Assert.AreNotEqual(InVar, OutVar);

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
      var color = new CBColor() { r = 33, g = 34, b = 35, a = 255 };
      var (r, g, b, a) = color;
      Assert.AreEqual(33, r);
      Assert.AreEqual(34, g);
      Assert.AreEqual(35, b);
      Assert.AreEqual(255, a);

      InVar.SetValue(color);
      Assert.IsFalse(InVar.IsFloat());
      Assert.IsTrue(InVar.IsInteger());
      Assert.AreEqual(CBType.Color, InVar.type);
      Assert.AreEqual(color, InVar.color);

      OutVar.SetValue(default(CBColor));
      Assert.AreEqual(CBType.Color, OutVar.type);
      Assert.AreNotEqual(InVar.color, OutVar.color);
      Assert.AreNotEqual(InVar, OutVar);

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
