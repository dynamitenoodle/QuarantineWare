using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int currentSceneIndex;

    // SFX
    [SerializeField]
    private AudioSource cancelSound;

    // Quit Inspector fields
    [SerializeField]
    private GameObject quitObject;
    [SerializeField]
    private GameObject quitProgress;
    [SerializeField]
    private float quitHoldTime;

    // Menu Inspector Fields
    [SerializeField]
    private GameObject menuUI;
    [SerializeField]
    private GameObject creditsUI;

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
        progressTransform = quitProgress.GetComponent<RectTransform>();
        progressImage = quitProgress.GetComponent<Image>();
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
    
    // Call this method to go to the lose screen
    public void LoadLoseScreen()
    {
        // Loads the second scene. Make sure the menu build index is 1
        SceneManager.LoadScene(1);
        currentSceneIndex = 1;
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
        int nextSceneIndex = Random.Range(2, SceneManager.sceneCountInBuildSettings - 1); // Exclude menu and current scene
        
        if (nextSceneIndex >= currentSceneIndex && currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
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

    // Disables and enables credits
    public void ToggleCredits()
    {
        menuUI.SetActive(!menuUI.activeSelf);
        creditsUI.SetActive(!creditsUI.activeSelf);
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
                    quitObject.SetActive(true);
                }
                // Check if the bar is full and return to menu
                else if (currentQuitTime >= quitHoldTime)
                {
                    SceneManager.LoadScene(0);
                    currentSceneIndex = 0;

                    // Reset everything
                    currentQuitTime = 0;
                    quitObject.SetActive(false);

                    cancelSound.Play();

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
                    quitObject.SetActive(false);
                    return; // No need to draw
                }
            }

            // Only draw when active
            if (quitObject.activeSelf)
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
