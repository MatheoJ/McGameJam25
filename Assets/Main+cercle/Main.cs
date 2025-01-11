using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
    public float hauteur = 20;
    private float saveHauteur;
    public Transform cercle;
    private float distanceMax = 12;
    public float vitesse = 5;
    private bool recherche = true;
    //public Cercle cercle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveHauteur = hauteur;
    }

    // Update is called once per frame
    void Update()
    {
        if (recherche)
        {
            transform.position = new Vector3(cercle.position.x, cercle.position.y + hauteur, cercle.position.z + hauteur);
        }
    }

    public IEnumerator Appuyer()
    {
        while (hauteur - distanceMax > 0.1)
        {
            hauteur = Mathf.Lerp(hauteur, distanceMax, Time.deltaTime * vitesse);
            
            yield return null;
        }
        while (saveHauteur-hauteur > 0.1)
        {
            hauteur = Mathf.Lerp(hauteur, saveHauteur, Time.deltaTime * vitesse*2);

            yield return null;
        }
        cercle.GetComponent<Cercle>().recherche = true;
        cercle.GetComponent<Cercle>().agent.isStopped = false;
    }

}
