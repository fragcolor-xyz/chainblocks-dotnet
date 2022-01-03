using Chainblocks;
using UnityEngine;

public class Test1 : MonoBehaviour
{
  ExternalVariable _position;
  Variable _chain;

  // Start is called before the first frame update
  void Start()
  {
    Debug.Log("Start");

    _chain = new Variable();
    Native.cbLispEval(ChainblocksGame.Env, "(Chain \"test\" :Looped (Msg \"XXX\") .position (Log) (Pause 1.0))", _chain.Ptr);

    var position = new Vector3(3, 4, 5);
    _position = new ExternalVariable(_chain.Value.chainRef, "position");
    _position.Value.float3 = position.ToFloat3();
    _position.Value.type = CBType.Float3;
    _position.Value.flags = (1 << 2);

    Native.Core.Schedule(ChainblocksGame.Node, _chain.Value.chainRef);
  }

  void OnDestroy()
  {
    Debug.Log("OnDestroy");
    Native.Core.Unschedule(ChainblocksGame.Node, _chain.Value.chainRef);
    _position.Dispose();
    _chain.Dispose();
  }

  // Update is called once per frame
  void Update()
  {
    var position = this.transform.position;
    _position.Value.float3 = position.ToFloat3();
  }
}
