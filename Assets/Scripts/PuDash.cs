using UnityEngine;

public class PowerUpDash : PowerUpInterface
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Implement virtual function ExecutePowerUp()
    public override void ExecutePowerUp()
    {
        //Get the player object with tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Get PlayerMovement component from the player object
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        //Call the Dash function from the PlayerMovement component
        playerMovement.StartCoroutine(playerMovement.Dash());
    }
}
