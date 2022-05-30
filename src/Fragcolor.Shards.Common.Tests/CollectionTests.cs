/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using Fragcolor.Shards.Collections;
using NUnit.Framework;

namespace Fragcolor.Shards.Tests
{
  /// <summary>
  /// Tests for colleciton types in shards.
  /// </summary>
  [TestFixture]
  internal sealed class CollectionTests : TestBase
  {
#pragma warning disable CS8618
    private ExternalVariable _collectionVar;
#pragma warning restore CS8618

    public ref SHVar ColVar => ref _collectionVar.Value;

    /// <summary>
    /// Initializes a wire with an external variable to represent the collection being tested.
    /// </summary>
    [SetUp]
    public void Setup()
    {
      var name = TestContext.CurrentContext.Test.Name;
      _wire = new Variable();
      var ok = Env.Eval(@$"(Wire ""{name}"" :Looped .collection (Log))", _wire.Ptr);
      Assert.IsTrue(ok);
      Assert.IsTrue(Wire.IsValid());

      _collectionVar = new ExternalVariable(Wire, "collection");
    }

    /// <summary>
    /// Cleans up the wire and the external variable.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
      UnscheduleWire();
      _collectionVar.Dispose();
      _wire.Dispose();
    }

    /// <summary>
    /// Test the <see cref="Collections.Shards"/> collection type.
    /// </summary>
    [Test]
    public void TestShards()
    {
      var shards = default(Collections.Shards);
      Assert.AreEqual(0, shards.Count);

      var whenShard = Native.Core.CreateShard("When");
      Assert.IsTrue(whenShard.IsValid());
      shards.Push(whenShard);
      Assert.AreEqual(1, shards.Count);

      var ptr = shards.Pop();
      Assert.Throws(typeof(InvalidOperationException), () => shards.Pop());
      Assert.AreEqual(whenShard, ptr);
      Assert.AreEqual(whenShard.AsRef(), ptr.AsRef());
      Assert.AreEqual("When", whenShard.AsRef().Name());
      Assert.AreEqual(0, shards.Count);

      var whenNotShard = Native.Core.CreateShard("WhenNot");
      Assert.IsTrue(whenNotShard.IsValid());
      Assert.Throws(typeof(ArgumentOutOfRangeException), () => shards.Insert(1, whenNotShard));
      shards.Insert(0, whenNotShard);
      Assert.AreEqual(1, shards.Count);
      var elem = shards.At(0);
      Assert.Throws(typeof(ArgumentOutOfRangeException), () => shards.At(1));
      Assert.AreEqual("WhenNot", elem.Name());

      shards.RemoveAt(0);
      Assert.Throws(typeof(ArgumentOutOfRangeException), () => shards.RemoveAt(0));
      Assert.AreEqual(0, shards.Count);
      shards.Push(ref whenShard.AsRef());
      shards.Insert(0, ref whenNotShard.AsRef());
      Assert.AreEqual("WhenNot", shards.At(0).Name());
      Assert.AreEqual("When", shards[1].Name());
      Assert.Throws(typeof(IndexOutOfRangeException), () => _ = shards[2]);
    }

    /// <summary>
    /// Test the <see cref="SHExposedTypesInfo"/> collection type.
    /// </summary>
    [Test]
    public void TestExposedTypeInfos()
    {
      var typeInfos = default(SHExposedTypesInfo);
      Assert.AreEqual(0, typeInfos.Count);

      var boolInfo = default(SHExposedTypeInfo);
      boolInfo._exposedType = new() { _basicType = SHType.Bool };

      typeInfos.Push(ref boolInfo);
      Assert.AreEqual(1, typeInfos.Count);

      var floatInfo = default(SHExposedTypeInfo);
      floatInfo._exposedType = new() { _basicType = SHType.Float };
      Assert.Throws(typeof(ArgumentOutOfRangeException), () => typeInfos.Insert(2, ref floatInfo));
      typeInfos.Insert(0, ref floatInfo);
      Assert.AreEqual(2, typeInfos.Count);

      ref var myInfo = ref typeInfos.At(0);
      Assert.Throws(typeof(ArgumentOutOfRangeException), () => typeInfos.At(2));
      Assert.AreEqual(SHType.Float, myInfo._exposedType.BasicType());

      typeInfos.RemoveAt(0);
      Assert.Throws(typeof(ArgumentOutOfRangeException), () => typeInfos.RemoveAt(1));
      Assert.AreEqual(1, typeInfos.Count);

      var popped = typeInfos.Pop();
      Assert.Throws(typeof(InvalidOperationException), () => typeInfos.Pop());
      Assert.AreEqual(SHType.Bool, popped._exposedType.BasicType());
      Assert.AreEqual(0, typeInfos.Count);

      Assert.DoesNotThrow(() => typeInfos.Insert(0, ref floatInfo));
      Assert.AreEqual(1, typeInfos.Count);
      Assert.AreEqual(floatInfo, typeInfos[0]);
      Assert.Throws(typeof(IndexOutOfRangeException), () => _ = typeInfos[1]);
    }

    /// <summary>
    /// Test the <see cref="SHParametersInfo"/> collection type.
    /// </summary>
    [Test]
    public void TestParameterInfos()
    {
      var paramInfos = default(SHParametersInfo);
      Assert.AreEqual(0, paramInfos.Count);

      var boolInfo = new SHTypeInfo() { _basicType = SHType.Bool };
      var boolParamInfo = default(SHParameterInfo);
      boolParamInfo.Types().Push(ref boolInfo);

      paramInfos.Push(ref boolParamInfo);
      Assert.AreEqual(1, paramInfos.Count);

      var floatInfo = new SHTypeInfo() { _basicType = SHType.Float };
      var floatParamInfo = default(SHParameterInfo);
      floatParamInfo.Types().Push(ref floatInfo);
      Assert.Throws(typeof(ArgumentOutOfRangeException), () => paramInfos.Insert(2, ref floatParamInfo));
      paramInfos.Insert(0, ref floatParamInfo);
      Assert.AreEqual(2, paramInfos.Count);

      ref var myInfo = ref paramInfos.At(0);
      Assert.Throws(typeof(ArgumentOutOfRangeException), () => paramInfos.At(2));
      Assert.AreEqual(1, myInfo.Types().Count);
      Assert.AreEqual(SHType.Float, myInfo.Types()[0].BasicType());

      paramInfos.RemoveAt(0);
      Assert.Throws(typeof(ArgumentOutOfRangeException), () => paramInfos.RemoveAt(1));
      Assert.AreEqual(1, paramInfos.Count);

      var popped = paramInfos.Pop();
      Assert.Throws(typeof(InvalidOperationException), () => paramInfos.Pop());
      Assert.AreEqual(1, popped.Types().Count);
      Assert.AreEqual(SHType.Bool, popped.Types()[0].BasicType());
      Assert.AreEqual(0, paramInfos.Count);

      Assert.DoesNotThrow(() => paramInfos.Insert(0, ref floatParamInfo));
      Assert.AreEqual(1, paramInfos.Count);
      Assert.AreEqual(floatParamInfo, paramInfos[0]);
      Assert.Throws(typeof(IndexOutOfRangeException), () => _ = paramInfos[1]);
    }

    /// <summary>
    /// Test the <see cref="SHSeq"/> collection type.
    /// </summary>
    [Test]
    public void TestSeq()
    {
      ColVar.type = SHType.Seq;

      ScheduleWire();

      using var @float = VariableUtil.NewFloat(42);

      Assert.AreEqual(0, ColVar.seq.Count);
      Tick();

      ColVar.seq.Push(ref @float.Value);
      Assert.AreEqual(1, ColVar.seq.Count);
      Tick();

      using var float4 = VariableUtil.NewFloat4(new() { y = 5 });
      Assert.Throws(typeof(ArgumentOutOfRangeException), () => ColVar.seq.Insert(2, ref float4.Value));
      ColVar.seq.Insert(0, ref float4.Value);
      Assert.AreEqual(2, ColVar.seq.Count);
      Tick();

      ref var myvar = ref ColVar.seq.At(0);
      Assert.Throws(typeof(ArgumentOutOfRangeException), () => ColVar.seq.At(2));
      myvar.float4.z = 42;
      Tick();

      ColVar.seq.RemoveAt(0);
      Assert.Throws(typeof(ArgumentOutOfRangeException), () => ColVar.seq.RemoveAt(1));
      Assert.AreEqual(1, ColVar.seq.Count);
      Tick();

      var popped = ColVar.seq.Pop();
      Assert.Throws(typeof(InvalidOperationException), () => ColVar.seq.Pop());
      Assert.AreEqual(SHType.Float, popped.type);
      Assert.AreEqual(42, popped.@float);
      Assert.AreEqual(0, ColVar.seq.Count);
      Tick();

      Assert.DoesNotThrow(() => ColVar.seq.Insert(0, ref @float.Value));
      Assert.AreEqual(1, ColVar.seq.Count);
      Assert.AreEqual(@float.Value, ColVar.seq[0]);
      Assert.Throws(typeof(IndexOutOfRangeException), () => _ = ColVar.seq[1]);
      Tick();
    }

    /// <summary>
    /// Test the <see cref="SHSet"/> collection type.
    /// </summary>
    [Test]
    public void TestSet()
    {
      ColVar.set = SHSet.New();
      ColVar.type = SHType.Set;

      ScheduleWire();

      using var int3 = VariableUtil.NewInt3(new() { x = 5 });

      Assert.AreEqual(0, ColVar.set.Size());
      Tick();

      Assert.IsTrue(ColVar.set.Include(ref int3.Value));
      Assert.IsTrue(ColVar.set.Contains(ref int3.Value));
      Assert.AreEqual(1, ColVar.set.Size());
      Tick();

      var iterator = ColVar.set.GetIterator();
      Assert.IsTrue(ColVar.set.Next(ref iterator, out var value));
      Assert.AreEqual(value.float3, int3.Value.float3);
      Assert.IsFalse(ColVar.set.Next(ref iterator, out _));
      Assert.IsTrue(ColVar.set.Exclude(ref int3.Value));
      Assert.IsFalse(ColVar.set.Contains(ref int3.Value));
      Assert.AreEqual(0, ColVar.set.Size());
      Tick();

      using var float2 = VariableUtil.NewFloat2(new() { y = 5 });
      Assert.IsTrue(ColVar.set.Include(ref float2.Value));
      Assert.IsTrue(ColVar.set.Contains(ref float2.Value));
      Assert.AreEqual(1, ColVar.set.Size());
      Tick();

      ColVar.set.Clear();
      Assert.AreEqual(0, ColVar.set.Size());
      Tick();
    }

    /// <summary>
    /// Test the <see cref="SHStrings"/> collection type.
    /// </summary>
    [Test]
    public void TestStrings()
    {
      var strings = default(SHStrings);
      Assert.AreEqual(0, strings.Count);
      Tick();

      const string hello = "Привет";

      strings.Push(hello);
      Assert.AreEqual(1, strings.Count);
      Tick();

      const string world = "мир";
      Assert.Throws(typeof(ArgumentOutOfRangeException), () => strings.Insert(2, world));
      strings.Insert(0, world);
      Assert.AreEqual(2, strings.Count);
      Tick();

      var myString = strings.At(0);
      Assert.Throws(typeof(ArgumentOutOfRangeException), () => strings.At(2));
      Assert.AreEqual(world, myString);

      strings.RemoveAt(0);
      Assert.Throws(typeof(ArgumentOutOfRangeException), () => strings.RemoveAt(1));
      Assert.AreEqual(1, strings.Count);

      var popped = strings.Pop();
      Assert.Throws(typeof(InvalidOperationException), () => strings.Pop());
      Assert.AreEqual(hello, popped);
      Assert.AreEqual(0, strings.Count);

      strings.Insert(0, world);
      Assert.AreEqual(1, strings.Count);
      Assert.AreEqual(world, strings[0]);
      Assert.Throws(typeof(IndexOutOfRangeException), () => _ = strings[1]);
    }

    /// <summary>
    /// Test the <see cref="SHTable"/> collection type.
    /// </summary>
    [Test]
    public void TestTable()
    {
      ColVar.table = SHTable.New();
      ColVar.type = SHType.Table;

      ScheduleWire();

      using var color = VariableUtil.NewColor(new() { r = 255 });

      Assert.AreEqual(0, ColVar.table.Size());
      Tick();

      Assert.IsFalse(ColVar.table.Contains("赤色"));
      ref var elem = ref ColVar.table.At("赤色");
      Assert.IsTrue(elem.IsNone());
      elem.SetValue(color.Value.color);
      Assert.AreEqual(1, ColVar.table.Size());
      Assert.IsTrue(ColVar.table.Contains("赤色"));
      Tick();

      elem.float3.z = 42;
      var iterator = ColVar.table.GetIterator();
      Assert.IsTrue(ColVar.table.Next(ref iterator, out var key, out var value));
      Assert.IsFalse(ColVar.table.Next(ref iterator, out _, out _));
      Assert.AreEqual("赤色", key);
      Assert.AreEqual(42, value.float3.z);
      Tick();

      ColVar.table.Remove("赤色");
      Assert.AreEqual(0, ColVar.table.Size());
      Tick();

      Assert.IsFalse(ColVar.table.Contains("none"));
      _ = ColVar.table.At("none");
      Assert.IsTrue(ColVar.table.Contains("none"));
      Tick();

      ColVar.table.Clear();
      Assert.AreEqual(0, ColVar.table.Size());
      Tick();
    }

    /// <summary>
    /// Test the <see cref="SHTypesInfo"/> collection type.
    /// </summary>
    [Test]
    public void TestTypeInfos()
    {
      var typeInfos = default(SHTypesInfo);
      Assert.AreEqual(0, typeInfos.Count);

      var boolInfo = default(SHTypeInfo);
      boolInfo._basicType = SHType.Bool;

      typeInfos.Push(ref boolInfo);
      Assert.AreEqual(1, typeInfos.Count);

      var floatInfo = default(SHTypeInfo);
      floatInfo._basicType = SHType.Float;
      Assert.Throws(typeof(ArgumentOutOfRangeException), () => typeInfos.Insert(2, ref floatInfo));
      typeInfos.Insert(0, ref floatInfo);
      Assert.AreEqual(2, typeInfos.Count);

      ref var myInfo = ref typeInfos.At(0);
      Assert.Throws(typeof(ArgumentOutOfRangeException), () => typeInfos.At(2));
      Assert.AreEqual(SHType.Float, myInfo.BasicType());

      typeInfos.RemoveAt(0);
      Assert.Throws(typeof(ArgumentOutOfRangeException), () => typeInfos.RemoveAt(1));
      Assert.AreEqual(1, typeInfos.Count);

      var popped = typeInfos.Pop();
      Assert.Throws(typeof(InvalidOperationException), () => typeInfos.Pop());
      Assert.AreEqual(SHType.Bool, popped.BasicType());
      Assert.AreEqual(0, typeInfos.Count);

      Assert.DoesNotThrow(() => typeInfos.Insert(0, ref floatInfo));
      Assert.AreEqual(1, typeInfos.Count);
      Assert.AreEqual(floatInfo, typeInfos[0]);
      Assert.Throws(typeof(IndexOutOfRangeException), () => _ = typeInfos[1]);
    }
  }
}
