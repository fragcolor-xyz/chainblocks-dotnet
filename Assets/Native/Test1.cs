using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using Chainblocks;

public class Test1 : MonoBehaviour
{
  IntPtr _env;

  ExternalVariable _position;

  IntPtr _node;

  // Start is called before the first frame update
  void Start()
  {
    Debug.Log("Start");

    _env = Native.cbLispCreate("");

    using (var chain = new Variable())
    {
      Native.cbLispEval(_env, "(Chain \"test\" :Looped (Msg \"XXX\") .position (Log) (Pause 1.0))", chain.Ptr);

      _position = new ExternalVariable(chain.Value.chainRef, "position");
      _position.Value.vector3 = new Vector3(3, 4, 5);
      _position.Value.type = 12;
      _position.Value.flags = (1 << 2);

      _node = Native.Core.CreateNode();
      Native.Core.Schedule(_node, chain.Value.chainRef);
    }
  }

  // Update is called once per frame
  void Update()
  {
    _position.Value.vector3 = this.transform.position;
    Native.Core.Tick(_node);
  }
}
