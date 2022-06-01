/* SPDX-License-Identifier: BSD-3-Clause */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using System;
using System.Runtime.InteropServices;

namespace Fragcolor.Shards
{
  /// <summary>
  /// Core struct.
  /// </summary>
  /// <remarks>
  /// See <see cref="SHCoreExtensions"/> for available methods on this struct.
  /// </remarks>
  [StructLayout(LayoutKind.Sequential)]
  public struct SHCore
  {
    //! Native struct, don't edit
    internal IntPtr _alloc;
    internal IntPtr _free;
    internal IntPtr _tableNew;
    internal IntPtr _setNew;
    internal IntPtr _composeShards;
    internal IntPtr _runShards;
    internal IntPtr _runShards2;
    internal IntPtr _runShardsHashed;
    internal IntPtr _runShardsHashed2;
    internal IntPtr _log;
    internal IntPtr _logLevel;
    internal IntPtr _createShard;
    internal IntPtr _validateSetParam;
    internal IntPtr _createWire;
    internal IntPtr _setWireName;
    internal IntPtr _setWireLooped;
    internal IntPtr _setWireUnsafe;
    internal IntPtr _addShard;
    internal IntPtr _removeShard;
    internal IntPtr _destroyWire;
    internal IntPtr _stopWire;
    internal IntPtr _composeWire;
    internal IntPtr _runWire;
    internal IntPtr _getWireInfo;
    internal IntPtr _getGlobalWire;
    internal IntPtr _setGlobalWire;
    internal IntPtr _unsetGlobalWire;
    internal IntPtr _createMesh;
    internal IntPtr _destroyMesh;
    internal IntPtr _schedule;
    internal IntPtr _unschedule;
    internal IntPtr _tick;
    internal IntPtr _sleep;
    internal IntPtr _getRootPath;
    internal IntPtr _setRootPath;
    internal IntPtr _asyncActivate;
    internal IntPtr _getShards;
    internal IntPtr _registerShard;
    internal IntPtr _registerObjectType;
    internal IntPtr _registerEnumType;
    internal IntPtr _registerRunLoopCallback;
    internal IntPtr _unregisterRunLoopCallback;
    internal IntPtr _registerExitCallback;
    internal IntPtr _unregisterExitCallback;
    internal IntPtr _referenceVariable;
    internal IntPtr _referenceWireVariable;
    internal IntPtr _releaseVariable;
    internal IntPtr _setExternalVariable;
    internal IntPtr _removeExternalVariable;
    internal IntPtr _allocExternalVariable;
    internal IntPtr _freeExternalVariable;
    internal IntPtr _suspend;
    internal IntPtr _getState;
    internal IntPtr _abortWire;
    internal IntPtr _cloneVar;
    internal IntPtr _destroyVar;
    internal IntPtr _readCachedString;
    internal IntPtr _writeCachedString;
    internal IntPtr _isEqualVar;
    internal IntPtr _isEqualType;
    internal IntPtr _deriveTypeInfo;
    internal IntPtr _freeDerivedTypeInfo;
    internal IntPtr _seqFree;
    internal IntPtr _seqPush;
    internal IntPtr _seqInsert;
    internal IntPtr _seqPop;
    internal IntPtr _seqResize;
    internal IntPtr _seqFastDelete;
    internal IntPtr _seqSlowDelete;
    internal IntPtr _typesFree;
    internal IntPtr _typesPush;
    internal IntPtr _typesInsert;
    internal IntPtr _typesPop;
    internal IntPtr _typesResize;
    internal IntPtr _typesFastDelete;
    internal IntPtr _typesSlowDelete;
    internal IntPtr _paramsFree;
    internal IntPtr _paramsPush;
    internal IntPtr _paramsInsert;
    internal IntPtr _paramsPop;
    internal IntPtr _paramsResize;
    internal IntPtr _paramsFastDelete;
    internal IntPtr _paramsSlowDelete;
    internal IntPtr _shardsFree;
    internal IntPtr _shardsPush;
    internal IntPtr _shardsInsert;
    internal IntPtr _shardsPop;
    internal IntPtr _shardsResize;
    internal IntPtr _shardsFastDelete;
    internal IntPtr _shardsSlowDelete;
    internal IntPtr _expTypesFree;
    internal IntPtr _expTypesPush;
    internal IntPtr _expTypesInsert;
    internal IntPtr _expTypesPop;
    internal IntPtr _expTypesResize;
    internal IntPtr _expTypesFastDelete;
    internal IntPtr _expTypesSlowDelete;
    internal IntPtr _enumsFree;
    internal IntPtr _enumsPush;
    internal IntPtr _enumsInsert;
    internal IntPtr _enumsPop;
    internal IntPtr _enumsResize;
    internal IntPtr _enumsFastDelete;
    internal IntPtr _enumsSlowDelete;
    internal IntPtr _stringsFree;
    internal IntPtr _stringsPush;
    internal IntPtr _stringsInsert;
    internal IntPtr _stringsPop;
    internal IntPtr _stringsResize;
    internal IntPtr _stringsFastDelete;
    internal IntPtr _stringsSlowDelete;
  }
}
