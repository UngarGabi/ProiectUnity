using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public ItemData[] inventory;
    private int inventorySpace = 3;

    private void Awake()
    {
        if (inventory == null || inventory.Length == 0)
        {
            inventory = new ItemData[inventorySpace];
        }
    }
    void Start()
    {

    }

    void Update()
    {

    }

    public bool AddItem(ItemData item)
    {
        if (item == null)
        {
            Debug.LogWarning("Tried to add null item to inventory");
            return false;
        }

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                return true;
            }
        }
        return false;
    }

    public void DropItem(int itemIndex)
    {
        if (!IsValidIndex(itemIndex)) 
            return;

        inventory[itemIndex] = null;


    }

    private bool IsValidIndex(int index)
    {
        if (index < 0 || index >= inventory.Length)
        {
            Debug.LogWarning($"[Inventory] Slot index invalid: {index}");
            return false;
        }
        return true;
    }
}
