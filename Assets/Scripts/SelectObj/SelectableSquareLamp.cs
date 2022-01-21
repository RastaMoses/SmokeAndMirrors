using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableSquareLamp : SelectableObj
{
    private L _light;
    private string _swapButton = "A";

    protected override void InitializeOnStart()
    {
        _light = GetComponent<L>();
    }

    public override void ProcessInput()
    {
        if (Input.GetButtonDown(_swapButton))
        {
            _light.AddToSwap();
        }
    }
  

    void OnMouseDown()
    {
        _light.AddToSwap();
    }
}
