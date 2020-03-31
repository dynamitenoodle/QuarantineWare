using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashHandler : MonoBehaviour
{
    public List<GameObject> Sections;
    int currentSection;

    // Start is called before the first frame update
    void Start()
    {
        currentSection = 0;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
