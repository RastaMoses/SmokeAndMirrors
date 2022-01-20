using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTestBehaviourScript : MonoBehaviour
{
    private Color32 _startColor;
    private SwitchCondition _switchCondition;
    private bool _stateOn;

    // Start is called before the first frame update
    void Start()
    {
        _startColor = GetComponent<SpriteRenderer>().color;
        _switchCondition = GetComponent<SwitchCondition>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_switchCondition.on && !_stateOn)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
            _stateOn = true;
        }
        if (!_switchCondition.on && _stateOn)
        {
            GetComponent<SpriteRenderer>().color = _startColor;
            _stateOn = false;
        }
    }
}
