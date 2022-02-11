/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using NUnit.Framework;

namespace Fragcolor.Chainblocks.Tests
{
  internal sealed class VariableTests : TestBase
  {
    [Test]
    public void TestCloneBool()
    {
      using var var = VariableUtil.NewBool(true);
      using var clone = var.Clone();
      Assert.AreEqual(CBType.Bool, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    [Test]
    public void TestCloneInt()
    {
      using var var = VariableUtil.NewInt(42);
      using var clone = var.Clone();
      Assert.AreEqual(CBType.Int, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    [Test]
    public void TestCloneInt2()
    {
      using var var = VariableUtil.NewInt2((-1, 1));
      using var clone = var.Clone();
      Assert.AreEqual(CBType.Int2, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    [Test]
    public void TestCloneInt3()
    {
      using var var = VariableUtil.NewInt3((1, 2, 3));
      using var clone = var.Clone();
      Assert.AreEqual(CBType.Int3, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    [Test]
    public void TestCloneInt4()
    {
      using var var = VariableUtil.NewInt4((1, 2, 3, 4));
      using var clone = var.Clone();
      Assert.AreEqual(CBType.Int4, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    [Test]
    public void TestCloneInt8()
    {
      using var var = VariableUtil.NewInt8(((1, 2, 3, 4), (5, 6, 7, 8)));
      using var clone = var.Clone();
      Assert.AreEqual(CBType.Int8, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    [Test]
    public void TestCloneInt16()
    {
      using var var = VariableUtil.NewInt16(((1, 2, 3, 4), (5, 6, 7, 8), (1, 2, 3, 4), (5, 6, 7, 8)));
      using var clone = var.Clone();
      Assert.AreEqual(CBType.Int16, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    [Test]
    public void TestCloneFloat()
    {
      using var var = VariableUtil.NewFloat(Math.PI);
      using var clone = var.Clone();
      Assert.AreEqual(CBType.Float, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    [Test]
    public void TestCloneFloat2()
    {
      using var var = VariableUtil.NewFloat2((1.0, 2.0));
      using var clone = var.Clone();
      Assert.AreEqual(CBType.Float2, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    [Test]
    public void TestCloneFloat3()
    {
      using var var = VariableUtil.NewFloat3((1.0f, 2.0f, 3.0f));
      using var clone = var.Clone();
      Assert.AreEqual(CBType.Float3, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    [Test]
    public void TestCloneFloat4()
    {
      using var var = VariableUtil.NewFloat4((1.0f, 2.0f, 3.0f, 4.0f));
      using var clone = var.Clone();
      Assert.AreEqual(CBType.Float4, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    [Test]
    public void TestCloneColor()
    {
      using var var = VariableUtil.NewColor((255, 50, 125, 255));
      using var clone = var.Clone();
      Assert.AreEqual(CBType.Color, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    [Test]
    public void TestCloneExternalVariable()
    {
      using var chain = new Variable();
      Env.Eval(@"(Chain ""empty"" .var (Log))", chain.Ptr);
      using var var = new ExternalVariable(chain.Value.chain, "var");
      var.Value.SetValue((Int4)(1, 2, 3, 4));
      using var clone = var.Clone();
      Assert.AreEqual(CBType.Int4, clone.Value.type);
      // note: external flag is not duplicated
      // so we force setting it here in order to compare, and then we restore it
      var flags = clone.Value.flags;
      clone.Value.flags = CBVarFlags.External;
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
      clone.Value.flags = flags;
    }

    [Test]
    public void TestExternalVariableDestructor()
    {
      var chain = new Variable();
      Env.Eval(@"(Chain ""empty"" .var (Log))", chain.Ptr);
      {
        _ = new ExternalVariable(chain.Value.chain, "var");
      }
      // destructor eventualy called when out of scope
      GC.Collect();
      GC.WaitForPendingFinalizers();
    }

    [Test]
    public void TestVariableDestructor()
    {
      {
        _ = new Variable();
      }
      // destructor eventually called when out of scope
      GC.Collect();
      GC.WaitForPendingFinalizers();
    }
  }
}
