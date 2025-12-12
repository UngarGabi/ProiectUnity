using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public ItemData itemData;
    private Collider _col; // collider-ul obiectului
    private bool pickedAlready; // variabila care verifica daca obiectul a fost adaugat deja

    private void Awake()
    {
        _col = GetComponent<Collider>();
    }
    void Start()
    {

    }

    void Update()
    {
        ItemSpin();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pickedAlready) return;

        PlayerInventory inventory = other.GetComponent<PlayerInventory>(); // inventar player
        if (inventory != null)
        {
            pickedAlready = true;                 // blocheaza dublarea
            if (_col != null) _col.enabled = false;  // opreste triggerul imediat

            bool addedItem = inventory.AddItem(itemData); // adaugare item in inventar


            if (addedItem)
                Destroy(gameObject);
            else
                Debug.Log("Inventory full");
        }
        else
        {
            Debug.Log("No inventory");
            pickedAlready = false;
            if (_col != null) _col.enabled = true;
        }
    }

    private void ItemSpin()
    {
        float rotationMoveSpeed = 90.0f;
        transform.Rotate(Vector3.up, rotationMoveSpeed * Time.deltaTime);
    }
}
