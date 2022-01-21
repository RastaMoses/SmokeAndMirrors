using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLever : MonoBehaviour
{
    private bool _enabled;
    private L _light;
    private SwitchCondition _switchCondition;
    private void Awake()
    {
        _light = GetComponent<L>();
        _switchCondition = GetComponent<SwitchCondition>();
        _enabled = _switchCondition.on;
        _light.enabled = _enabled;
    }
    private void Start()
    {
    }

    void Update()
    {
        _enabled = _switchCondition.on;
        _light.enabled = _enabled;
    }

}
