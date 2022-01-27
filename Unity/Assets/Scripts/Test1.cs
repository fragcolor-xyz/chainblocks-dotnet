/* SPDX-License-Identifier: BUSL-1.1 */
/* Copyright © 2022 Fragcolor Pte. Ltd. */

using Fragcolor.Chainblocks;
using Fragcolor.Chainblocks.UnityEngine;

using UnityEngine;

public class Test1 : MonoBehaviour
{
  ExternalVariable _position;
  Variable _chain;

  void Start()
  {
    Debug.Log("Start");

    _chain = new Variable();
    ChainblocksController.Env.Eval(@"(Chain ""test"" :Looped (Msg ""XXX"") .position (Log) (Pause 1.0))", _chain.Ptr);

    var position = new Vector3(3, 4, 5);
    _position = new ExternalVariable(_chain.Value.chain, "position", CBType.Float3);
    _position.Value.float3 = position.ToFloat3();

    Native.Core.Schedule(ChainblocksController.Node, _chain.Value.chain);
    Debug.Log($"[{gameObject.name}] Position: {_position.Value.float3.ToVector3()}");
  }

  void OnDestroy()
  {
    Debug.Log("OnDestroy");
    Native.Core.Unschedule(ChainblocksController.Node, _chain.Value.chain);
    _position.Dispose();
    _chain.Dispose();
  }

  // Update is called once per frame
  void Update()
  {
    var position = this.transform.position;
    _position.Value.float3 = position.ToFloat3();
    Debug.Log($"[{gameObject.name}] Position: {_position.Value.float3.ToVector3()}");
  }
}
