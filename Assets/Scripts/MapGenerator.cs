using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private int[,] map = { { 0, 1, 0, 1, 0 }, { 1, 0, 1, 0, 1 } };
    public GameObject prefab;
    void Start()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == 1)
                {
                    GameObject a = GameObject.Instantiate(prefab, new Vector3(i, j, 0), Quaternion.identity);
                }
            }
        }
    }

    void Update()
    {
        
    }
}
