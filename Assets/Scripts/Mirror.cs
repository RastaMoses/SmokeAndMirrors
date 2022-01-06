using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public bool reflecting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (reflecting)
        {
            GetComponent<LightScript>().enabled = true;
        }
        else
        {
            GetComponent<LightScript>().enabled = false;
        }
    }
}
