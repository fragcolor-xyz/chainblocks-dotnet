using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBlock
  {
    //! Native struct, don't edit
    internal IntPtr _inlineBlockId;
    internal IntPtr _owned;
    internal IntPtr _name;
    internal IntPtr _hash;
    internal IntPtr _help;
    internal IntPtr _inputHelp;
    internal IntPtr _outputHelp;
    internal IntPtr _properties;
    internal IntPtr _setup;
    internal IntPtr _destroy;
    internal IntPtr _inputTypes;
    internal IntPtr _outputTypes;
    internal IntPtr _exposedVariables;
    internal IntPtr _requiredVariables;
    internal IntPtr _compose;
    internal IntPtr _composed;
    internal IntPtr _parameters;
    internal IntPtr _setParam;
    internal IntPtr _getParam;
    internal IntPtr _warmup;
    internal IntPtr _activate;
    internal IntPtr _cleanup;
    internal IntPtr _nextFrame;
    internal IntPtr _mutate;
    internal IntPtr _crossover;
    internal IntPtr _getState;
    internal IntPtr _setState;
    internal IntPtr _resetState;
  }
}
