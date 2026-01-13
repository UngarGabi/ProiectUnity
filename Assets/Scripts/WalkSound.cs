using UnityEngine;

public class SunetPasiSimplu : MonoBehaviour
{
    AudioSource sunet;

    void Start()
    {
        sunet = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (!sunet.isPlaying)
            {
                sunet.Play();
            }
        }
        else
        {
            sunet.Stop();
        }
    }
}