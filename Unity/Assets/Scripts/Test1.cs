/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using Fragcolor.Shards;
using Fragcolor.Shards.UnityEngine;

using UnityEngine;

public class Test1 : MonoBehaviour
{
  ExternalVariable _position;
  Variable _wire;

  void Start()
  {
    Debug.Log("Start");

    _wire = new Variable();
    ShardsController.Env.Eval(@"(Wire ""test"" :Looped (Msg ""XXX"") .position (Log) (Pause 1.0))", _wire.Ptr);

    var position = new Vector3(3, 4, 5);
    _position = new ExternalVariable(_wire.Value.wire, "position", SHType.Float3);
    _position.Value.float3 = position.ToFloat3();

    Native.Core.Schedule(ShardsController.Mesh, _wire.Value.wire);
    Debug.Log($"[{gameObject.name}] Position: {_position.Value.float3.ToVector3()}");
  }

  void OnDestroy()
  {
    Debug.Log("OnDestroy");
    Native.Core.Unschedule(ShardsController.Mesh, _wire.Value.wire);
    _position.Dispose();
    _wire.Dispose();
  }

  // Update is called once per frame
  void Update()
  {
    var position = this.transform.position;
    _position.Value.float3 = position.ToFloat3();
    Debug.Log($"[{gameObject.name}] Position: {_position.Value.float3.ToVector3()}");
  }
}
