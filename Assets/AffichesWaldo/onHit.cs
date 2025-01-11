using UnityEngine;

public class onHit : MonoBehaviour
{
    public GameObject canvas;
    public Cercle cercle;
    public bool stunMode=true;
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
        }
        if (cercle != null)
        {
            if(stunMode) cercle.StunHand();
            else cercle.baitHand();
        }


    }
}
