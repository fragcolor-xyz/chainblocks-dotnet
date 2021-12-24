using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using Chainblocks;

public class Test1 : MonoBehaviour
{
  CBVar ActivateUnmanaged(IntPtr context, IntPtr input)
  {
    Native.Core.Suspend(context, 2.0);
    var res = new CBVar();
    res.type = 12;
    res.vector3 = new Vector3(1, 2, 3);
    //! WE CAN'T TOUCH THE GC HERE or we crash...
    return res;
  }

  ActivateUnmanagedDelegate _activateUnmanagedDelegate;

  IntPtr _env;

  IntPtr _positionPtr;

  IntPtr _node;

  // Start is called before the first frame update
  void Start()
  {
    Debug.Log("Start");

    _env = Native.cbLispCreate("");

    _activateUnmanagedDelegate = new ActivateUnmanagedDelegate(ActivateUnmanaged);
    var fptr = Marshal.GetFunctionPointerForDelegate(_activateUnmanagedDelegate);
    var sptr = "0x" + fptr.ToString("X");
    var chain = Native.cbLispEval(_env, "(do (defloop test (Msg \"XXX\") (UnsafeActivate! " + sptr + ") (ExpectFloat3) (Log) .position (Log)) test)");

    var fooPosition = new CBVar();
    fooPosition.vector3 = new Vector3(3, 4, 5);
    fooPosition.type = 12;
    fooPosition.flags = (1 << 2);
    _positionPtr = Marshal.AllocHGlobal(Marshal.SizeOf(fooPosition));
    Marshal.StructureToPtr(fooPosition, _positionPtr, false);
    Native.Core.SetExternalVariable(chain.chainRef, "position", _positionPtr);

    _node = Native.Core.CreateNode();
    Native.Core.Schedule(_node, chain.chainRef);
  }

  // Update is called once per frame
  void Update()
  {
    var position = new CBVar();
    position.vector3 = this.transform.position;
    position.type = 12;
    position.flags = (1 << 2);
    // TODO I can't remember but I'm sure there might be a faster way to do this
    Marshal.StructureToPtr(position, _positionPtr, false);
    Native.Core.Tick(_node);
  }
}
