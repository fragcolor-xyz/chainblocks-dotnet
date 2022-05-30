/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using NUnit.Framework;

namespace Fragcolor.Chainblocks.Claymore.Tests
{
  internal abstract class TestBase
  {
#pragma warning disable CS8618
    private LispEnv _env;
#pragma warning restore CS8618

    protected ref readonly LispEnv Env => ref _env;

    [OneTimeSetUp]
    protected void BaseOneTimeSetup()
    {
      _env = new LispEnv();
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
      Native.Core.Log($"{name} done.");
    }
  }
}
