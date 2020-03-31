using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private int orientation;
    private float speed;

    private AudioSource audioSource;
    private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        manager = FindObjectOfType<GameManager>();

        speed = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.gameOver)
        {
            if (transform.position.y < -13.0f || transform.position.y > 11.0f)
                Destroy(gameObject);

            transform.Translate(Vector2.down * orientation * speed * Time.deltaTime);
            audioSource.volume = 1.0f / Vector2.Distance(transform.position, FindObjectOfType<PlayerController>().transform.position);
        }
        else
            audioSource.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            manager.GameOverDead();
    }
}
