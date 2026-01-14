using UnityEngine;

public class ConsumeHealthPotion : MonoBehaviour
{
    [SerializeField] private float holdTime = 1f;

    private float holdTimer;

    private PlayerInventory inventory;
    private HealthSystem health;

    private void Awake()
    {
        inventory = GetComponent<PlayerInventory>();
        health = GetComponent<HealthSystem>();
    }

    private void Update()
    {
        if (inventory == null || health == null)
            return;

        int index = inventory.inventoryIndex;

        if (index < 0)
        {
            holdTimer = 0f;
            return;
        }

        // protectie index 
        if (index >= inventory.inventory.Length)
        {
            holdTimer = 0f;
            return;
        }

        ItemData item = inventory.inventory[index];

        if (item == null || item.itemType != ItemType.Consumable)
        {
            holdTimer = 0f;
            return;
        }

        Debug.Log("Index = " + inventory.inventoryIndex);

        if (Input.GetKey(KeyCode.P))
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= holdTime)
            {
                Consume(item, index);
                holdTimer = 0f;
            }
        }
        else
        {
            holdTimer = 0f;
        }
    }

    private void Consume(ItemData item, int index)
    {
        Debug.Log("CONSUME called, heal=" + item.healAmount);
        // heal
        health.Heal(item.healAmount);
        inventory.ConsumeSelectedItem(); // scoate din inventar + scoate din mana
    }
}
