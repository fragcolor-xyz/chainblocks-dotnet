/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using Fragcolor.Shards;

using Microsoft.Xna.Framework;

namespace MyGame
{
    internal sealed class Test1
    {
        private ExternalVariable _position;
        private Variable _wire;

        public void Initialize()
        {
            _wire = new Variable();
            ShardsController.Env.Eval(@"(Wire ""test"" :Looped (Msg ""XXX"") .position (Log) (Pause 1.0))", _wire.Ptr);

            var position = new Vector3(3, 4, 5);
            _position = new ExternalVariable(_wire.Value.wire, "position", SHType.Float3);
            _position.Value.float3 = position.ToFloat3();

            Native.Core.Schedule(ShardsController.Mesh, _wire.Value.wire);
        }

        public void Update(GameTime _)
        {
            var position = new Vector3(0, 10, 0);
            _position.Value.float3 = position.ToFloat3();
        }
    }
}
