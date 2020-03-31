using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashHandler : MonoBehaviour
{
    List<GameObject> sections;
    List<GameObject> currentSection;

    public float sectionTimerMax;
    float sectionTimer;
    float timerScale;
    GameObject timer;

    GameObject handWash;
    public List<Sprite> handWashSprites;
    int currentSprite;

    // Start is called before the first frame update
    void Start()
    {
        sections = new List<GameObject>();
        currentSection = new List<GameObject>();

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeSelf)
                gameObject.transform.GetChild(i).gameObject.SetActive(false);

            sections.Add(gameObject.transform.GetChild(i).gameObject);
        }

        SetSection();

        sectionTimer = sectionTimerMax;
        timer = GameObject.Find("Timer");
        timerScale = timer.transform.localScale.x;

        handWash = GameObject.Find("HandSprite");
        currentSprite = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckClick();

        if (currentSection.Count == 0)
        {
            // Destroy the current section
            GameObject temp = sections[0];
            sections.RemoveAt(0);
            Destroy(temp);

            //reset the timer
            sectionTimer = sectionTimerMax;

            SetSection();

            currentSprite++;
            handWash.GetComponent<SpriteRenderer>().sprite = handWashSprites[currentSprite];
        }

        UpdateTimer();
    }

    // Checks to see if the player has clicked a circle, and Destroys the currentcircle if it has
    void CheckClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            if (currentSection[0].GetComponent<Collider2D>().bounds.Contains(mousePos))
            {
                GameObject temp = currentSection[0];
                currentSection.RemoveAt(0);
                Destroy(temp);
            }
        }
    }

    // Updates the in game timer
    void UpdateTimer()
    {
        sectionTimer -= Time.deltaTime;

        timer.transform.localScale = new Vector3(sectionTimer * (timerScale / sectionTimerMax), timer.transform.localScale.y, timer.transform.localScale.z);

        if (sectionTimer <= 0)
        {
            // End the minigame
        }
    }

    // Sets the current section to the number inputted
    void SetSection()
    {
        if (!sections[0].activeSelf)
            sections[0].SetActive(true);

        for (int i = 0; i < sections[0].transform.childCount; i++)
        {
            currentSection.Add(sections[0].transform.GetChild(i).gameObject);
        }
    }
}
