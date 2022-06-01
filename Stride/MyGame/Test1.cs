/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Threading.Tasks;

using Fragcolor.Shards;

using Stride.Core.Mathematics;
using Stride.Engine;

namespace MyGame
{
    public sealed class Test1 : AsyncScript
    {
        private ExternalVariable _position;

        private Variable _wire;

        public override void Cancel()
        {
            Log.Debug("Cancel");
            Native.Core.Unschedule(ShardsController.Mesh, _wire.Value.wire);
            base.Cancel(); // note: dispose the collected objects in reverse order
        }

        public override async Task Execute()
        {
            Log.Debug("Start");

            _wire = new Variable();
            ShardsController.Env.Eval("(Wire \"test\" :Looped (Msg \"XXX\") .position (Log) (Pause 1.0))", _wire.Ptr);

            var position = new Vector3(3, 4, 5);
            _position = new ExternalVariable(_wire.Value.wire, "position", SHType.Float3);
            _position.Value.float3 = position.ToFloat3();

            Collector.Add(_wire);
            Collector.Add(_position);
            Native.Core.Schedule(ShardsController.Mesh, _wire.Value.wire);

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
