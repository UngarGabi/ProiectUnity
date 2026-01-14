using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public ItemData[] inventory;
    private int inventorySpace = 3;
    public int inventoryIndex = -1;
    [SerializeField]
    public Transform itemPosition;
    public Transform playerPosition;
    private GameObject equippedObject;

    private void Awake()
    {
        if (inventory == null || inventory.Length == 0)
        {
            inventory = new ItemData[inventorySpace];
        } 
        
    }

    void Update()
    {
        int previousIndex = inventoryIndex;
        SelectIndex();
        if (IsValidIndex(inventoryIndex) && (previousIndex != inventoryIndex)) // prevenim punerea a 2 obiecte in mana in ac timp
            EquipItem(inventory[inventoryIndex]);

        if (Input.GetKeyDown(KeyCode.G))
        {
            DropItem();
        }
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

    public void DropItem()
    {
        if (!IsValidIndex(inventoryIndex)) 
            return;

        int idx = inventoryIndex;

        ItemData item = inventory[inventoryIndex];
        if (item == null) return;

        if (item.itemPrefab != null)
        {
            Vector3 dropPos = playerPosition.position + playerPosition.forward * 1.0f;
            Quaternion dropRot = Quaternion.identity; // sau playerPosition.rotation

            Instantiate(item.itemPrefab, dropPos, dropRot);
        }
        else
        {
            Debug.LogWarning($"{item.itemName} nu are worldPrefab setat pentru drop!");
        }
        inventory[idx] = null;
        inventoryIndex = -1;
        Unequip();


    }

    private bool IsValidIndex(int index) // validare index inventar
    {
        if (index < 0 || index >= inventory.Length)
        {
            return false;
        }
        return true;
    }

    private void SelectIndex() // iau input pentru ce obiect vreau din inventar
    {
        
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                inventoryIndex = 0;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                inventoryIndex = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                inventoryIndex = 2;
            }
    }

    private void EquipItem(ItemData item)
    {
        // scoatem ce era echipat inainte
        Unequip();

        if (item == null) return;
        if (item.itemPrefab == null)
        {
            Debug.LogWarning($"{item.itemName} nu are itemPrefab setat!");
            return;
        }

        // instantiem ca child (ca sa urmeze playerul)
        equippedObject = Instantiate(item.itemPrefab, itemPosition);

        // reset local 
        equippedObject.transform.localPosition = Vector3.zero;
        equippedObject.transform.localRotation = Quaternion.identity;

        if (equippedObject.GetComponent<RangedAttack>() == null)
        {
            equippedObject.transform.Rotate(90f, 0f, 0f);
            equippedObject.transform.localScale = Vector3.one;
        }



        // dezactivam fizica / collider-ele ca sa nu declanseze pickup-uri
        DisablePhysics(equippedObject);

        var droppedItem  = equippedObject.GetComponentInChildren<DroppedItem>();
        if (droppedItem != null) 
            droppedItem.enabled = false;
    }

    private void Unequip() // dezactivarea obiectului care e in mana
    {
        if (equippedObject != null)
        {
            Destroy(equippedObject);
            equippedObject = null;
        }
    }

    private void DisablePhysics(GameObject obj) // dezactiveaza collidere
    {
        foreach (var col in obj.GetComponentsInChildren<Collider>())
            col.enabled = false;

        foreach (var rb in obj.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
    }

    public void ConsumeSelectedItem() // pentru potiuni
    {
        if (!IsValidIndex(inventoryIndex))
            return;

        inventory[inventoryIndex] = null;
        inventoryIndex = -1;

        Unequip(); 
    }

}
