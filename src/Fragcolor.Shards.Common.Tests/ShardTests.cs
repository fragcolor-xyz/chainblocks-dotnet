/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using Fragcolor.Shards.Collections;

using NUnit.Framework;

namespace Fragcolor.Shards.Tests
{
  /// <summary>
  /// Tests for shards and wire introspection.
  /// </summary>
  [TestFixture]
  internal sealed class ShardTests : TestBase
  {
    /// <summary>
    /// Cleans up the wire.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
      UnscheduleWire();
      _wire?.Dispose();
    }

    /// <summary>
    /// Tests wire introspection.
    /// </summary>
    [Test]
    public void TestInstrospection()
    {
      var name = TestContext.CurrentContext.Test.Name;
      _wire = new Variable();
      var ok = Env.Eval(@$"(Wire ""{name}"" :Looped ""Hello"" > .output (Log))", _wire.Ptr);
      Assert.IsTrue(ok);
      Assert.IsTrue(Wire.IsValid());

      using var var = new ExternalVariable(Wire, "output", SHType.String);

      var info = Native.Core.GetWireInfo(Wire);
      Assert.AreEqual(name, info.Name());
      Assert.IsTrue(info.IsLooped());
      Assert.IsFalse(info.IsRunning());
      Assert.IsFalse(info.IsUnsafe());

      var shards = info.Shards();
      Assert.AreEqual(3, shards.Count);

      {
        ref var shard = ref shards[0];
        Assert.AreEqual("Const", shard.Name());
        Assert.AreNotEqual(0, shard.Hash());
        var properties = shard.Properties();
        Assert.IsFalse(properties.IsValid());
        var inputs = shard.InputTypes();
        Assert.AreEqual(1, inputs.Count);
        Assert.AreEqual(SHType.None, inputs[0].BasicType());
        var outputs = shard.OutputTypes();
        Assert.AreEqual(1, outputs.Count);
        Assert.AreEqual(SHType.Any, outputs[0].BasicType());
        var parameters = shard.Parameters();
        Assert.AreEqual(1, parameters.Count);
        {
          var param = parameters[0];
          Assert.AreEqual("Value", param.Name());
          var paramTypes = param.Types();
          Assert.AreEqual(1, paramTypes.Count);
          Assert.AreEqual(SHType.Any, paramTypes[0].BasicType());
        }
      }

      {
        ref var shard = ref shards[1];
        Assert.AreEqual("Update", shard.Name());
        Assert.AreNotEqual(0, shard.Hash());
        var inputs = shard.InputTypes();
        Assert.AreEqual(1, inputs.Count);
        Assert.AreEqual(SHType.Any, inputs[0].BasicType());
        var outputs = shard.OutputTypes();
        Assert.AreEqual(1, outputs.Count);
        Assert.AreEqual(SHType.Any, outputs[0].BasicType());
        var parameters = shard.Parameters();
        Assert.AreEqual(3, parameters.Count);
        {
          var param = parameters.At(0);
          Assert.AreEqual("Name", param.Name());
          var paramTypes = param.Types();
          Assert.AreEqual(2, paramTypes.Count);
          Assert.AreEqual(SHType.String, paramTypes[0].BasicType());
          Assert.AreEqual(SHType.ContextVar, paramTypes[1].BasicType());
        }
        {
          var param = parameters.At(1);
          Assert.AreEqual("Key", param.Name());
          var paramTypes = param.Types();
          Assert.AreEqual(3, paramTypes.Count);
          Assert.AreEqual(SHType.String, paramTypes[0].BasicType());
          Assert.AreEqual(SHType.ContextVar, paramTypes[1].BasicType());
          Assert.AreEqual(SHType.None, paramTypes.At(2).BasicType());
        }
        {
          var param = parameters.At(2);
          Assert.AreEqual("Global", param.Name());
          var paramTypes = param.Types();
          Assert.AreEqual(1, paramTypes.Count);
          Assert.AreEqual(SHType.Bool, paramTypes[0].BasicType());
        }
      }

      {
        ref var shard = ref shards.At(2);
        Assert.AreEqual("Log", shard.Name());
        Assert.AreNotEqual(0, shard.Hash());
        var inputs = shard.InputTypes();
        Assert.AreEqual(1, inputs.Count);
        Assert.AreEqual(SHType.Any, inputs[0].BasicType());
        var outputs = shard.OutputTypes();
        Assert.AreEqual(1, outputs.Count);
        Assert.AreEqual(SHType.Any, outputs[0].BasicType());
        var parameters = shard.Parameters();
        Assert.AreEqual(1, parameters.Count);
        {
          var param = parameters[0];
          Assert.AreEqual("Prefix", param.Name());
          var paramTypes = param.Types();
          Assert.AreEqual(1, paramTypes.Count);
          Assert.AreEqual(SHType.String, paramTypes[0].BasicType());
        }
      }

      ScheduleWire();
      Assert.IsFalse(info.IsRunning());
      info = Native.Core.GetWireInfo(Wire);
      Assert.IsTrue(info.IsRunning());

      Tick();

      UnscheduleWire();
      Assert.IsTrue(info.IsRunning());
      info = Native.Core.GetWireInfo(Wire);
      Assert.IsFalse(info.IsRunning());
    }

    /// <summary>
    /// Tests a shard marked as experimental.
    /// </summary>
    [Test]
    public void TestExperimentalShard()
    {
      var shardPtr = Native.Core.CreateShard("Gizmo.CubeView");
      Assert.IsTrue(shardPtr.IsValid());
      ref var shard = ref shardPtr.AsRef();
      Assert.AreEqual("Gizmo.CubeView", shard.Name());
      Assert.AreNotEqual(0, shard.Hash());
      Assert.AreNotEqual(0, shard.Help()._crc);
      Assert.AreNotEqual(0, shard.InputHelp()._crc);
      Assert.AreNotEqual(0, shard.OutputHelp()._crc);
#if DEBUG
      Assert.IsFalse(string.IsNullOrEmpty((string?)shard.Help()));
      Assert.IsFalse(string.IsNullOrEmpty((string?)shard.InputHelp()));
      Assert.IsFalse(string.IsNullOrEmpty((string?)shard.OutputHelp()));
#else
      Assert.IsTrue(string.IsNullOrEmpty((string?)shard.Help()));
      Assert.IsTrue(string.IsNullOrEmpty((string?)shard.InputHelp()));
      Assert.IsTrue(string.IsNullOrEmpty((string?)shard.OutputHelp()));
#endif

      var properties = shard.Properties();
      Assert.IsTrue(properties.IsValid());
      Assert.AreEqual(1, properties.Size());

      Assert.IsTrue(properties.Contains("experimental"));
      ref var prop = ref properties.At("experimental");
      Assert.AreEqual(SHType.Bool, prop.type);
      Assert.AreEqual(true, (bool)prop.@bool);

      // FIXME: exposed and required variables require the shard to be composed (e.g. executed in a wire)
      var exposed = shard.ExposedVariables();

      var required = shard.RequiredVariables();
    }
  }
}
