using UnityEngine;

public class PowerUpTest : PowerUpInterface
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
        Debug.Log("Power up test executed");
    }
}
