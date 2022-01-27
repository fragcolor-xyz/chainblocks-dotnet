/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;

using Fragcolor.Chainblocks;

public sealed class ChainblocksController : Godot.Node
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

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (!_initialized)
        {
            Env = new LispEnv();
            Node = Native.Core.CreateNode();
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
        Native.Core.Tick(Node);
    }
}
