using UnityEngine;

public enum EnemyState
{
    Wander,
    Chase
}

public class EnemyScript : MonoBehaviour
{
    public float chaseSpeed = 5.0f;
    public float wanderSpeed = 2.0f;
    public float minimumDistance = 10.0f;
    public float wanderRadius = 5.0f;

    private Transform playerTransform;
    private EnemyState currentState;
    private Vector3 wanderTarget;
    private float distanceToPlayer;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }

        currentState = EnemyState.Wander;
        GetNewWanderPosition();
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < minimumDistance)
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            currentState = EnemyState.Wander;
        }

        switch (currentState)
        {
            case EnemyState.Wander:
                WanderBehavior();
                break;
            case EnemyState.Chase:
                ChaseBehavior();
                break;
        }
    }

    void ChaseBehavior()
    {
        Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, chaseSpeed * Time.deltaTime);
    }

    void WanderBehavior()
    {
        Vector3 moveSpot = new Vector3(wanderTarget.x, transform.position.y, wanderTarget.z);
        transform.position = Vector3.MoveTowards(transform.position, moveSpot, wanderSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveSpot) < 0.2f)
        {
            GetNewWanderPosition();
        }
    }

    void GetNewWanderPosition()
    {
        Vector2 randomPoint = Random.insideUnitCircle * wanderRadius;
        wanderTarget = new Vector3(transform.position.x + randomPoint.x, transform.position.y, transform.position.z + randomPoint.y);
    }
}