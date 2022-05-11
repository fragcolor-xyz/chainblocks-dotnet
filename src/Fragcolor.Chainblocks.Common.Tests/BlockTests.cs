/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using Fragcolor.Chainblocks.Collections;

using NUnit.Framework;

namespace Fragcolor.Chainblocks.Tests
{
  /// <summary>
  /// Tests for blocks and chain introspection.
  /// </summary>
  [TestFixture]
  internal sealed class BlockTests : TestBase
  {
    /// <summary>
    /// Cleans up the chain.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
      UnscheduleChain();
      _chain?.Dispose();
    }

    /// <summary>
    /// Tests chain introspection.
    /// </summary>
    [Test]
    public void TestInstrospection()
    {
      var name = TestContext.CurrentContext.Test.Name;
      _chain = new Variable();
      var ok = Env.Eval(@$"(Chain ""{name}"" :Looped ""Hello"" > .output (Log))", _chain.Ptr);
      Assert.IsTrue(ok);
      Assert.IsTrue(Chain.IsValid());

      using var var = new ExternalVariable(Chain, "output", CBType.String);

      var info = Native.Core.GetChainInfo(Chain);
      Assert.AreEqual(name, info.Name());
      Assert.IsTrue(info.IsLooped());
      Assert.IsFalse(info.IsRunning());
      Assert.IsFalse(info.IsUnsafe());

      var blocks = info.Blocks();
      Assert.AreEqual(3, blocks.Count);

      {
        ref var block = ref blocks[0];
        Assert.AreEqual("Const", block.Name());
        Assert.AreNotEqual(0, block.Hash());
        var properties = block.Properties();
        Assert.IsFalse(properties.IsValid());
        var inputs = block.InputTypes();
        Assert.AreEqual(1, inputs.Count);
        Assert.AreEqual(CBType.None, inputs[0].BasicType());
        var outputs = block.OutputTypes();
        Assert.AreEqual(1, outputs.Count);
        Assert.AreEqual(CBType.Any, outputs[0].BasicType());
        var parameters = block.Parameters();
        Assert.AreEqual(1, parameters.Count);
        {
          var param = parameters[0];
          Assert.AreEqual("Value", param.Name());
          var paramTypes = param.Types();
          Assert.AreEqual(1, paramTypes.Count);
          Assert.AreEqual(CBType.Any, paramTypes[0].BasicType());
        }
      }

      {
        ref var block = ref blocks[1];
        Assert.AreEqual("Update", block.Name());
        Assert.AreNotEqual(0, block.Hash());
        var inputs = block.InputTypes();
        Assert.AreEqual(1, inputs.Count);
        Assert.AreEqual(CBType.Any, inputs[0].BasicType());
        var outputs = block.OutputTypes();
        Assert.AreEqual(1, outputs.Count);
        Assert.AreEqual(CBType.Any, outputs[0].BasicType());
        var parameters = block.Parameters();
        Assert.AreEqual(3, parameters.Count);
        {
          var param = parameters.At(0);
          Assert.AreEqual("Name", param.Name());
          var paramTypes = param.Types();
          Assert.AreEqual(2, paramTypes.Count);
          Assert.AreEqual(CBType.String, paramTypes[0].BasicType());
          Assert.AreEqual(CBType.ContextVar, paramTypes[1].BasicType());
        }
        {
          var param = parameters.At(1);
          Assert.AreEqual("Key", param.Name());
          var paramTypes = param.Types();
          Assert.AreEqual(3, paramTypes.Count);
          Assert.AreEqual(CBType.String, paramTypes[0].BasicType());
          Assert.AreEqual(CBType.ContextVar, paramTypes[1].BasicType());
          Assert.AreEqual(CBType.None, paramTypes.At(2).BasicType());
        }
        {
          var param = parameters.At(2);
          Assert.AreEqual("Global", param.Name());
          var paramTypes = param.Types();
          Assert.AreEqual(1, paramTypes.Count);
          Assert.AreEqual(CBType.Bool, paramTypes[0].BasicType());
        }
      }

      {
        ref var block = ref blocks.At(2);
        Assert.AreEqual("Log", block.Name());
        Assert.AreNotEqual(0, block.Hash());
        var inputs = block.InputTypes();
        Assert.AreEqual(1, inputs.Count);
        Assert.AreEqual(CBType.Any, inputs[0].BasicType());
        var outputs = block.OutputTypes();
        Assert.AreEqual(1, outputs.Count);
        Assert.AreEqual(CBType.Any, outputs[0].BasicType());
        var parameters = block.Parameters();
        Assert.AreEqual(1, parameters.Count);
        {
          var param = parameters[0];
          Assert.AreEqual("Prefix", param.Name());
          var paramTypes = param.Types();
          Assert.AreEqual(1, paramTypes.Count);
          Assert.AreEqual(CBType.String, paramTypes[0].BasicType());
        }
      }

      ScheduleChain();
      Assert.IsFalse(info.IsRunning());
      info = Native.Core.GetChainInfo(Chain);
      Assert.IsTrue(info.IsRunning());

      Tick();

      UnscheduleChain();
      Assert.IsTrue(info.IsRunning());
      info = Native.Core.GetChainInfo(Chain);
      Assert.IsFalse(info.IsRunning());
    }

    /// <summary>
    /// Tests a block marked as experimental.
    /// </summary>
    [Test, Ignore("Block isn't available yet, after wgpu refactoring")]
    public void TestExperimentalBlock()
    {
      var blockPtr = Native.Core.CreateBlock("Gizmo.CubeView");
      Assert.IsTrue(blockPtr.IsValid());
      ref var block = ref blockPtr.AsRef();
      Assert.AreEqual("Gizmo.CubeView", block.Name());
      Assert.AreNotEqual(0, block.Hash());
      Assert.AreNotEqual(0, block.Help()._crc);
      Assert.AreNotEqual(0, block.InputHelp()._crc);
      Assert.AreNotEqual(0, block.OutputHelp()._crc);
#if DEBUG
      Assert.IsFalse(string.IsNullOrEmpty((string?)block.Help()));
      Assert.IsFalse(string.IsNullOrEmpty((string?)block.InputHelp()));
      Assert.IsFalse(string.IsNullOrEmpty((string?)block.OutputHelp()));
#else
      Assert.IsTrue(string.IsNullOrEmpty((string?)block.Help()));
      Assert.IsTrue(string.IsNullOrEmpty((string?)block.InputHelp()));
      Assert.IsTrue(string.IsNullOrEmpty((string?)block.OutputHelp()));
#endif

      var properties = block.Properties();
      Assert.IsTrue(properties.IsValid());
      Assert.AreEqual(1, properties.Size());

      Assert.IsTrue(properties.Contains("experimental"));
      ref var prop = ref properties.At("experimental");
      Assert.AreEqual(CBType.Bool, prop.type);
      Assert.AreEqual(true, (bool)prop.@bool);

      // FIXME: exposed and required variables require the block to be composed (e.g. executed in a chain)
      var exposed = block.ExposedVariables();

      var required = block.RequiredVariables();
    }
  }
}
