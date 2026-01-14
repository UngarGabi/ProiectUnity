using UnityEngine;

public class WinDoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("AI CASTIGAT!");

            // stop joc
            Time.timeScale = 0f;

         
        }
    }
}
