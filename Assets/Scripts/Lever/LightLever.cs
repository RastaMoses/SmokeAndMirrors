using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLever : MonoBehaviour
{
    public bool b;
    L l;
    private SwitchCondition _switchCondition;

    private void Start()
    {
        l = GetComponent<L>();
        _switchCondition = GetComponent<SwitchCondition>();
    }

    void Update()
    {
        b = _switchCondition.on;
        l.enabled = b;
    }

}
