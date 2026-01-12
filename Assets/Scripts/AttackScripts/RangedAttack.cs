using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float attackCooldown = 0.4f;
    [SerializeField] private float bulletDamage = 40.0f;

    private float cooldownTimer;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        cooldownTimer -= Time.deltaTime;
    }

    private void Shoot()
    {
        if (cooldownTimer > 0f)
            return;

        cooldownTimer = attackCooldown;

        GameObject bulletObj = Instantiate(
            bulletPrefab,
            bulletSpawnPoint.position,
            bulletSpawnPoint.rotation
        );

        BulletScript bullet = bulletObj.GetComponent<BulletScript>();
        if (bullet != null)
        {
            bullet.Fire(bulletSpawnPoint.forward, bulletDamage);
        }
    }
}
