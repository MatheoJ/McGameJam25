using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Stopwatch stopwatch;

    [Header("Waldo Puzzles")]
    public Transform puzzlesParent;
    public Transform puzzleLocationsParent;

    [Header("NoteBook")]
    public TextMeshProUGUI puzzleText;
    public TextMeshProUGUI findExitText;
    public Image findExitBlackCheckbox;
    public Image findExitGreyCheckbox;
    public Image puzzleTick;
    public Image findExitTick;
    public Color greyColor = Color.grey;


    private List<GameObject> allPuzzles;
    private List<Transform> allPuzzleLocations;

    public GameObject exitLadder;

    private int nbPuzzlesInScene = 4;
    private int nbPuzzlesCompleted = 0;

    private int nbBonusFound; // can be cool for the end stats (end menu)


    void Start()
    {
        exitLadder.SetActive(false);
        
        // Stopwatch initialization
        stopwatch = FindFirstObjectByType<Stopwatch>();
        stopwatch.StartStopwatch();

        // Notebook initialization
        UpdatePuzzleText();
        findExitText.color = greyColor;
        puzzleTick.gameObject.SetActive(false);
        findExitTick.gameObject.SetActive(false);
        findExitBlackCheckbox.gameObject.SetActive(false);


        // Puzzles initialization
          // Puzzles
        allPuzzles = new List<GameObject>();
        //Debug.Log("puzzles : ");
        foreach (Transform child in puzzlesParent)
        {
            //Debug.Log(child);
            allPuzzles.Add(child.gameObject);
        }

          // Puzzle locations
        allPuzzleLocations = new List<Transform>();
        //Debug.Log("puzzle locations : ");
        foreach (Transform child in puzzleLocationsParent)
        {
            //Debug.Log(child);
            allPuzzleLocations.Add(child);
        }

        ////Debug.Log("allpuzzles : ");
        ////foreach (var child in allPuzzles) {
        ////    Debug.Log(child.name);
        ////}


            InitializeWaldoPuzzles();
            
            //Get all audio listener in the scene
            
            //Log there owner
  
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



    //// PUZZLES ////

    private void UpdatePuzzleText()
    {
        puzzleText.text = ":  " + nbPuzzlesCompleted + " / " + nbPuzzlesInScene;
    }
    public void puzzleCompleted()
    {
        if (nbPuzzlesCompleted < nbPuzzlesInScene)
        {
            nbPuzzlesCompleted++;
            UpdatePuzzleText();
        }

        //If all puzzles are completed
        if (nbPuzzlesCompleted == nbPuzzlesInScene)
        {
            puzzleTick.gameObject.SetActive(true);

            findExitGreyCheckbox.gameObject.SetActive(false);
            findExitBlackCheckbox.gameObject.SetActive(true);
            findExitText.color = Color.black;
            
            exitLadder.SetActive(true);

            // Enable/open Exit door     /!\ TO DO
        }
    }

    public void ExitCompleted()
    {
        findExitTick.gameObject.SetActive(true);
    }



    //// BONUS ////
    public void IncrementNbBonusFound() // delete this comment when function used
    {
        nbBonusFound++;
    }
}
