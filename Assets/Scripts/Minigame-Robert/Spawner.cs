using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject NPCPrefab;
    [SerializeField]
    private float spawnRate, timerRange, distanceRange;
    private MiniGameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<MiniGameManager>();

        Invoke("SpawnNPC", spawnRate + Random.Range(0.0f, timerRange));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnNPC()
    {
        if (!manager.gameOver)
        {
            Vector3 pos = transform.position;
            pos.x += Random.Range(-distanceRange, distanceRange);

            Instantiate(NPCPrefab, pos, Quaternion.identity);

            Invoke("SpawnNPC", spawnRate + Random.Range(0.0f, timerRange));
        }
    }
}
