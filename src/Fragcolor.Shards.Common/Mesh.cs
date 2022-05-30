/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Fragcolor.Shards
{
  /// <summary>
  /// Wraps a <see cref="SHMeshRef"/> as a managed object.
  /// </summary>
  /// <remarks>
  /// Once this instance is not used anymore, <see cref="Dispose()"/> must be called to clean up the unmanaged resource.
  /// </remarks>
  public sealed class Mesh : IDisposable
  {
    private SHMeshRef _meshRef;
    private int _disposeState;

    public Mesh()
    {
      _meshRef = Native.Core.CreateMesh();
    }

    ~Mesh()
    {
      Dispose(false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator SHMeshRef(Mesh mesh)
    {
      return mesh._meshRef;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Schedule(SHWireRef wireRef)
    {
      Native.Core.Schedule(_meshRef, wireRef);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Tick()
    {
      Native.Core.Tick(_meshRef);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Unschedule(SHWireRef wireRef)
    {
      Native.Core.Unschedule(_meshRef, wireRef);
    }

    private void Dispose(bool _)
    {
      if (Interlocked.CompareExchange(ref _disposeState, 1, 0) != 0) return;

      Native.Core.DestroyMesh(_meshRef);
      _meshRef = default;
    }
  }
}
