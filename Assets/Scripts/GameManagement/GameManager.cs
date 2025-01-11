using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Stopwatch stopwatch;
    void Start()
    {
        // Initialization
        stopwatch = FindFirstObjectByType<Stopwatch>();
        stopwatch.StartStopwatch();
    }


    void Update()
    {

    }


    void EndGame()
    {
        stopwatch.StopStopwatch();
    }
}
