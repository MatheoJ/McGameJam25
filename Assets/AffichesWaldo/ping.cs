using UnityEngine;

public class ping : MonoBehaviour
{
    //public AudioClip sound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayPing()
    {
        //GetComponent<AudioSource>().clip = sound;
        GetComponent<AudioSource>().Play();
    }
}
