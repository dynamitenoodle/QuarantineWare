using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    // Inspector Fields
    [SerializeField]
    private List<GameObject> people;
    [SerializeField]
    private float minigameTime;
    [SerializeField]
    private float resultTime;
    [SerializeField]
    private int minCount;
    [SerializeField]
    private int maxCount;
    [SerializeField]
    private int peopleLimit;
    [SerializeField]
    private GameObject progressBar;
    [SerializeField]
    private Text textUI;
    [SerializeField]
    private AudioSource correctSound;
    [SerializeField]
    private AudioSource wrongSound;
    
    // Variables
    private int count;

    // Timers
    private float minigameTimer;
    private float resultTimer;

    // Time bar
    private RectTransform progressTransform;
    private Image progressImage;

    private bool passed;

    // Start is called before the first frame update
    void Start()
    {
        // Create random amount of people
        count = Random.Range(minCount, maxCount);
        for (int i = 0; i < count; i++)
        {
            Instantiate(people[Random.Range(0, people.Count)]);
        }

        resultTimer = -1; // Value for inactive
        minigameTimer = minigameTime;

        progressTransform = progressBar.GetComponent<RectTransform>();
        progressImage = progressBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (minigameTimer > 0 && resultTimer == -1)
        {
            minigameTimer -= Time.deltaTime;

            // Draw the progress
            float percentage = minigameTimer / minigameTime;
            progressTransform.localScale = new Vector3(percentage, 1f, 1f); // Scale bar based on percentage

            if (percentage >= 0.5f)
                progressImage.color = new Color(1 - (percentage - 0.5f) * 2, 1, 0); // Switch from green to yellow
            else
                progressImage.color = new Color(1, (percentage * 2), 0); // Switch from yellow to red
        }
        // Ran out of time
        else if (resultTimer == -1)
        {
            textUI.text = $"Attendees: {count}";

            if (count <= peopleLimit)
            {
                // Code for winning
                textUI.color = new Color(0, 1, 0);

                correctSound.Play();

                passed = true;
            }
            else
            {
                // Code for losing
                textUI.color = new Color(1, 0, 0);

                wrongSound.Play();

                passed = false;
            }

            // Start timer
            resultTimer = 0;
        }

        // Check if result timer should start counting
        if (resultTimer != -1)
        {
            resultTimer += Time.deltaTime;

            // Go to next miningame after timer ends
            if (resultTimer >= resultTime)
            {
                if (passed)
                    GameObject.Find("Game Manager").GetComponent<GameManager>().StartRandomMinigame();
                else
                    GameObject.Find("Game Manager").GetComponent<GameManager>().LoadLoseScreen();
            }
        }
    }

    // Called when the player tells to cancel the event
    public void CheckPeople()
    {
        textUI.text = $"Attendees: {count}";

        if (count > peopleLimit)
        {
            // Code for winning
            textUI.color = new Color(0, 1, 0);

            correctSound.Play();

            passed = true;
        }
        else
        {
            // Code for losing
            textUI.color = new Color(1, 0, 0);

            wrongSound.Play();

            passed = false;
        }

        // Start timer
        resultTimer = 0;
    }
}
