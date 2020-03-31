using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Image staminaBar;
    private GameManager manager;
    private float speed;
    private float maxStamina;
    private float stamina;
    private bool tired;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();

        speed = 0.5f;
        maxStamina = 30.0f;
        stamina = maxStamina;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.gameOver)
        {
            if (transform.position.y > -5.0f && transform.position.y < 3.8f)
            {
                Vector3 cameraPos = Camera.main.transform.position;
                cameraPos.y = transform.position.y;
                Camera.main.transform.position = cameraPos;
            }

            staminaBar.fillAmount = stamina / maxStamina;

            if (Input.GetKey(KeyCode.LeftShift) && !tired)
            {
                speed = 2.0f;
                GetComponent<AudioSource>().pitch = 2.0f;
                if (stamina > 0.0f)
                    stamina -= 0.4f;
                else
                    tired = true;
            }
            else
            {
                speed = 1.0f;
                GetComponent<AudioSource>().pitch = 1.0f;
                if (stamina < maxStamina)
                    stamina += 0.2f;
                else
                    tired = false;
            }

            Vector2 tarPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (tarPos.x < -5.7f)
                tarPos.x = -5.7f;
            else if (tarPos.x > 6.7f)
                tarPos.x = 6.7f;
            transform.position = Vector2.MoveTowards(transform.position, tarPos, speed * Time.deltaTime);

            if (transform.position.y > 8.6)
                manager.GameOverWin();
        }
        else
        {
            GetComponent<AudioSource>().enabled = false;
        }
    }
}
