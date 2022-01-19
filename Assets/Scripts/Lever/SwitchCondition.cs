using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCondition : Condition
{
    public bool On { get; private set; }

    protected override void InitializeValues(int target = 1, int start = 0)
    {
        base.InitializeValues(0, start);
        //can be removed if only 1 "on" is needed to turn a switachable on
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
