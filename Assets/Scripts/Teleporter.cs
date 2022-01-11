    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public LightScript otherEnd;
    public bool teleporting;
    // Start is called before the first frame update
    void Start()
    {
        otherEnd.GetComponent<LineRenderer>().enabled = false;
        otherEnd.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (teleporting)
        {
            otherEnd.GetComponent<LineRenderer>().enabled = true;
            otherEnd.enabled = true;
        }
        else
        {
            otherEnd.GetComponent<LineRenderer>().enabled = false;
            otherEnd.enabled = false;
        }
    }
}
