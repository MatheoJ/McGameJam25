using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int currentPowerUpIndex;
    public List<PowerUpInterface> powerUpInTheGame;
    
    //Image component in canvas to display power up
    public UnityEngine.UI.Image powerUpImage;
    public Sprite defaultPowerUpImage;
    
    
    public Transform cameraTransform;
    void Start()
    {
        currentPowerUpIndex = -1;
        powerUpImage.sprite = defaultPowerUpImage;
    }

    // Update is called once per frame
    void Update()
    {
        //if Q is pressed, execute power up
        if (Input.GetKeyDown(KeyCode.Q))
        {
            tryExecutePowerUp();
        }
        
        //if E is pressed, change power up
        if (Input.GetKeyDown(KeyCode.E))
        {
            tryGetPowerUp();
        }
    }
    
    void tryGetPowerUp()
    {
        // Do a raycast to check if the player is looking at a power up
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 10))
        {
            
            if (hit.transform.tag == "ObjectForPowerUp")
            {
                Debug.Log("Power up obtained");
                Destroy(hit.transform.gameObject);
                currentPowerUpIndex = Random.Range(0, powerUpInTheGame.Count);
                powerUpImage.sprite = powerUpInTheGame[currentPowerUpIndex].powerUpImage;
            }
            else
            {
                Debug.Log("Not a power up");
            }
        }
    }
    
    void tryExecutePowerUp()
    {
        if (currentPowerUpIndex != -1)
        {
            powerUpInTheGame[currentPowerUpIndex].ExecutePowerUp();
            currentPowerUpIndex = -1;
            powerUpImage.sprite = defaultPowerUpImage;
        }
        else
        {
            Debug.Log("No power up to execute");
        }
    }
}
