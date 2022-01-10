/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.CompilerServices;

using Fragcolor.Chainblocks;

using Microsoft.Xna.Framework;

namespace MyGame
{
    internal static class ChainblocksExtensions
    {
        public static Float2 ToFloat2(this ref Vector2 vector)
        {
            return new Float2 { x = vector.X, y = vector.Y };
        }

        public static ref Float3 ToFloat3(this ref Vector3 vector)
        {
            return ref Unsafe.As<Vector3, Float3>(ref vector);
        }

        public static ref Float4 ToFloat4(this ref Vector4 vector)
        {
            return ref Unsafe.As<Vector4, Float4>(ref vector);
        }

        public static Vector2 ToVector2(this ref Float2 @float)
        {
            return new Vector2((float)@float.x, (float)@float.y);
        }

        public static ref Vector3 ToVector3(this ref Float3 @float)
        {
            return ref Unsafe.As<Float3, Vector3>(ref @float);
        }

        public static ref Vector4 ToVector4(this ref Float4 @float)
        {
            return ref Unsafe.As<Float4, Vector4>(ref @float);
        }
    }
}
