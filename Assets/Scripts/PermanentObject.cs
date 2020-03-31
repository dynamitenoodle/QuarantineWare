using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Put this script on any objects that you want to stay across scenes.
/// </summary>
public class PermanentObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Make sure this stays across all scenes
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
