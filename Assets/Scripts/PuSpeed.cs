using UnityEngine;

public class PowerUpSpeed : PowerUpInterface
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speedMultiplier = 2.7f;
    public float duration = 5.0f;
    
    public AudioSource powerUpSound;
 
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
        playerMovement.StartCoroutine(playerMovement.ScaleSpeedForDuration(duration,speedMultiplier));
        
        
        powerUpSound.Play();
        if (powerUpSound != null)
        {
            
        }
    }
}
