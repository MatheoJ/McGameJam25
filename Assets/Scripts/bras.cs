using System.Collections;
using UnityEngine;

public class bras : MonoBehaviour
{
    public int accessiblePuzzles=0;
    //public MeshRenderer brasMesh;
    public GameObject brasPrefab;
    private Vector3 startingPos;
    public float pointingSpeed = 5;
    private bool ongoing = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPos = brasPrefab.transform.localPosition;
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.renderQueue = 4000; // Overlay queue
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!ongoing)
            {
                ongoing = true;
                StartCoroutine(Point());
            }
        }
    }

    IEnumerator Point()
    {
        //float z = brasPrefab.transform.position.z;
        while (startingPos.z+0.5 - brasPrefab.transform.localPosition.z   > 0.05)
        {
            brasPrefab.transform.localPosition = Vector3.Lerp(brasPrefab.transform.localPosition, startingPos + Vector3.forward*.5f, Time.deltaTime * pointingSpeed);
            yield return null;
        }
        //Debug.Log("avant fini");
        while (brasPrefab.transform.localPosition.z - startingPos.z > 0.05)
        {
            brasPrefab.transform.localPosition = Vector3.Lerp(brasPrefab.transform.localPosition, startingPos, Time.deltaTime * pointingSpeed);
            yield return null;
        }
        ongoing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puzzle"))
        {
            //brasMesh.enabled = true;
            brasPrefab.SetActive(true);
            accessiblePuzzles++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Puzzle"))
        {
            
            accessiblePuzzles--;
            if(accessiblePuzzles < 0) accessiblePuzzles = 0;
            if (accessiblePuzzles == 0)
            {
                //brasMesh.enabled = false;
                brasPrefab.SetActive(false);
            }
        }
    }
}
