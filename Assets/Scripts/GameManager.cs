using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int currentSceneIndex;

    // Quit Inspector fields
    [SerializeField]
    private GameObject QuitObject;
    [SerializeField]
    private GameObject QuitProgress;
    [SerializeField]
    private float quitHoldTime;

    // Quit variables
    private RectTransform progressTransform;
    private Image progressImage;
    private float currentQuitTime;

    // Start is called before the first frame update
    void Start()
    {
        // Initialization
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        currentQuitTime = 0;
        progressTransform = QuitProgress.GetComponent<RectTransform>();
        progressImage = QuitProgress.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        QuitInput();
    }

    #region Public Methods
    // Call this method to return back to the menu screen
    public void LoadMainMenu()
    {
        // Loads the first scene. Make sure the menu build index is 0
        SceneManager.LoadScene(0);
        currentSceneIndex = 0;
    }

    // Call this method when the current minigame is over
    public void StartNextMinigame()
    {
        // Make sure it's not the last scene
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            // Load the next scene in the build index
            SceneManager.LoadScene(currentSceneIndex + 1);
            currentSceneIndex++;
        }
    }

    // Call this method when the current minigame is over
    public void StartRandomMinigame()
    {
        int nextSceneIndex = Random.Range(1, SceneManager.sceneCountInBuildSettings - 1); // Exclude menu and current scene
        
        // Make sure it doesn't load the same minigame
        if (nextSceneIndex >= currentSceneIndex)
            nextSceneIndex++;

        // Load the selected scene in the build index
        SceneManager.LoadScene(nextSceneIndex);
        currentSceneIndex = nextSceneIndex;
    }

    // Call this to just exit the game
    public void QuitApplication()
    {
        Application.Quit();
    }
    #endregion

    #region Private Methods
    private void QuitInput()
    {
        // Check if it's not the menu already
        if (currentSceneIndex != 0)
        {
            // Check if cancel is being held down
            if (Input.GetButton("Cancel"))
            {
                // Check if pressing for first time
                if (currentQuitTime == 0)
                {
                    QuitObject.SetActive(true);
                }
                // Check if the bar is full and return to menu
                else if (currentQuitTime >= quitHoldTime)
                {
                    SceneManager.LoadScene(0);
                    currentSceneIndex = 0;

                    // Reset everything
                    currentQuitTime = 0;
                    QuitObject.SetActive(false);
                    return; // No need to draw
                }

                // Increase bar while held down
                currentQuitTime += Time.deltaTime;

                // Don't let it surpass the maximum time
                if (currentQuitTime > quitHoldTime)
                    currentQuitTime = quitHoldTime;
            }
            // Decrease bar over time
            else if (currentQuitTime > 0)
            {
                currentQuitTime -= Time.deltaTime * 2; // Decrease twice as fast

                // Set back to inactive if hits 0
                if (currentQuitTime <= 0)
                {
                    currentQuitTime = 0;
                    QuitObject.SetActive(false);
                    return; // No need to draw
                }
            }

            // Only draw when active
            if (QuitObject.activeSelf)
            {
                // Draw the progress
                float percentage = currentQuitTime / quitHoldTime;
                progressTransform.localScale = new Vector3(percentage, 1f, 1f); // Scale bar based on percentage

                if (percentage <= 0.5f)
                    progressImage.color = new Color(percentage * 2, 1, 0); // Switch from green to yellow
                else
                    progressImage.color = new Color(1, 1 - (percentage - 0.5f) * 2, 0); // Switch from yellow to red
            }
        }
    }
    #endregion
}
