using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTrees : MonoBehaviour
{
    public Terrain terrain; // 指定要提取樹木的 Terrain
    public float treeScale = 1f; // 樹木的縮放比例
    public bool clearOriginalTrees = false; // 是否清除原始樹木

    void Start()
    {
        CombineTreesFromTerrain();
    }

    void CombineTreesFromTerrain()
    {
        // 確保指定的 Terrain 不為空
        if (terrain == null)
        {
            Debug.LogError("請指定 Terrain！");
            return;
        }

        // 獲取 TerrainData
        TerrainData terrainData = terrain.terrainData;
        TreeInstance[] treeInstances = terrainData.treeInstances; // 所有樹木實例
        TreePrototype[] treePrototypes = terrainData.treePrototypes; // 樹木原型資料

        // 用於合併的 CombineInstance 列表
        CombineInstance[] combineInstances = new CombineInstance[treeInstances.Length];

        for (int i = 0; i < treeInstances.Length; i++)
        {
            TreeInstance tree = treeInstances[i];
            TreePrototype treePrototype = treePrototypes[tree.prototypeIndex];
            Mesh treeMesh = treePrototype.prefab.GetComponent<MeshFilter>().sharedMesh;
            Material treeMaterial = treePrototype.prefab.GetComponent<MeshRenderer>().sharedMaterial;

            // 計算樹的位置
            Vector3 treeWorldPos = Vector3.Scale(tree.position, terrainData.size) + terrain.transform.position;

            // 設置樹的縮放和旋轉
            Matrix4x4 matrix = Matrix4x4.TRS(
                treeWorldPos,
                Quaternion.Euler(0, tree.rotation * Mathf.Rad2Deg, 0),
                Vector3.one * treeScale * tree.widthScale
            );

            // 添加到合併數組中
            combineInstances[i].mesh = treeMesh;
            combineInstances[i].transform = matrix;
        }

        // 創建合併後的 Mesh
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combineInstances);

        // 創建新的 GameObject 並附加 Mesh
        GameObject combinedTreeObject = new GameObject("CombinedTrees");
        MeshFilter mf = combinedTreeObject.AddComponent<MeshFilter>();
        MeshRenderer mr = combinedTreeObject.AddComponent<MeshRenderer>();
        mf.mesh = combinedMesh;

        // 使用第一個樹的材質
        mr.material = treePrototypes[0].prefab.GetComponent<MeshRenderer>().sharedMaterial;

        // 是否清除原始樹木
        if (clearOriginalTrees)
        {
            terrainData.treeInstances = new TreeInstance[0];
        }

        Debug.Log("樹木已合併完成！");
    }
}
