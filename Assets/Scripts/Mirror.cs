using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public bool reflecting;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<LineRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (reflecting)
        {
            GetComponent<LineRenderer>().enabled = true;
            GetComponent<LightScript>().enabled = true;
        }
        else
        {
            GetComponent<LineRenderer>().enabled = false;
            GetComponent<LightScript>().enabled = false;
        }
    }
}
