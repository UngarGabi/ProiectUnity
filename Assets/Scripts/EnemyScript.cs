using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

enum EnemyStae
{
    Wander,
    Chase,
}
public class EnemyScript : MonoBehaviour
{
    public GameObject Player;
    private Vector3 playerPosition;
    private float distancePlayerToEnemy;
    private float minimumDistance = 10.0f;
    private float chaseSpeed = 5.0f;
    void Start()
    {
       
    }

    void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        distancePlayerToEnemy = Vector3.Distance(playerPosition, transform.position);

        if (distancePlayerToEnemy < minimumDistance)

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPosition.x, transform.position.y, playerPosition.z), chaseSpeed * Time.deltaTime);
    }
}
