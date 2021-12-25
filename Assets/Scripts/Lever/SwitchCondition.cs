using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCondition : Condition
{
    public bool On;

    public SwitchCondition(int target, int start) : base (target, start)
    {

    }

    protected override void OnFullfilled()
    {
        On = true;
    }

    protected override void OnUnfullfilled()
    {
        On = false;
    }


}
