/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using Fragcolor.Shards;

using Godot;

public class Test1 : Spatial
{
    private ExternalVariable _position;

    private Variable _wire;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _wire = new Variable();
        ShardsController.Env.Eval(@"(Wire ""test"" :Looped (Msg ""XXX"") .position (Log) (Pause 1.0))", _wire.Ptr);

        var position = new Vector3(3, 4, 5);
        _position = new ExternalVariable(_wire.Value.wire, "position", SHType.Float3);
        _position.Value.float3 = position.ToFloat3();

        Native.Core.Schedule(ShardsController.Mesh, _wire.Value.wire);
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
                Native.Core.Unschedule(ShardsController.Mesh, _wire.Value.wire);
                _position.Dispose();
                _wire.Dispose();
                break;
        }
    }
}
