/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.CompilerServices;

using Fragcolor.Shards;

using Godot;

internal static class ShardsExtensions
{
    public static Float2 ToFloat2(this ref Vector2 vector)
    {
        return new Float2 { x = vector.x, y = vector.y };
    }

    public static ref Float3 ToFloat3(this ref Vector3 vector)
    {
        return ref Unsafe.As<Vector3, Float3>(ref vector);
    }

    public static Vector2 ToVector2(this ref Float2 @float)
    {
        return new Vector2((float)@float.x, (float)@float.y);
    }

    public static ref Vector3 ToVector3(this ref Float3 @float)
    {
        return ref Unsafe.As<Float3, Vector3>(ref @float);
    }
}
