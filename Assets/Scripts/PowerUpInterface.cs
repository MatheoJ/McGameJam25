using UnityEngine;

public class PowerUpInterface : MonoBehaviour
{
    // Virtual function ExecutePowerUp() that will be overridden by the power up classes
    public virtual void ExecutePowerUp()
    {
        // Create error message if the function is not overridden
        Debug.LogError("ExecutePowerUp() function not implemented");
    }
}
