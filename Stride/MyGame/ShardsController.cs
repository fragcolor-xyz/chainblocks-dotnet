/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;

using Fragcolor.Shards;

using Stride.Engine;

namespace MyGame
{
    public class ShardsController : SyncScript
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

        public override void Start()
        {
            if (!_initialized)
            {
                Env = new ScriptingEnv();
                Mesh = Native.Core.CreateMesh();
                _initialized = true;
            }
            else
            {
                Script.Remove(this);
            }
        }

        public override void Update()
        {
            Native.Core.Tick(Mesh);
        }
    }
}
