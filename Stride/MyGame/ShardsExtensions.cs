/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System.Runtime.CompilerServices;

using Fragcolor.Shards;

using Stride.Core.Mathematics;

namespace MyGame
{
    using SHInt2 = Fragcolor.Shards.Int2;
    using SHInt3 = Fragcolor.Shards.Int3;
    using SHInt4 = Fragcolor.Shards.Int4;
    using StrideInt2 = Stride.Core.Mathematics.Int2;
    using StrideInt3 = Stride.Core.Mathematics.Int3;
    using StrideInt4 = Stride.Core.Mathematics.Int4;

    internal static class ShardsExtensions
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

        public static SHInt2 ToInt2(this ref StrideInt2 vector)
        {
            return new SHInt2 { x = vector.X, y = vector.Y };
        }

        public static ref SHInt3 ToInt3(this ref StrideInt3 vector)
        {
            return ref Unsafe.As<StrideInt3, SHInt3>(ref vector);
        }

        public static ref SHInt4 ToInt4(this ref StrideInt4 vector)
        {
            return ref Unsafe.As<StrideInt4, SHInt4>(ref vector);
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

        public static StrideInt2 ToStrideInt2(this ref SHInt2 @int)
        {
            return new StrideInt2((int)@int.x, (int)@int.y);
        }

        public static ref StrideInt3 ToStrideInt3(this ref SHInt3 @int)
        {
            return ref Unsafe.As<SHInt3, StrideInt3>(ref @int);
        }

        public static ref StrideInt4 ToStrideInt4(this ref SHInt4 @int)
        {
            return ref Unsafe.As<SHInt4, StrideInt4>(ref @int);
        }
    }
}
