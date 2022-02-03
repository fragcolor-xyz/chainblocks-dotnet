/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;

using Fragcolor.Chainblocks;

using Stride.Engine;

namespace MyGame
{
    public class ChainblocksController : SyncScript
    {
        private static bool _initialized;

        private static LispEnv _env;
        private static CBNodeRef _node;

        public static LispEnv Env
        {
            get
            {
                if (!_initialized)
                    throw new InvalidOperationException();
                return _env;
            }
            private set { _env = value; }
        }

        public static CBNodeRef Node
        {
            get
            {
                if (!_initialized)
                    throw new InvalidOperationException();
                return _node;
            }
            private set { _node = value; }
        }

        public override void Start()
        {
            if (!_initialized)
            {
                Env = new LispEnv();
                Node = Native.Core.CreateNode();
                _initialized = true;
            }
            else
            {
                Script.Remove(this);
            }
        }

        public override void Update()
        {
            Native.Core.Tick(Node);
        }
    }
}
