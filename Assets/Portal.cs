using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public GameObject linkedPortal;
    public bool canTeleport = false;
    
    //audio source
    public AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCanTeleport(bool canTeleport)
    {
        this.canTeleport = canTeleport;
    }
    
    private void DisableAllPortals()
    {
        GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");
        foreach (GameObject portal in portals)
        {
            portal.GetComponent<Portal>().SetCanTeleport(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canTeleport)
        {
            return;
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            
            GameObject cameraHolder = GameObject.FindGameObjectWithTag("CameraHolder");
            BlinkEffect blink = cameraHolder.GetComponentInChildren<BlinkEffect>();
            // If BlinkEffect is not a child of cameraHolder, just find it differently
            // e.g. blink = FindObjectOfType<BlinkEffect>();

            // Trigger the blink
            if (blink != null)
            {
                blink.StartBlink();
            }
            
            audioSource.Play();
            
            //desactivate linked portal
            //linkedPortal.SetActive(false);
            //teleport player to linked portal
            linkedPortal.GetComponent<Portal>().SetCanTeleport(false);
            other.gameObject.transform.position = linkedPortal.transform.position;
            // get the cameraHolder game object with the tag cameraHolder
            
            // apply the rotation of the linked portal to the cameraHolder
            
            
            CameraRotation camRotScript = cameraHolder.GetComponent<CameraRotation>();

            // 1) We get the *desired* new rotation (the same math you had before):
            Quaternion originalCameraRotation = cameraHolder.transform.rotation;
            Quaternion inPortalRotation = transform.rotation;

            // Often this order is best:
            Quaternion relativeRotation = Quaternion.Inverse(inPortalRotation) * originalCameraRotation;
            Quaternion newRotation = linkedPortal.transform.rotation * relativeRotation;

            // 2) Convert that to Euler angles
            Vector3 euler = newRotation.eulerAngles;
            
            // 3) Apply the new rotation
            camRotScript.yRotation = euler.y - 180;
            
            // Update the player velocity of the rigidbody to match the linked portal
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.linearVelocity = linkedPortal.transform.TransformVector(
                transform.InverseTransformVector(rb.linearVelocity)
            );
            Debug.Log("Teleporting player to linked portal");
            
            
            DisableAllPortals();
            

        }
        else
        {
            Debug.Log(other.gameObject.tag);
        }
    }
}
