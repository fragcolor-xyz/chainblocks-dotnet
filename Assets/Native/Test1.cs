using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using Chainblocks;

public class Test1 : MonoBehaviour
{
  ExternalVariable _position;

  // Start is called before the first frame update
  void Start()
  {
    Debug.Log("Start");

    using (var chain = new Variable())
    {
      Native.cbLispEval(ChainblocksGame.Env, "(Chain \"test\" :Looped (Msg \"XXX\") .position (Log) (Pause 1.0))", chain.Ptr);

      _position = new ExternalVariable(chain.Value.chainRef, "position");
      _position.Value.vector3 = new Vector3(3, 4, 5);
      _position.Value.type = 12;
      _position.Value.flags = (1 << 2);

      Native.Core.Schedule(ChainblocksGame.Node, chain.Value.chainRef);
    }
  }

  // Update is called once per frame
  void Update()
  {
    _position.Value.vector3 = this.transform.position;
  }
}
