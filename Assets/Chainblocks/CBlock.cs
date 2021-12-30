using System;
using System.Runtime.InteropServices;

namespace Chainblocks
{
  [StructLayout(LayoutKind.Sequential)]
  public struct CBlock
  {
    //! Native struct, don't edit
    IntPtr _inlineBlockId;
    IntPtr _owned;
    IntPtr _name;
    IntPtr _hash;
    IntPtr _help;
    IntPtr _inputHelp;
    IntPtr _outputHelp;
    IntPtr _properties;
    IntPtr _setup;
    IntPtr _destroy;
    IntPtr _inputTypes;
    IntPtr _outputTypes;
    IntPtr _exposedVariables;
    IntPtr _requiredVariables;
    IntPtr _compose;
    IntPtr _composed;
    IntPtr _parameters;
    IntPtr _setParam;
    IntPtr _getParam;
    IntPtr _warmup;
    IntPtr _activate;
    IntPtr _cleanup;
    IntPtr _nextFrame;
    IntPtr _mutate;
    IntPtr _crossover;
    IntPtr _getState;
    IntPtr _setState;
    IntPtr _resetState;
  }
}
