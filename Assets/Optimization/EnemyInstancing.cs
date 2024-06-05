using UnityEngine;

public class DualMaterialInstancing : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 100;
    public Material[] enemyMaterials; // �� ���� ��Ƽ���� �迭

    private Matrix4x4[] matrices;
    private MaterialPropertyBlock materialPropertyBlock;

    void Start()
    {
        matrices = new Matrix4x4[enemyCount];
        materialPropertyBlock = new MaterialPropertyBlock();

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(-10.0f, 10.0f),
                0,
                Random.Range(-10.0f, 10.0f)
            );

            Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            Vector3 scale = Vector3.one;

            matrices[i] = Matrix4x4.TRS(position, rotation, scale);
        }
    }

    void Update()
    {
        // ù ��° ��Ƽ���� ���� �ν��Ͻ�
        Graphics.DrawMeshInstanced(
            enemyPrefab.GetComponent<MeshFilter>().sharedMesh,
            0,
            enemyMaterials[0],
            matrices,
            enemyCount / 2,
            materialPropertyBlock
        );

        // �� ��° ��Ƽ���� ���� �ν��Ͻ�
        Graphics.DrawMeshInstanced(
            enemyPrefab.GetComponent<MeshFilter>().sharedMesh,
            0,
            enemyMaterials[1],
            matrices,
            enemyCount / 2,
            materialPropertyBlock
        );
    }
}
