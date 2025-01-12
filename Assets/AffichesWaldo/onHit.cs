using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.VFX;

public class onHit : MonoBehaviour
{
    public GameObject canvas;
    public Cercle cercle;
    public bool stunMode=true;
    //public VisualEffectAsset effectAsset;
    public VisualEffect VisualEffect;
    //public AudioClip ping;
    private GameManager gm;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
        if (gm == null)
        {
            UnityEngine.Debug.LogError("GameManager not found!");
        }
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
            gm.puzzleCompleted();

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