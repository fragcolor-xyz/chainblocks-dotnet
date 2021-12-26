using UnityEngine;
using UnityEditor.AssetImporters;
using System.IO;

// [ScriptedImporter(1, "edn")]
// public class EdnImporter : ScriptedImporter
// {
//   // public float m_Scale = 1;

//   public override void OnImportAsset(AssetImportContext ctx)
//   {
//     // var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
//     // var position = JsonUtility.FromJson<Vector3>(File.ReadAllText(ctx.assetPath));

//     // cube.transform.position = position;
//     // cube.transform.localScale = new Vector3(m_Scale, m_Scale, m_Scale);

//     // // 'cube' is a GameObject and will be automatically converted into a prefab
//     // // (Only the 'Main Asset' is eligible to become a Prefab.)
//     // ctx.AddObjectToAsset("main obj", cube);
//     // ctx.SetMainObject(cube);

//     // var material = new Material(Shader.Find("Standard"));
//     // material.color = Color.red;

//     // // Assets must be assigned a unique identifier string consistent across imports
//     // ctx.AddObjectToAsset("my Material", material);

//     // // Assets that are not passed into the context as import outputs must be destroyed
//     // var tempMesh = new Mesh();
//     // DestroyImmediate(tempMesh);
//   }
// }

//! Class name must be equal to file name or unity breaks...
[ScriptedImporter(1, "frag")]
public class FragImporter : ScriptedImporter
{
  // public float m_Scale = 1;

  [System.Serializable]
  public class FragRef
  {
    public string ipfsHash;
  }

  public override void OnImportAsset(AssetImportContext ctx)
  {
    var frag = JsonUtility.FromJson<FragRef>(File.ReadAllText(ctx.assetPath));
    Debug.Log(frag.ipfsHash);
    var obj = new GameObject();
    ctx.AddObjectToAsset("ipfs hash", obj);
    ctx.SetMainObject(obj);
    // var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    // var position = JsonUtility.FromJson<Vector3>(File.ReadAllText(ctx.assetPath));

    // cube.transform.position = position;
    // cube.transform.localScale = new Vector3(m_Scale, m_Scale, m_Scale);

    // // 'cube' is a GameObject and will be automatically converted into a prefab
    // // (Only the 'Main Asset' is eligible to become a Prefab.)
    // ctx.AddObjectToAsset("main obj", cube);
    // ctx.SetMainObject(cube);

    // var material = new Material(Shader.Find("Standard"));
    // material.color = Color.red;

    // // Assets must be assigned a unique identifier string consistent across imports
    // ctx.AddObjectToAsset("my Material", material);

    // // Assets that are not passed into the context as import outputs must be destroyed
    // var tempMesh = new Mesh();
    // DestroyImmediate(tempMesh);
  }
}