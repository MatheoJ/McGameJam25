using UnityEngine;

public class overlapAll : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.renderQueue = 4000; // Overlay queue
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
