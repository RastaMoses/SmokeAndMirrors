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
            GetComponent<MirrorScript>().enabled = true;
            GetComponent<LineRenderer>().enabled = true;
        }
        else
        {
            GetComponent<MirrorScript>().enabled = false;
            GetComponent<LineRenderer>().enabled = false;
        }
    }
}
