using UnityEditor;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject Tile;
    private BoxCollider StageArea;

    void Start()
    {
        StageArea = GetComponent<BoxCollider>();

        for (int i = 0; i < StageArea.size.x; i++)
        {
            for (int j = 0; j < StageArea.size.z; j++)
            {
                Vector3 position = new Vector3(
                    transform.position.x - StageArea.size.x / 2 + i + 0.5f,
                    0f,
                    transform.position.z - StageArea.size.z / 2 + j + 0.5f
                );
                Instantiate(Tile, position, Quaternion.identity, transform);
            }
        }
    }
}
