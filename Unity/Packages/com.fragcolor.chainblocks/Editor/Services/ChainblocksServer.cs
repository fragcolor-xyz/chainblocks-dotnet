/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using System;
using Fragcolor.Chainblocks.Collections;
using UnityEngine;

namespace Fragcolor.Chainblocks.UnityEditor.Services
{
  internal sealed class ChainblocksServer : IDisposable
  {
    private readonly Variable _chain;
    private readonly ExternalVariable? _list;

    private readonly bool _evalSuccess;

    internal static int Port { get; set; } = 7071;

    internal ChainblocksServer()
    {
      ChainblocksEditorController.EnsureInitialize();

      _chain = new Variable(false);
      var textAsset = ChainblocksEditorUtility.LoadEditorAssetAtPath<TextAsset>("server.edn");
      if (_evalSuccess = ChainblocksEditorController.Env.Eval(textAsset.text, _chain.Ptr))
      {
        _list = new ExternalVariable(_chain.Value.chain, "result", CBType.Seq);
        ChainblocksEditorController.Node.Schedule(_chain.Value.chain);
        ChainblocksEditorController.Start();
        ChainblocksEditorController.Update += OnUpdate;
      }
    }

    ~ChainblocksServer()
    {
      Dispose(false);
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
      if (disposing && _evalSuccess)
      {
        ChainblocksEditorController.Update -= OnUpdate;
        Native.Core.Unschedule(ChainblocksEditorController.Node, _chain.Value.chain);
      }

      _list?.Dispose();
      _chain.Dispose();
    }

    private void OnUpdate()
    {
      // consumes the list
      while (_list!.Value.seq.Count > 0)
      {
        var popped = _list.Value.seq.Pop();
        // log it for now
        Debug.LogFormat("Item: {0}", popped.GetString());
      }
    }
  }
}
