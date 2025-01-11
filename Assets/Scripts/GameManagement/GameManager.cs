using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private Stopwatch stopwatch;

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
    public Transform puzzleLocation0;
    public Transform puzzleLocation1;
    public Transform puzzleLocation2;
    public Transform puzzleLocation3;
    public Transform puzzleLocation4;
    public Transform puzzleLocation5;
    public Transform puzzleLocation6;
    public Transform puzzleLocation7;
    public Transform puzzleLocation8;
    public Transform puzzleLocation9;

    private List<GameObject> allPuzzles;
    private List<Transform> allPuzzleLocations;

    private int nbPuzzlesInScene = 4;
    private int nbPuzzlesDone = 0;


    void Start()
    {
        // Stopwatch initialization
        stopwatch = FindFirstObjectByType<Stopwatch>();
        stopwatch.StartStopwatch();

        // Puzzles initialization
        allPuzzles = new List<GameObject> {puzzle0, puzzle1, puzzle2, puzzle3, puzzle4,};
        //puzzle4, puzzle5, puzzle6, puzzle7, puzzle8, puzzle9};

        allPuzzleLocations = new List<Transform> {puzzleLocation0, puzzleLocation1, puzzleLocation2, puzzleLocation3,
                         puzzleLocation4, puzzleLocation5, puzzleLocation6, puzzleLocation7, puzzleLocation8, puzzleLocation9};
        InitializeWaldoPuzzles();
    }


    void Update()
    {

    }


    void EndGame()
    {
        stopwatch.StopStopwatch();
    }

    void InitializeWaldoPuzzles()
    {
        // 1. Check if there are enough locations and puzzles
        if (allPuzzles.Count < nbPuzzlesInScene || allPuzzleLocations.Count < nbPuzzlesInScene)
        {
            Debug.LogError("Not enough puzzles or locations to initialize.");
            return;
        }

        // 2. Pick random locations
        List<Transform> availableLocations = new List<Transform>(allPuzzleLocations);
        List<Transform> selectedLocations = new List<Transform>(); // Empty at first

        for (int i = 0; i < nbPuzzlesInScene; i++)
        {
            int randomIndex = Random.Range(0, availableLocations.Count);
            Transform selectedLocation = availableLocations[randomIndex];
            availableLocations.RemoveAt(randomIndex); // Remove the selected location to avoid duplicates
            selectedLocations.Add(selectedLocation); // Add the selected location to the selectedLocations
        }

        // 3. Pick random puzzles and place them at selected locations
        List<GameObject> availablePuzzles = new List<GameObject>(allPuzzles);

        for (int i = 0; i < nbPuzzlesInScene; i++)
        {
            int randomPuzzleIndex = Random.Range(0, availablePuzzles.Count);
            GameObject selectedPuzzle = availablePuzzles[randomPuzzleIndex];
            availablePuzzles.RemoveAt(randomPuzzleIndex); // Remove the selected puzzle to avoid duplicates

            // Place the puzzle at the selected location with the good rotation
            selectedPuzzle.transform.position = selectedLocations[i].position;
            selectedPuzzle.transform.rotation = selectedLocations[i].rotation;
        }
    }
}
