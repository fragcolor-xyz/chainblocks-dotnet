/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;

using Fragcolor.Shards;

using Microsoft.Xna.Framework;

namespace MyGame
{
    internal sealed class ShardsController : GameComponent
    {
        private static bool _initialized;

        private static ScriptingEnv _env;
        private static SHMeshRef _mesh;

        public static ScriptingEnv Env
        {
            get
            {
                if (!_initialized)
                    throw new InvalidOperationException();
                return _env;
            }
            private set { _env = value; }
        }

        public static SHMeshRef Mesh
        {
            get
            {
                if (!_initialized)
                    throw new InvalidOperationException();
                return _mesh;
            }
            private set { _mesh = value; }
        }

        public ShardsController(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            if (_initialized) return;

            Env = new ScriptingEnv();
            Mesh = Native.Core.CreateMesh();
            _initialized = true;
        }

        public override void Update(GameTime _)
        {
            Native.Core.Tick(Mesh);
        }
    }
}
