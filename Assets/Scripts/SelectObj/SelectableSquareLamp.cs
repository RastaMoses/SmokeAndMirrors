using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableSquareLamp : SelectableObj
{
    private NL _light;
    private string _swapButton = "A";
    private string _cancelButton = "B";

    protected override void InitializeOnStart()
    {
        _light = GetComponent<NL>();
    }

    public override void ProcessInput()
    {
        if (Input.GetButtonDown(_swapButton))
        {
            if (_light.enabled)
                FindObjectOfType<NLC>().ATS(_light);
        }
    }

    void Update()
    {
        if(Input.GetButtonDown(_cancelButton))
        {
            FindObjectOfType<NLC>().CSW();
        }
    }

}
