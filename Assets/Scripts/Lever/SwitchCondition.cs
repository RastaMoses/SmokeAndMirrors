using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCondition : Condition
{
    //State

    public bool on;

    //Whyyyyy????!
    /*
    protected override void InitializeValues(int target = 1, int start = 0)
    {
        base.InitializeValues(0, start);
        //can be removed if only 1 "on" is needed to turn a switachable on
    }

    */

    protected override void OnFullfilled()
    {
        on = true;
    }

    protected override void OnUnfullfilled()
    {
        on = false;
    }

}
