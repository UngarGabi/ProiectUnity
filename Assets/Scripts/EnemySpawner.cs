using UnityEngine;
using UnityEngine.TerrainUtils;

public class EnemySpawner : MonoBehaviour
{
    [Header("Reference")]
    public Transform playerPosition;
    public Terrain mapTerrain;
    public GameObject enemyPrefab;

    [Header("SpawnPositions")]
    public float minDistanceFromPlayer = 10.0f;
    public float maxDistanceFromPlayer = 40.0f;
    public float spawnInterval = 0.5f;       
    public int maxEnemies = 10;

    [Header("Obstacles")]
    public float spawnHeightOffset = 0.05f;
    public float maxInclineSpawn = 20.0f;
    public float obstacleCheckRadius = 0.75f; 
    public LayerMask obstacleMask;

    private float spawnTimer;
    private float enemyHalfHeight;


    private void Awake()
    {
        if (enemyPrefab != null)
        {
            Collider col = enemyPrefab.GetComponentInChildren<Collider>();
            if (col != null)
            {
                enemyHalfHeight = col.bounds.extents.y;
            }
            else
            {
                enemyHalfHeight = 0.5f; 
            }
        }
    }

    void Start()
    {
        spawnTimer = spawnInterval;
    }
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0.0f)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 enemySpawnPosition = GetSpawnPosition();
        if (CheckSpawnPosition(ref enemySpawnPosition))
            Instantiate(enemyPrefab, enemySpawnPosition, enemyPrefab.transform.rotation);
    }
    private Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        
        float distanceFromPlayer = Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
        float angle = Random.Range(0.0f, Mathf.PI * 2.0f); ; // se alege un unghi intre 0 - 360(2pi)

        Vector3 offset = new Vector3(Mathf.Cos(angle), 0.0f, Mathf.Sin(angle)) * distanceFromPlayer;
        // transformam toate componentele intr-o pozitie
        // Cos - afla componenta de pe Ox
        // Sin - afla componenta de pe Oy
        // distanceFromPlayer - impinge pozitia la directia dorita
        // pe scurt ce se intampla, distanceFromPlayer este o raza de la player la inelul cercului,
        // iar codul calculeaza un punct random de pe inelul cercului facut de acea raza

        spawnPosition = playerPosition.position + offset; // calculam pozitia fata de jucator

        return spawnPosition;
    }

    private bool CheckSpawnPosition(ref Vector3 spawnPosition)
    {
        TerrainData data = mapTerrain.terrainData;
        Vector3 terrainPos = mapTerrain.transform.position;
        
        // culeg limitele hartii
        float terrainMinX = terrainPos.x;
        float terrainMaxX = terrainPos.x + data.size.x;
        float terrainMinZ = terrainPos.z;
        float terrainMaxZ = terrainPos.z + data.size.z;

        // fac in asa fel incat sa am punctul pe harta, chiar daca spawnpoint-ul luat este in afara ei
        // Clamp imi baga in harta punctele alese in afara ei, aducand punctul la min sau max in functie de ce este mai aproape
        spawnPosition.x = Mathf.Clamp(spawnPosition.x, terrainMinX, terrainMaxX);
        spawnPosition.z = Mathf.Clamp(spawnPosition.z, terrainMinZ, terrainMaxZ);


        // luam inaltimea terenului in punctul de interes si adaugam y-ul terenului in cazul in care nu este terenul setat la (0,0,0)
        // offset pt inaltime ca sa nu se spawneze in paman obiectul
        float height = mapTerrain.SampleHeight(spawnPosition) + terrainPos.y;
        spawnPosition.y = height + spawnHeightOffset + enemyHalfHeight;


    // normalizez pozitia de spawn ca sa verific ca nu este prea abrut
        float normalizedX = (spawnPosition.x - terrainPos.x) / data.size.x;
        float normalizedZ = (spawnPosition.z - terrainPos.z) / data.size.z;

        //Steepsness -> in grade, cat de abrupt este terenul intr-un punct
        float steepness = data.GetSteepness(normalizedX, normalizedZ); 
        if (steepness > maxInclineSpawn)
            return false;


        // creeaza o sfera intr-un punct care verifica daca se loveste de un layer
        if (Physics.CheckSphere(spawnPosition, obstacleCheckRadius, obstacleMask))
            return false;


        return true;

    }

    public bool CheckSpawnPositionPublic(ref Vector3 spawnPosition)
    {
        return CheckSpawnPosition(ref spawnPosition);
    }



}
