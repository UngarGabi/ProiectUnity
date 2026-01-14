using UnityEngine;

public enum ItemType
{
    Weapon,
    Consumable
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public GameObject itemPrefab;
    public ItemType itemType;
    public float attackDamage;
    public float healAmount;
    
}
