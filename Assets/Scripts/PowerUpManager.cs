using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PowerUpInterface currentPowerUp;
    private int currentPowerUpIndex = 0;
    public List<PowerUpInterface> powerUpInTheGame;
    
    public Transform cameraTransform;
    void Start()
    {
        currentPowerUpIndex = -1;
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
        }
        else
        {
            Debug.Log("No power up to execute");
        }
    }
}
