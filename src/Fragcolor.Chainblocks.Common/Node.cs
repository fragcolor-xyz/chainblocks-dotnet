/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Fragcolor.Chainblocks
{
  /// <summary>
  /// Wraps a <see cref="CBNodeRef"/> as a managed object.
  /// </summary>
  /// <remarks>
  /// Once this instance is not used anymore, <see cref="Dispose()"/> must be called to clean up the unmanaged resource.
  /// </remarks>
  public sealed class Node : IDisposable
  {
    private CBNodeRef _nodeRef;
    private int _disposeState;

    public Node()
    {
      _nodeRef = Native.Core.CreateNode();
    }

    ~Node()
    {
      Dispose(false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator CBNodeRef(Node node)
    {
      return node._nodeRef;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Schedule(CBChainRef chainRef)
    {
      Native.Core.Schedule(_nodeRef, chainRef);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Tick()
    {
      Native.Core.Tick(_nodeRef);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Unschedule(CBChainRef chainRef)
    {
      Native.Core.Unschedule(_nodeRef, chainRef);
    }

    private void Dispose(bool _)
    {
      if (Interlocked.CompareExchange(ref _disposeState, 1, 0) != 0) return;

      Native.Core.DestroyNode(_nodeRef);
      _nodeRef = default;
    }
  }
}
