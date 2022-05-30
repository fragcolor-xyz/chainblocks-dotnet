/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using NUnit.Framework;

namespace Fragcolor.Shards.Tests
{
  internal sealed class VariableTests : TestBase
  {
    /// <summary>
    /// Tests cloning a <see cref="SHType.Bool"/> variable.
    /// </summary>
    [Test]
    public void TestCloneBool()
    {
      using var var = VariableUtil.NewBool(true);
      using var clone = var.Clone();
      Assert.AreEqual(SHType.Bool, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    /// <summary>
    /// Tests cloning a <see cref="SHType.Int"/> variable.
    /// </summary>
    [Test]
    public void TestCloneInt()
    {
      using var var = VariableUtil.NewInt(42);
      using var clone = var.Clone();
      Assert.AreEqual(SHType.Int, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    /// <summary>
    /// Tests cloning a <see cref="SHType.Int2"/> variable.
    /// </summary>
    [Test]
    public void TestCloneInt2()
    {
      using var var = VariableUtil.NewInt2((-1, 1));
      using var clone = var.Clone();
      Assert.AreEqual(SHType.Int2, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    /// <summary>
    /// Tests cloning a <see cref="SHType.Int3"/> variable.
    /// </summary>
    [Test]
    public void TestCloneInt3()
    {
      using var var = VariableUtil.NewInt3((1, 2, 3));
      using var clone = var.Clone();
      Assert.AreEqual(SHType.Int3, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    /// <summary>
    /// Tests cloning a <see cref="SHType.Int4"/> variable.
    /// </summary>
    [Test]
    public void TestCloneInt4()
    {
      using var var = VariableUtil.NewInt4((1, 2, 3, 4));
      using var clone = var.Clone();
      Assert.AreEqual(SHType.Int4, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    /// <summary>
    /// Tests cloning a <see cref="SHType.Int8"/> variable.
    /// </summary>
    [Test]
    public void TestCloneInt8()
    {
      using var var = VariableUtil.NewInt8(((1, 2, 3, 4), (5, 6, 7, 8)));
      using var clone = var.Clone();
      Assert.AreEqual(SHType.Int8, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    /// <summary>
    /// Tests cloning a <see cref="SHType.Int16"/> variable.
    /// </summary>
    [Test]
    public void TestCloneInt16()
    {
      using var var = VariableUtil.NewInt16(((1, 2, 3, 4), (5, 6, 7, 8), (1, 2, 3, 4), (5, 6, 7, 8)));
      using var clone = var.Clone();
      Assert.AreEqual(SHType.Int16, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    /// <summary>
    /// Tests cloning a <see cref="SHType.Float"/> variable.
    /// </summary>
    [Test]
    public void TestCloneFloat()
    {
      using var var = VariableUtil.NewFloat(Math.PI);
      using var clone = var.Clone();
      Assert.AreEqual(SHType.Float, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    /// <summary>
    /// Tests cloning a <see cref="SHType.Float2"/> variable.
    /// </summary>
    [Test]
    public void TestCloneFloat2()
    {
      using var var = VariableUtil.NewFloat2((1.0, 2.0));
      using var clone = var.Clone();
      Assert.AreEqual(SHType.Float2, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    /// <summary>
    /// Tests cloning a <see cref="SHType.Float3"/> variable.
    /// </summary>
    [Test]
    public void TestCloneFloat3()
    {
      using var var = VariableUtil.NewFloat3((1.0f, 2.0f, 3.0f));
      using var clone = var.Clone();
      Assert.AreEqual(SHType.Float3, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    /// <summary>
    /// Tests cloning a <see cref="SHType.Float4"/> variable.
    /// </summary>
    [Test]
    public void TestCloneFloat4()
    {
      using var var = VariableUtil.NewFloat4((1.0f, 2.0f, 3.0f, 4.0f));
      using var clone = var.Clone();
      Assert.AreEqual(SHType.Float4, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    /// <summary>
    /// Tests cloning a <see cref="SHType.Color"/> variable.
    /// </summary>
    [Test]
    public void TestCloneColor()
    {
      using var var = VariableUtil.NewColor((255, 50, 125, 255));
      using var clone = var.Clone();
      Assert.AreEqual(SHType.Color, clone.Value.type);
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
    }

    /// <summary>
    /// Tests cloning an external variable.
    /// </summary>
    [Test]
    public void TestCloneExternalVariable()
    {
      using var wire = new Variable();
      Env.Eval(@"(Wire ""empty"" .var (Log))", wire.Ptr);
      using var var = new ExternalVariable(wire.Value.wire, "var");
      var.Value.SetValue((Int4)(1, 2, 3, 4));
      using var clone = var.Clone();
      Assert.AreEqual(SHType.Int4, clone.Value.type);
      // note: external flag is not duplicated
      // so we force setting it here in order to compare, and then we restore it
      var flags = clone.Value.flags;
      clone.Value.flags = CBVarFlags.External;
      Assert.AreEqual(var.Value, clone.Value);
      Assert.AreNotSame(var.Value, clone.Value);
      clone.Value.flags = flags;
    }

    /// <summary>
    /// Tests the destructor of <see cref="ExternalVariable"/>;
    /// </summary>
    /// <remarks>
    /// This is for completion purpose.
    /// Consummer code is expected to properly dispose of it so that the finalizer doesn't have to be called.
    /// </remarks>
    [Test]
    public void TestExternalVariableDestructor()
    {
      var wire = new Variable();
      Env.Eval(@"(Wire ""empty"" .var (Log))", wire.Ptr);
      {
        _ = new ExternalVariable(wire.Value.wire, "var");
      }
      // destructor eventualy called when out of scope
      GC.Collect();
      GC.WaitForPendingFinalizers();
    }

    /// <summary>
    /// Tests the destructor of <see cref="Variable"/>;
    /// </summary>
    /// <remarks>
    /// This is for completion purpose.
    /// Consummer code is expected to properly dispose of it so that the finalizer doesn't have to be called.
    /// </remarks>
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
