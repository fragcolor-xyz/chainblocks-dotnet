using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Chainblocks;

public class ChainblocksGame : MonoBehaviour
{
  static public IntPtr Env
  {
    get;
    private set;
  }

  static public IntPtr Node
  {
    get;
    private set;
  }

  void Awake()
  {
    if (Env == IntPtr.Zero)
    {
      Env = Native.cbLispCreate("");
      Node = Native.Core.CreateNode();
      DontDestroyOnLoad(this.gameObject);
    }
    else
    {
      Destroy(this.gameObject);
    }
  }

  void Update()
  {
    Native.Core.Tick(Node);
  }
}