using UnityEngine;

public class WeaponSound : MonoBehaviour
{
    public AudioSource sursaAudio;

    [Header("Numele armelor")]
    public string numePistol = "M1911"; 

    [Header("Sunete")]
    public AudioClip sunetPistol;
    public AudioClip sunetMelee;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
          
            if (VerificaDacaAmPistol())
            {
                sursaAudio.PlayOneShot(sunetPistol);
            }
            else
            {
                sursaAudio.PlayOneShot(sunetMelee);
            }
        }
    }

  
    bool VerificaDacaAmPistol()
    {
       
        Transform[] totiCopiii = GetComponentsInChildren<Transform>(true);

        foreach (Transform copil in totiCopiii)
        {
          
            if (copil.name.Contains(numePistol) && copil.gameObject.activeInHierarchy)
            {
                return true; 
            }
        }
        return false;  
    }
}