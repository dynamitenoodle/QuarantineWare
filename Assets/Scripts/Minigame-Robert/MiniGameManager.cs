using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] playerSpawns;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Text infectedText, gameOverText;
    private int infectedCount;
    public bool gameOver;

    [SerializeField]
    private AudioClip[] coughs;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        infectedCount = 0;
        infectedText.enabled = false;

        int index = Random.Range(0, playerSpawns.Length);

        Instantiate(playerPrefab, playerSpawns[index].position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddInfected()
    {
        infectedCount++;
        infectedText.text = "Number Infected: " + infectedCount;

        int index = Random.Range(0, coughs.Length);

        audioSource.PlayOneShot(coughs[index]);
    }

    public void GameOverDead()
    {
        gameOver = true;
        gameOverText.text = "You died and infected " + infectedCount + " people!";
        gameOverText.enabled = true;
        StartCoroutine(LoadNextScene());
    }

    public void GameOverWin()
    {
        gameOver = true;
        gameOverText.text = "You made it back and infected " + infectedCount + " people!";
        gameOverText.enabled = true;
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        while(true)
        {
            yield return new WaitForSeconds(3.0f);
            //Load the next game/scene
            if (gameOver)
            {
                GameObject.Find("Game Manager").GetComponent<GameManager>().LoadLoseScreen();
            }
            else
            {
                GameObject.Find("Game Manager").GetComponent<GameManager>().StartRandomMinigame();
            }
        }
    }
}
