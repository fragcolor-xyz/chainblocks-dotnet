/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using NUnit.Framework;

namespace Fragcolor.Chainblocks.Tests
{
  internal abstract class TestBase
  {
#pragma warning disable CS8618
    protected Variable _chain;
    private LispEnv _env;
    private CBNodeRef _node;
#pragma warning restore CS8618
    private bool _isScheduled;

    protected ref readonly CBChainRef Chain => ref _chain.Value.chain;

    protected ref readonly LispEnv Env => ref _env;

    protected ref readonly CBNodeRef Node => ref _node;

    [OneTimeSetUp]
    protected void BaseOneTimeSetup()
    {
      _env = new LispEnv();
      _node = Native.Core.CreateNode();
    }

    [OneTimeTearDown]
    protected void BaseOneTimeTearDown()
    {
      Env.Dispose();
    }

    [SetUp]
    protected void BaseSetup()
    {
      var name = TestContext.CurrentContext.Test.Name;
      Native.Core.Log($"Starting {name}...");
    }

    [TearDown]
    protected void BaseTearDown()
    {
      var name = TestContext.CurrentContext.Test.Name;
      Native.Core.Log($"{name} done..");
    }

    protected void ScheduleChain()
    {
      if (_isScheduled) return;
      Native.Core.Schedule(Node, Chain);
      _isScheduled = true;
    }

    protected void Tick()
    {
      Native.Core.Tick(Node);
    }

    protected void UnscheduleChain()
    {
      if (!_isScheduled) return;
      Native.Core.Unschedule(Node, Chain);
      _isScheduled = false;
    }
  }
}
