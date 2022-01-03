namespace Chainblocks
{
  public enum CBType : byte
  {
    None,
    Any,
    Enum,
    Bool,
    Int,    // A 64bits int
    Int2,   // A vector of 2 64bits ints
    Int3,   // A vector of 3 32bits ints
    Int4,   // A vector of 4 32bits ints
    Int8,   // A vector of 8 16bits ints
    Int16,  // A vector of 16 8bits ints
    Float,  // A 64bits float
    Float2, // A vector of 2 64bits floats
    Float3, // A vector of 3 32bits floats
    Float4, // A vector of 4 32bits floats
    Color,  // A vector of 4 uint8
    Block,  // a block, useful for future introspection blocks!

    EndOfBlittableTypes = 50, // anything below this is not blittable (ish)

    Bytes, // pointer + size
    String,
    Path,       // An OS filesystem path
    ContextVar, // A string label to find from CBContext variables
    Image,
    Seq,
    Table,
    Chain,
    Object,
    Array, // Notice: of just bilttable types/WIP!
    Set,
    Audio
  }
}