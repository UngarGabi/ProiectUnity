using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private PlayerDamageScript damageStats; // script-ul cu damage stats
    private ItemData[] inventoryObjects; // referinta la inventar
    private int itemIndex; // index-ul la care suntem
    public float currentAttackDamage; // cat dmg o sa dea player-ul
    private PlayerInventory inventoryRef;

    [Header("Hit settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius = 1.2f;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private float attackCooldown = 0.4f;
    
    [SerializeField] private float attackAngle = 90f; // unghi total (ex: 90 = 45 stg + 45 dr)
    [SerializeField] private float enemyCenterHeightOffset = 0.9f; // ca sa nu calculeze spre picioare


    private float cooldownTimer;
    void Start()
    {
        currentAttackDamage = damageStats.baseDamage; // base damage la inceput
        inventoryRef = GetComponent<PlayerInventory>(); // referinta inventar
    }

    void Update()
    {
        inventoryObjects = inventoryRef.inventory; // luam inventar
        itemIndex = inventoryRef.inventoryIndex; // luam indexul

        if (itemIndex >= 0)
            currentAttackDamage = getTheAttackDamage(); // setam damage-ul
        else
            currentAttackDamage = damageStats.baseDamage;

        // la click ataca
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        cooldownTimer -= Time.deltaTime; // update la cooldown
    }

    private float getTheAttackDamage() // functie pentru a lua damage-ul
    {
        if(inventoryObjects[itemIndex] == null) // daca nu avem item dam inapoi base damage
            return damageStats.baseDamage;

        damageStats.SetItemBonus(inventoryObjects[itemIndex].attackDamage); // setam damage-ul ca fiind base damage + item de pe index
        return damageStats.GetCurrentDamage(); 
    }

    private void Attack()
    {
        if (attackPoint == null) return;

        if (cooldownTimer > 0f)
            return;

        cooldownTimer = attackCooldown;

        Collider[] hits = Physics.OverlapSphere(attackPoint.position, radius, targetMask);
        Debug.Log("Hits: " + hits.Length);

        HashSet<HealthSystem> alreadyHit = new HashSet<HealthSystem>();

        // jumatate din unghi: ex 90 total -> 45 pe fiecare parte
        float halfAngle = attackAngle * 0.5f;
        float minDot = Mathf.Cos(halfAngle * Mathf.Deg2Rad);

        Vector3 origin = transform.position;
        Vector3 forward = transform.forward;

        for (int i = 0; i < hits.Length; i++)
        {
            HealthSystem health = hits[i].GetComponentInParent<HealthSystem>();
            Debug.Log("Health found? " + (health != null));

            if (health == null)
                continue;

            if (alreadyHit.Contains(health))
                continue;

            // punct mai sus ca sa fie mai stabil
            Vector3 targetPos = health.transform.position + Vector3.up * enemyCenterHeightOffset;

            Vector3 dirToTarget = targetPos - origin;
            dirToTarget.y = 0f; // ignoram diferenta de inaltime (doar XZ)
            dirToTarget.Normalize();

            float dot = Vector3.Dot(forward, dirToTarget);

            // daca e in spate sau prea in lateral, nu lovim
            if (dot < minDot)
                continue;

            alreadyHit.Add(health);
            Debug.Log("playerhit");
            health.TakeDamage(currentAttackDamage);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }




}
