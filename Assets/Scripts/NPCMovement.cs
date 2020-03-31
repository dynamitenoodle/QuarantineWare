using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    // Inspector Fields
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Rect bounds;
    [SerializeField]
    private float waitMaxTime;

    // Movement variables
    private Vector3 targetLocation;
    private float waitTime;
    private bool isMoving;

    private const float LOCATION_MARGIN = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Initalization
        isMoving = false;
        waitTime = Random.Range(0, waitMaxTime);

        // Set random position
        transform.position = new Vector3(Random.Range(bounds.min.x, bounds.max.x), transform.position.y, Random.Range(bounds.min.y, bounds.max.y));
    }

    // Update is called once per frame
    void Update()
    {
        // Check if moving to next location
        if (isMoving)
        {
            // Reached location
            if ((transform.position - targetLocation).magnitude <= LOCATION_MARGIN)
            {
                isMoving = false;
                waitTime = Random.Range(0, waitMaxTime);

                // Set animator to stop
                animator.SetBool("Walk Forward", false);
            }
        }
        // Check if waiting is done
        else if (waitTime <= 0)
        {
            // Set to moving state
            isMoving = true;
            waitTime = 0;

            // Determine location to move to
            targetLocation = new Vector3(Random.Range(bounds.min.x, bounds.max.x), transform.position.y, Random.Range(bounds.min.y, bounds.max.y));
            transform.LookAt(targetLocation);
            
            // Set animator to move
            animator.SetBool("Walk Forward", true);
        }
        // Count down the waiting
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
