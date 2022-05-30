using System.Runtime.CompilerServices;

using Fragcolor.Shards;

using UnityEngine;

namespace Fragcolor.Shards
{
    public static class ShardsExtensions
    {
        public static Float2 ToFloat2(this ref Vector2 vector)
        {
            return new Float2 { x = vector.x, y = vector.y };
        }

        public static ref Float3 ToFloat3(this ref Vector3 vector)
        {
            return ref Unsafe.As<Vector3, Float3>(ref vector);
        }

        public static ref Float4 ToFloat4(this ref Vector4 vector)
        {
            return ref Unsafe.As<Vector4, Float4>(ref vector);
        }
        public static Int2 ToInt2(this ref Vector2Int vector)
        {
            return new Int2 { x = vector.x, y = vector.y };
        }

        public static ref Int3 ToInt3(this ref Vector3Int vector)
        {
            return ref Unsafe.As<Vector3Int, Int3>(ref vector);
        }

        // public static ref Int4 ToInt4(this ref Vector4Int vector)
        // {
        //     return ref Unsafe.As<Vector4Int, Int4>(ref vector);
        // }

        public static Vector2 ToVector2(this ref Float2 @float)
        {
            return new Vector2((float)@float.x, (float)@float.y);
        }

        public static Vector2Int ToVector2Int(this ref Int2 @int)
        {
            return new Vector2Int((int)@int.x, (int)@int.y);
        }

        public static ref Vector3 ToVector3(this ref Float3 @float)
        {
            return ref Unsafe.As<Float3, Vector3>(ref @float);
        }

        public static ref Vector3Int ToVector3Int(this ref Int3 @int)
        {
            return ref Unsafe.As<Int3, Vector3Int>(ref @int);
        }

        public static ref Vector4 ToVector4(this ref Float4 @float)
        {
            return ref Unsafe.As<Float4, Vector4>(ref @float);
        }

        // public static ref Vector4Int ToVector4Int(this ref Int4 @int)
        // {
        //     return ref Unsafe.As<Int4, Vector4Int>(ref @int);
        // }
    }
}
