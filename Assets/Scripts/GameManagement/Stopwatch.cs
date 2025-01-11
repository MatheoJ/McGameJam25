using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    public TextMeshProUGUI stopwatchText;
    private float elapsedTime = 0f;
    private bool isRunning = false;

    void Start()
    {
        UpdateStopwatchText(); // Initialization
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime; // += Time it took to complete the last frame (sec)
            UpdateStopwatchText();
        }
    }

    public void StartStopwatch()
    {
        isRunning = true;
    }

    public void StopStopwatch()
    {
        isRunning = false;
    }

    public void ResetStopwatch()
    {
        elapsedTime = 0f;
        UpdateStopwatchText();
    }

    private void UpdateStopwatchText()
    {
        // Format the elapsed time as minutes:seconds:milliseconds
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);

        stopwatchText.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }
}
