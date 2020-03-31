using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    // Inspector Fields
    [SerializeField]
    private List<GameObject> people;
    [SerializeField]
    private int minCount;
    [SerializeField]
    private int maxCount;
    [SerializeField]
    private int peopleLimit;
    
    // The count of people
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        // Create random amount of people
        count = Random.Range(minCount, maxCount);
        for (int i = 0; i < count; i++)
        {
            Instantiate(people[Random.Range(0, people.Count)]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called when the player tells to cancel the event
    public void CheckPeople()
    {
        if (count >= peopleLimit)
        {
            // Code for winning
        }
        else
        {
            // Code for losing
        }

        GameObject.Find("Game Manager").GetComponent<GameManager>().StartRandomMinigame();
    }
}
