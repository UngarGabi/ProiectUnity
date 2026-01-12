using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawnerFunction; 
    [SerializeField] private Terrain mapTerrain;
    [SerializeField] private GameObject[] itemPrefabs;
    void Start()
    {
        if (itemPrefabs == null || itemPrefabs.Length == 0)
        {
            Debug.LogError("ItemSpawner: itemPrefabs este gol!");
            return;
        }

        int numberOfItemsInTheWorld = Random.Range(50, 60); // intre 20 si 30 de iteme
       
        for (int i = 0; i < numberOfItemsInTheWorld; i++)
        {
            Vector3 positionInTheMap = getRandomPointOnMap();

            bool itemPlaced = false;
            for(int tries = 0; tries < 15; tries++)
            {
                positionInTheMap = getRandomPointOnMap();

                if(enemySpawnerFunction.CheckSpawnPositionPublic(ref positionInTheMap))
                {
                    GameObject itemPrefab = GetRandomItemPrefab();
                    Instantiate(itemPrefab, positionInTheMap, Quaternion.identity);
                    itemPlaced = true;
                    break;
                }

            }
        }
            
    }
     
    void Update()
    {

    }

    private Vector3 getRandomPointOnMap() // iau o pozitie pentru a da spawn la iteme
    {
        TerrainData data = mapTerrain.terrainData;
        Vector3 terrainPos = mapTerrain.transform.position;

        // culeg limitele hartii
        float terrainMinX = terrainPos.x;
        float terrainMaxX = terrainPos.x + data.size.x;
        float terrainMinZ = terrainPos.z;
        float terrainMaxZ = terrainPos.z + data.size.z;

        float randomX = Random.Range(terrainMinX, terrainMaxX); // x random
        float randomZ = Random.Range(terrainMinZ, terrainMaxZ); // y random

        float heightMap = mapTerrain.SampleHeight(new Vector3(randomX, 0.0f, randomZ)) + terrainPos.y; // inaltimea din punctul x,z

        return new Vector3(randomX, heightMap, randomZ);
    }

    private GameObject GetRandomItemPrefab()
    {
        return itemPrefabs[Random.Range(0, itemPrefabs.Length)];
    }
}
