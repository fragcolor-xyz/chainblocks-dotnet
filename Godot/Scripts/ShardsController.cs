/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;

using Fragcolor.Shards;

public sealed class ShardsController : Godot.Node
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

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (!_initialized)
        {
            Env = new ScriptingEnv();
            Mesh = Native.Core.CreateMesh();
            _initialized = true;
        }
        else
        {
            GetParent()?.RemoveChild(this);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Native.Core.Tick(Mesh);
    }
}
