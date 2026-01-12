using UnityEngine;

public class PlayerDamageScript : MonoBehaviour
{
    [SerializeField] public float baseDamage = 10f; // damage de baza al jucatorului
    private float itemDamageBonus = 0f; // damage care va veni din arma
   
    void Update()
    {
        
    }

    public float GetCurrentDamage() // returneaza damage-ul total base + weapon
    {
        return baseDamage + itemDamageBonus;
    }

    public void SetItemBonus(float bonus) // aplica damage-ul armei
    {
        if (bonus < 0f)
            itemDamageBonus = 0f;
        else
            itemDamageBonus = bonus;
    }
}
