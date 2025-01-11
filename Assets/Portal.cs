using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public GameObject linkedPortal;
    public bool canTeleport = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canTeleport)
        {
            return;
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            //desactivate linked portal
            //linkedPortal.SetActive(false);
            //teleport player to linked portal
            linkedPortal.GetComponent<Portal>().canTeleport = false;
            other.gameObject.transform.position = linkedPortal.transform.position;
            // get the cameraHolder game object with the tag cameraHolder
            GameObject cameraHolder = GameObject.FindGameObjectWithTag("CameraHolder");
            // apply the rotation of the linked portal to the cameraHolder
            
            
            Quaternion originalCameraRotation = cameraHolder.transform.rotation;
            Quaternion inPortalRotation = transform.rotation;
            Quaternion relativeRotation = originalCameraRotation * Quaternion.Inverse(inPortalRotation);
            
            // Then we multiply the linked portal's rotation by that relative offset
            cameraHolder.transform.rotation = linkedPortal.transform.rotation * relativeRotation;
            
            Debug.Log("Teleporting player to linked portal");
        }
        else
        {
            Debug.Log(other.gameObject.tag);
        }
    }
}
