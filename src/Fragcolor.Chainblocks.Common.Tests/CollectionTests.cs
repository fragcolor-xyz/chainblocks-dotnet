/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;

using Fragcolor.Chainblocks.Collections;

using NUnit.Framework;

namespace Fragcolor.Chainblocks.Tests
{
  [TestFixture]
  internal sealed class CollectionTests : TestBase
  {
#pragma warning disable CS8618
    private ExternalVariable _collectionVar;
#pragma warning restore CS8618

    public ref CBVar ColVar => ref _collectionVar.Value;

    [SetUp]
    public void Setup()
    {
      var name = TestContext.CurrentContext.Test.Name;
      _chain = new Variable();
      var ok = Env.Eval(@$"(Chain ""{name}"" :Looped .collection (Log))", _chain.Ptr);
      Assert.IsTrue(ok);
      Assert.IsTrue(Chain.IsValid());

      _collectionVar = new ExternalVariable(Chain, "collection");
    }

    [TearDown]
    public void TearDown()
    {
      UnscheduleChain();
      _collectionVar.Dispose();
      _chain.Dispose();
    }

    [Test]
    public void TestSeq()
    {
      ColVar.type = CBType.Seq;

      ScheduleChain();

      using var @float = VariableUtil.NewFloat(42);

      Assert.AreEqual(0, ColVar.seq.Size());
      Tick();

      ColVar.seq.Push(ref @float.Value);
      Assert.AreEqual(1, ColVar.seq.Size());
      Tick();

      using var float4 = VariableUtil.NewFloat4(new() { y = 5 });
      Assert.Throws(typeof(IndexOutOfRangeException), () => ColVar.seq.Insert(2, ref float4.Value));
      ColVar.seq.Insert(0, ref float4.Value);
      Assert.AreEqual(2, ColVar.seq.Size());
      Tick();

      ref var myvar = ref ColVar.seq.At(0);
      myvar.float4.z = 42;
      Tick();

      ColVar.seq.RemoveAt(0);
      Assert.Throws(typeof(IndexOutOfRangeException), () => ColVar.seq.RemoveAt(1));
      Assert.AreEqual(1, ColVar.seq.Size());
      Tick();

      var popped = ColVar.seq.Pop();
      Assert.Throws(typeof(InvalidOperationException), () => ColVar.seq.Pop());
      Assert.AreEqual(CBType.Float, popped.type);
      Assert.AreEqual(42, popped.@float);
      Assert.AreEqual(0, ColVar.seq.Size());
      Tick();

      Assert.DoesNotThrow(() => ColVar.seq.Insert(0, ref @float.Value));
      Assert.AreEqual(1, ColVar.seq.Size());
      Assert.AreEqual(@float.Value, ColVar.seq[0]);
      Assert.Throws(typeof(IndexOutOfRangeException), () => _ = ColVar.seq[1]);
      Tick();
    }

    [Test]
    public void TestSet()
    {
      ColVar.set = CBSet.New();
      ColVar.type = CBType.Set;

      ScheduleChain();

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

    [Test]
    public void TestTable()
    {
      ColVar.table = CBTable.New();
      ColVar.type = CBType.Table;

      ScheduleChain();

      using var color = VariableUtil.NewColor(new() { r = 255 });

      Assert.AreEqual(0, ColVar.table.Size());
      Tick();

      Assert.IsFalse(ColVar.table.Contains("red"));
      ref var elem = ref ColVar.table.At("red");
      Assert.IsTrue(elem.IsNone());
      elem.SetValue(color.Value.color);
      Assert.AreEqual(1, ColVar.table.Size());
      Assert.IsTrue(ColVar.table.Contains("red"));
      Tick();

      elem.float3.z = 42;
      var iterator = ColVar.table.GetIterator();
      Assert.IsTrue(ColVar.table.Next(ref iterator, out var key, out var value));
      Assert.IsFalse(ColVar.table.Next(ref iterator, out _, out _));
      Assert.AreEqual("red", key);
      Assert.AreEqual(42, value.float3.z);
      Tick();

      ColVar.table.Remove("red");
      Assert.AreEqual(0, ColVar.table.Size());
      Tick();

      Assert.IsFalse(ColVar.table.Contains("none"));
      _ = ref ColVar.table.At("none");
      Assert.IsTrue(ColVar.table.Contains("none"));
      Tick();

      ColVar.table.Clear();
      Assert.AreEqual(0, ColVar.table.Size());
      Tick();
    }
  }
}
