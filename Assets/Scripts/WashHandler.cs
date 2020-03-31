using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashHandler : MonoBehaviour
{
    public List<List<GameObject>> Sections;
    int currentSection;

    // Start is called before the first frame update
    void Start()
    {
        current = 0;
        foreach (Transform child in gameObject.transform.child)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
