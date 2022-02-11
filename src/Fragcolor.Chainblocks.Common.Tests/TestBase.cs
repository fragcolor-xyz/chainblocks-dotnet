/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using NUnit.Framework;

namespace Fragcolor.Chainblocks.Tests
{
  /// <summary>
  /// Base class for chainblocks tests.
  /// </summary>
  internal abstract class TestBase
  {
#pragma warning disable CS8618
    protected Variable _chain;
    private LispEnv _env;
    private CBNodeRef _node;
#pragma warning restore CS8618
    private bool _isScheduled;

    /// <summary>
    /// A reference to the chain.
    /// </summary>
    /// <remarks>
    /// Derived test classes are responsible for creating and disposing of the chain.
    /// </remarks>
    protected ref readonly CBChainRef Chain => ref _chain.Value.chain;

    /// <summary>
    /// The LISP environment used by the test.
    /// </summary>
    protected ref readonly LispEnv Env => ref _env;

    /// <summary>
    /// Initializes the LISP environment and creates a root node.
    /// </summary>
    /// <remarks>
    /// The same env and node will be used by all the tests in a given test class.
    /// </remarks>
    [OneTimeSetUp]
    protected void BaseOneTimeSetup()
    {
      _env = new LispEnv();
      _node = Native.Core.CreateNode();
    }

    /// <summary>
    /// Cleans up the environment and the root node.
    /// </summary>
    [OneTimeTearDown]
    protected void BaseOneTimeTearDown()
    {
      Native.Core.DestroyNode(_node);
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
    /// Schedules <see cref="Chain"/> on the root node.
    /// </summary>
    protected void ScheduleChain()
    {
      if (_isScheduled) return;
      Native.Core.Schedule(_node, Chain);
      _isScheduled = true;
    }

    /// <summary>
    /// Ticks the root node once.
    /// </summary>
    protected void Tick()
    {
      Native.Core.Tick(_node);
    }

    /// <summary>
    /// Unschedules <see cref="Chain"/> from the root node.
    /// </summary>
    protected void UnscheduleChain()
    {
      if (!_isScheduled) return;
      Native.Core.Unschedule(_node, Chain);
      _isScheduled = false;
    }
  }
}
