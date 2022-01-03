using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCondition : Condition
{
    public bool On { get; private set; }

    protected override void OnFullfilled()
    {
        On = true;
    }

    protected override void OnUnfullfilled()
    {
        On = false;
    }


}
