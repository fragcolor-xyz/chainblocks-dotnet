/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

#nullable enable

using System;

using UnityEditor;

using UnityEngine;
using UnityEngine.Networking;

namespace Fragcolor.Chainblocks.UnityEditor.Services
{
  internal sealed class EditorWebRequest : IDisposable
  {
    private UnityWebRequest? _request;
    private Action<byte[]?>? _callback;

    private EditorWebRequest(UnityWebRequest request, Action<byte[]?> callback)
    {
      _request = request ?? throw new ArgumentNullException(nameof(request));
      _callback = callback ?? throw new ArgumentNullException(nameof(callback));

      EditorApplication.update += OnUpdate;
      _request.SendWebRequest();
    }

    void IDisposable.Dispose()
    {
      // note: dispose also aborts the underlying request
      _request?.Dispose();
      _request = null;
      _callback = null;
    }

    internal static EditorWebRequest GetBytes(string uri, Action<byte[]?> callback)
    {
      var request = UnityWebRequest.Get(uri);
      return new EditorWebRequest(request, callback);
    }

    internal void Abort()
    {
      ((IDisposable) this).Dispose();
    }

    private void OnUpdate()
    {
      if (_request == null)
      {
        // note: shoudln't happen but just in case
        EditorApplication.update -= OnUpdate;
        return;
      }

      if (!_request.isDone) return;

      if (_request.result != UnityWebRequest.Result.Success)
        Debug.LogError(_request.error);

      try
      {
        _callback?.Invoke(_request.downloadHandler.data);
      }
      finally
      {
        EditorApplication.update -= OnUpdate;

        // self abort to cleanup
        Abort();
      }
    }
  }
}
