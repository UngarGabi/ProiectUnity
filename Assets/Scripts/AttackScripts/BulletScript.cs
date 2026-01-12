using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody rb;
    private float attackDamage;
    private float lifeTimer;

    [SerializeField] private float speed = 30f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private LayerMask hitMask;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    // SE CHEAM? C�ND TRAGI
    public void Fire(Vector3 direction, float damage)
    {
        attackDamage = damage;
        lifeTimer = lifeTime;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.AddForce(direction.normalized * speed, ForceMode.Impulse);
    }

    private void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((hitMask.value & (1 << collision.gameObject.layer)) == 0)
            return;

        HealthSystem health = collision.gameObject.GetComponentInParent<HealthSystem>();
        if (health != null)
        {
            health.TakeDamage(attackDamage);
        }

        Destroy(gameObject);
    }
}