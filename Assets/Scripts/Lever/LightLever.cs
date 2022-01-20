using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLever : MonoBehaviour
{
    public bool b;
    L l;

    private void Start()
    {
        l = GetComponent<L>();
    }

    void Update()
    {
        b = GetComponent<SwitchCondition>().on;
        l.enabled = b;
    }

}
