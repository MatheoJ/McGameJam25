using UnityEngine;

public class PowerUpHandBait : PowerUpInterface
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource powerUpSound;
  

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Implement virtual function ExecutePowerUp()
    public override void ExecutePowerUp()
    {
        
        //Get Cercle object with tag "Cercle"
        GameObject cercle = GameObject.FindGameObjectWithTag("Cercle");
        //Get the cercle script from the cercle object
        Cercle cercleScript = cercle.GetComponent<Cercle>();
        
        cercleScript.baitHand();
        
        if (powerUpSound != null)
        {
            powerUpSound.Play();
        }
    }
}