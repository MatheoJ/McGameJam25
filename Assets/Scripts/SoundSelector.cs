using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class SoundSelector : MonoBehaviour
{
    public List<AudioClip> clips = new List<AudioClip>();
    public AudioSource audiosource;
    public float probaOfNoSound = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audiosource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRandomSound()
    {
        float tirage=Random.Range(0f, 1f);
        if (tirage > probaOfNoSound)
        {
            int index = Random.Range(0, clips.Count);
            Debug.Log(index);
            audiosource.clip = clips[index];
            audiosource.Play();
            
        }
    }


}
