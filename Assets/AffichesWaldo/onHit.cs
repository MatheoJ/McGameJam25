using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class onHit : MonoBehaviour
{
    public GameObject canvas;
    public Cercle cercle;
    public bool stunMode=true;
    //public VisualEffectAsset effectAsset;
    public VisualEffect VisualEffect;
    public AudioClip ping;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hit()
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
            //VisualEffect.enabled = true;
            StartCoroutine(Vfx());
            GetComponentInParent<ping>().PlayPing();
            GetComponent<SoundSelector>().PlayRandomSound();
        }
        if (cercle != null)
        {
            if(stunMode) cercle.StunHand();
            else cercle.baitHand();
        }


    }

    IEnumerator Vfx()
    {
        VisualEffect.enabled = true;
        yield return new WaitForSeconds(1f);
        VisualEffect.enabled = false;
    }
}
