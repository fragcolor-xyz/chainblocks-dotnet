/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

namespace Fragcolor.Shards
{
  /// <summary>
  /// Enumeration of supported types for <see cref="SHVar"/>.
  /// </summary>
  public enum SHType : byte
  {
    None,
    Any,
    Enum,
    Bool,
    /// <summary>
    /// A 64bits int
    /// </summary>
    Int,
    /// <summary>
    /// A vector of 2 64bits ints
    /// </summary>
    Int2,
    /// <summary>
    /// A vector of 3 32bits ints
    /// </summary>
    Int3,
    /// <summary>
    /// A vector of 4 32bits ints
    /// </summary>
    Int4,
    /// <summary>
    /// A vector of 8 16bits ints
    /// </summary>
    Int8,
    /// <summary>
    /// A vector of 16 8bits ints
    /// </summary>
    Int16,
    /// <summary>
    /// A 64bits float
    /// </summary>
    Float,
    /// <summary>
    /// A vector of 2 64bits floats
    /// </summary>
    Float2,
    /// <summary>
    /// A vector of 3 32bits floats
    /// </summary>
    Float3,
    /// <summary>
    /// A vector of 4 32bits floats
    /// </summary>
    Float4,
    /// <summary>
    /// A vector of 4 uint8
    /// </summary>
    Color,
    /// <summary>
    /// a shard, useful for future introspection shards!
    /// </summary>
    Shard,

    /// <summary>
    /// anything below this is not blittable (ish)
    /// </summary>
    EndOfBlittableTypes = 50,

    /// <summary>
    /// pointer + size
    /// </summary>
    Bytes,
    String,
    /// <summary>
    /// An OS filesystem path
    /// </summary>
    Path,
    /// <summary>
    /// A string label to find from CBContext variables
    /// </summary>
    ContextVar,
    Image,
    Seq,
    Table,
    Wire,
    Object,
    /// <summary>
    /// Notice: of just blittable types/WIP!
    /// </summary>
    Array,
    Set,
    Audio
  }
}
