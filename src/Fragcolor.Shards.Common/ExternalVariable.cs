/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Fragcolor.Shards
{
  public sealed class ExternalVariable : IDisposable
  {
    private SHWireRef _wire;
    private readonly string _name;
    private IntPtr _var;

    private int _disposeState;

    public ref SHVar Value
    {
      get
      {
        unsafe
        {
          return ref Unsafe.AsRef<SHVar>(_var.ToPointer());
        }
      }
    }

    public ExternalVariable(SHWireRef wire, string name, SHType type = SHType.None)
    {
      _wire = wire;
      _name = name;
      _var = Native.Core.AllocExternalVariable(wire, _name);
      Value.type = type;
      Value.flags = CBVarFlags.External;
    }

    ~ExternalVariable()
    {
      Dispose(false);
    }

    public Variable Clone()
    {
      var variable = new Variable();
      Native.Core.CloneVar(ref variable.Value, ref Value);
      return variable;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
      if (Interlocked.CompareExchange(ref _disposeState, 1, 0) != 0) return;

      // Finalization order cannot be guaranteed
      if (disposing)
      {
        Native.Core.FreeExternalVariable(_wire, _name);
      }

      _wire._ref = IntPtr.Zero;
      _var = IntPtr.Zero;
    }
  }
}
