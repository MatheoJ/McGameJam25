using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Stopwatch stopwatch;

    // Where is Waldo Puzzles

    

    [Header("Waldo puzzles")]
    public GameObject puzzle0;
    public GameObject puzzle1;
    public GameObject puzzle2;
    public GameObject puzzle3;
    public GameObject puzzle4;
    public GameObject puzzle5;
    public GameObject puzzle6;
    public GameObject puzzle7;
    public GameObject puzzle8;
    public GameObject puzzle9;

    [Header("Waldo puzzle locations")]
    public GameObject puzzleLocation0;
    public GameObject puzzleLocation1;
    public GameObject puzzleLocation2;
    public GameObject puzzleLocation3;
    public GameObject puzzleLocation4;
    public GameObject puzzleLocation5;
    public GameObject puzzleLocation6;
    public GameObject puzzleLocation7;
    public GameObject puzzleLocation8;
    public GameObject puzzleLocation9;

    private GameObject[] allPuzzles;
    private GameObject[] allPuzzleLocations;

    private int nbPuzzleInScene = 4;
    private int nbPuzzleDone = 0;


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
