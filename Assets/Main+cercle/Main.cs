using UnityEngine;

public class Main : MonoBehaviour
{
    public float hauteur = 20;
    public Transform cercle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(cercle.position.x , cercle.position.y + hauteur, cercle.position.z + hauteur);
    }
}
