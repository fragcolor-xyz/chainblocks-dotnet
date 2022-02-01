/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using NUnit.Framework;

namespace Fragcolor.Chainblocks.Tests
{
    public abstract class TestBase
    {
        public LispEnv Env { get; private set; }

        public Node Node { get; private set; }

        [OneTimeSetUp]
        protected void BaseSetup()
        {
            Env = new LispEnv();
            Node = Native.Core.CreateNode();
        }

        [OneTimeTearDown]
        protected void BaseTearDown()
        {
            Env.Dispose();
        }

        protected void Tick()
        {
            Native.Core.Tick(Node);
        }
    }
}
