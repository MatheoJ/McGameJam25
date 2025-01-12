using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
            GetComponent<Canvas>().enabled = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        else
        {
            Time.timeScale = 1f;
            AudioListener.pause = false;
            GetComponent<Canvas>().enabled = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
