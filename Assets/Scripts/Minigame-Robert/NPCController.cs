using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    [SerializeField]
    private Image contagionBar;
    [SerializeField]
    private float maxContagion;
    private float contagion;
    private GameManager manager;
    private float speed;
    private Vector2 direction;
    [SerializeField]
    private int orientation;

    public bool inDanger;
    public bool infected;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();

        maxContagion = 30.0f;
        contagion = 0.0f;
        inDanger = false;
        infected = false;

        speed = 0.25f + Random.Range(0.0f, 1.25f);
        direction = Vector2.down;

        InvokeRepeating("ChangePath", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.gameOver)
        {
            transform.Translate(direction * orientation * speed * Time.deltaTime);

            contagionBar.fillAmount = contagion / maxContagion;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2.75f);

            inDanger = false;
            float distanceToDanger = Mathf.Infinity;

            foreach (Collider2D collider in colliders)
            {
                float distanceToCollider = Vector2.Distance(transform.position, collider.transform.position);

                if (collider.tag == "Player")
                {
                    inDanger = true;
                    if (distanceToCollider < distanceToDanger)
                        distanceToDanger = distanceToCollider;
                }
                    
                else if (collider.tag == "NPC" && collider.GetComponent<NPCController>().infected)
                {
                    inDanger = true;
                    if (distanceToCollider < distanceToDanger)
                        distanceToDanger = distanceToCollider;
                }
            }

            if (inDanger && !infected)
            {
                Debug.Log(distanceToDanger);
                contagion += 1.0f / (distanceToDanger * 2.0f);
            } 
            else if (!inDanger && !infected)
                contagion -= 0.1f;

            if (contagion >= maxContagion && !infected)
            {
                infected = true;
                speed = speed / 2.0f;
                GetComponent<SpriteRenderer>().color = Color.green;
                manager.AddInfected();
            }

            if (transform.position.y < -11.0f || transform.position.y > 9.0f)
                Destroy(gameObject);
        }
        else
        {
            GetComponent<AudioSource>().enabled = false;
        }
    }

    private void ChangePath()
    {
        direction.x = Random.Range(-0.1f, 0.1f);
    }
}
