using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCondition : Condition
{
    //State
    public bool on;

    protected override void OnFullfilled()
    {
        on = true;
    }

    protected override void OnUnfullfilled()
    {
        on = false;
    }

}
