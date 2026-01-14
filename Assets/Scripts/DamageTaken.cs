using UnityEngine;

public class SunetDamage : MonoBehaviour
{
   
    public AudioClip sunetLovitura; 
    
    //timer
    private float timer = 0f;

    void Update()
    {
        
        if (timer > 0) timer -= Time.deltaTime;
    }
    
    void OnCollisionEnter(Collision coliziune)
    {
       
        if (coliziune.gameObject.tag == "Inamic" && timer <= 0)
        {
           
            AudioSource.PlayClipAtPoint(sunetLovitura, transform.position, 1.0f);
            
            // Resetam timerul
            timer = 0.5f;
        }
    }
}