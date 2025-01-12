using UnityEngine;

public class PowerUpPortal : PowerUpInterface
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
        GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");
        foreach (GameObject portal in portals)
        {
            portal.GetComponent<Portal>().SetCanTeleport(true);
        }
        
        if (powerUpSound != null)
        {
            powerUpSound.Play();
        }
    }
}