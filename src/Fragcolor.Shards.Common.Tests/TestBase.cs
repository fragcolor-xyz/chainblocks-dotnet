/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using NUnit.Framework;

namespace Fragcolor.Shards.Tests
{
  /// <summary>
  /// Base class for shards tests.
  /// </summary>
  internal abstract class TestBase
  {
#pragma warning disable CS8618
    protected Variable _wire;
    private ScriptingEnv _env;
    private Mesh _mesh;
#pragma warning restore CS8618
    private bool _isScheduled;

    /// <summary>
    /// A reference to the wire.
    /// </summary>
    /// <remarks>
    /// Derived test classes are responsible for creating and disposing of the wire.
    /// </remarks>
    protected ref readonly SHWireRef Wire => ref _wire.Value.wire;

    /// <summary>
    /// The scripting environment used by the test.
    /// </summary>
    protected ref readonly ScriptingEnv Env => ref _env;

    /// <summary>
    /// Initializes the scripting environment and creates a root mesh.
    /// </summary>
    /// <remarks>
    /// The same env and mesh will be used by all the tests in a given test class.
    /// </remarks>
    [OneTimeSetUp]
    protected void BaseOneTimeSetup()
    {
      _env = new ScriptingEnv();
      _mesh = new Mesh();
    }

    /// <summary>
    /// Cleans up the environment and the root mesh.
    /// </summary>
    [OneTimeTearDown]
    protected void BaseOneTimeTearDown()
    {
      _mesh.Dispose();
      Env.Dispose();
    }

    /// <summary>
    /// Logs the test name when it is starting.
    /// </summary>
    [SetUp]
    protected void BaseSetup()
    {
      var name = TestContext.CurrentContext.Test.Name;
      Native.Core.Log($"Starting {name}...");
    }

    /// <summary>
    /// Logs the test name when it is completed.
    /// </summary>
    [TearDown]
    protected void BaseTearDown()
    {
      var name = TestContext.CurrentContext.Test.Name;
      Native.Core.Log($"{name} done.");
    }

    /// <summary>
    /// Schedules <see cref="Wire"/> on the root mesh.
    /// </summary>
    protected void ScheduleWire()
    {
      if (_isScheduled) return;
      _mesh.Schedule(Wire);
      _isScheduled = true;
    }

    /// <summary>
    /// Ticks the root mesh once.
    /// </summary>
    protected void Tick()
    {
      Native.Core.Tick(_mesh);
    }

    /// <summary>
    /// Unschedules <see cref="Wire"/> from the root mesh.
    /// </summary>
    protected void UnscheduleWire()
    {
      if (!_isScheduled) return;
      _mesh.Unschedule(Wire);
      _isScheduled = false;
    }
  }
}
