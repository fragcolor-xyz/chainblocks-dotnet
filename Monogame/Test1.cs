/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using Fragcolor.Chainblocks;

using Microsoft.Xna.Framework;

namespace MyGame
{
    internal sealed class Test1
    {
        private ExternalVariable _position;
        private Variable _chain;

        public void Initialize()
        {
            _chain = new Variable();
            ChainblocksController.Env.Eval(@"(Chain ""test"" :Looped (Msg ""XXX"") .position (Log) (Pause 1.0))", _chain.Ptr);

            var position = new Vector3(3, 4, 5);
            _position = new ExternalVariable(_chain.Value.chain, "position");
            _position.Value.float3 = position.ToFloat3();
            _position.Value.type = CBType.Float3;
            _position.Value.flags = (1 << 2);

            Native.Core.Schedule(ChainblocksController.Node, _chain.Value.chain);
        }

        public void Update(GameTime _)
        {
            var position = new Vector3(0, 10, 0);
            _position.Value.float3 = position.ToFloat3();
        }
    }
}
