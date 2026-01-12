using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Wander,
    Chase,
    Attack,
    Idle
}

public class EnemyScript : MonoBehaviour
{
    public float chaseSpeed = 5.0f;
    public float wanderSpeed = 2.0f;
    public float minimumDistance = 10.0f;
    public float wanderRadius = 5.0f;
    public float baseDamage = 10.0f;

    private Transform playerTransform;
    private EnemyState currentState;
    private Vector3 wanderTarget;
    private float distanceToPlayer;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius = 1.2f;
    [SerializeField] private LayerMask targetMask;
    private bool isAttacking = false;

    private float idleDuration = 0.8f; 
    private float idleTimer;

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

        if (currentState != EnemyState.Attack && currentState != EnemyState.Idle)
        {
            if (distanceToPlayer < minimumDistance)
            {
                currentState = EnemyState.Chase;
            }
            else
            {
                currentState = EnemyState.Wander;
            }
        }

        switch (currentState)
        {
            case EnemyState.Wander:
                WanderBehavior();
                break;
            case EnemyState.Chase:
                ChaseBehavior();
                break;
            case EnemyState.Attack:
                AttackBehavior();
                isAttacking = false;
                idleTimer = idleDuration;
                currentState = EnemyState.Idle;
                break;
            case EnemyState.Idle:
                IdleBehavior();
                break;
        }
    }
    void IdleBehavior()
    {
        idleTimer -= Time.deltaTime;

        if (idleTimer <= 0f)
        {
            currentState = EnemyState.Wander;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentState = EnemyState.Attack;
            isAttacking = true;
        }
    }

    private void AttackBehavior()
    {
        Collider[] hits = Physics.OverlapSphere(attackPoint.position, radius, targetMask);

        HashSet<HealthSystem> alreadyHit = new HashSet<HealthSystem>();

        for (int i = 0; i < hits.Length; i++)
        {
            HealthSystem health = hits[i].GetComponentInParent<HealthSystem>();
            if (health == null)
                continue;

            if (alreadyHit.Contains(health))
                continue;

            alreadyHit.Add(health);
            health.TakeDamage(baseDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}