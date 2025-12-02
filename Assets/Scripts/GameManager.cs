using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject treePrefab;
    
    private float spawnZoneX;
    private float spawnZoneY;
    public float prefabCount;
    public float YRangeTrees = 400;
    public float xRangeTrees = 100;

    [Header("Layer Mask")]
    public LayerMask obstacleLayers;
    void Start()
    {
        //SpawnTrees();
    }

    void Update()
    {
        
    }

    void SpawnTrees()
    {
        for (int i = 0; i < prefabCount; i++) {
            spawnZoneX = Random.Range(xRangeTrees, YRangeTrees);
            spawnZoneY = Random.Range(xRangeTrees, YRangeTrees);

            Vector3 treePosition = new Vector3(spawnZoneX, 0, spawnZoneY);
            Instantiate(treePrefab, treePosition, treePrefab.GetComponent<Transform>().rotation);

        }
    }
}
