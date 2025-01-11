using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    
    public float sensX;
    public float sensY;
    
    public Transform orientation;
    
    public float xRotation;
    public float yRotation;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            // Lancer le rayon
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5f))
            {
                if(hit.transform.gameObject.name == "waldoHitbox")
                {
                    hit.transform.gameObject.GetComponent<onHit>().Hit();
                }
            }
        }
    }

}
