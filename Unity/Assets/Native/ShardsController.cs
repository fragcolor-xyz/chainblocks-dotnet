using System;

using Fragcolor.Shards;

using UnityEngine;

public class ShardsController : MonoBehaviour
{
    private static bool _initialized;

    private static ScriptingEnv _env;
    private static SHMeshRef _mesh;

    public static ScriptingEnv Env
    {
        get
        {
            if (!_initialized)
                throw new InvalidOperationException();
            return _env;
        }
        private set { _env = value; }
    }

    public static SHMeshRef Mesh
    {
        get
        {
            if (! _initialized)
                throw new InvalidOperationException();
            return _mesh;
        }
        private set { _mesh = value; }
    }

    void Awake()
    {
        if (!_initialized)
        {
            Env = new ScriptingEnv();
            Mesh = Native.Core.CreateMesh();
            _initialized = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        Native.Core.Tick(Mesh);
    }
}
