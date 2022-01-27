/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Threading.Tasks;

using Fragcolor.Chainblocks;

using Stride.Core.Mathematics;
using Stride.Engine;

namespace MyGame
{
    public sealed class Test1 : AsyncScript
    {
        private ExternalVariable _position;

        private Variable _chain;

        public override void Cancel()
        {
            Log.Debug("Cancel");
            Native.Core.Unschedule(ChainblocksController.Node, _chain.Value.chain);
            base.Cancel(); // note: dispose the collected objects in reverse order
        }

        public override async Task Execute()
        {
            Log.Debug("Start");

            _chain = new Variable();
            ChainblocksController.Env.Eval("(Chain \"test\" :Looped (Msg \"XXX\") .position (Log) (Pause 1.0))", _chain.Ptr);

            var position = new Vector3(3, 4, 5);
            _position = new ExternalVariable(_chain.Value.chain, "position");
            _position.Value.float3 = position.ToFloat3();
            _position.Value.type = CBType.Float3;
            _position.Value.flags = (1 << 2);

            Collector.Add(_chain);
            Collector.Add(_position);
            Native.Core.Schedule(ChainblocksController.Node, _chain.Value.chain);

            while (Game.IsRunning)
            {
                // let it tick first
                await Script.NextFrame();

                position = Entity.Transform.Position;
                _position.Value.float3 = position.ToFloat3();
            }
        }
    }
}
