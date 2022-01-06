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
        otherEnd.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (teleporting)
        {
            otherEnd.enabled = true;
        }
        else
        {
            otherEnd.enabled = false;
        }
    }
}
