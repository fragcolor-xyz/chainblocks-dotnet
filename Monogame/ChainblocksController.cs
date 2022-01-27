/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;

using Fragcolor.Chainblocks;

using Microsoft.Xna.Framework;

namespace MyGame
{
    internal sealed class ChainblocksController : GameComponent
    {
        private static bool _initialized;

        private static LispEnv _env;
        private static Node _node;

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

        public static Node Node
        {
            get
            {
                if (!_initialized)
                    throw new InvalidOperationException();
                return _node;
            }
            private set { _node = value; }
        }

        public ChainblocksController(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            if (_initialized) return;

            Env = new LispEnv();
            Node = Native.Core.CreateNode();
            _initialized = true;
        }

        public override void Update(GameTime _)
        {
            Native.Core.Tick(Node);
        }
    }
}
