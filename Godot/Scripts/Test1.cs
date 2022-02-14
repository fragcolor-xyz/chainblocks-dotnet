/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using Fragcolor.Chainblocks;

using Godot;

public class Test1 : Spatial
{
    private ExternalVariable _position;

    private Variable _chain;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _chain = new Variable();
        ChainblocksController.Env.Eval(@"(Chain ""test"" :Looped (Msg ""XXX"") .position (Log) (Pause 1.0))", _chain.Ptr);

        var position = new Vector3(3, 4, 5);
        _position = new ExternalVariable(_chain.Value.chain, "position", CBType.Float3);
        _position.Value.float3 = position.ToFloat3();

        Native.Core.Schedule(ChainblocksController.Node, _chain.Value.chain);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        var position = Translation;
        _position.Value.float3 = position.ToFloat3();
    }

    public override void _Notification(int what)
    {
        switch (what)
        {
            case NotificationExitTree:
                Native.Core.Unschedule(ChainblocksController.Node, _chain.Value.chain);
                _position.Dispose();
                _chain.Dispose();
                break;
        }
    }
}
