using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinyGoalTrigger : MonoBehaviour
{
    private Goal _goal;
    private ShinyParent _shiny;
    private bool _activated = false;


    // Start is called before the first frame update
    void Start()
    {
        _goal = FindObjectOfType<Goal>();
        _shiny = this.GetComponent<ShinyParent>();
        _goal.AddToTarget(1);
    }

    // Update is called once per frame
    void Update()
    {
        if(_activated != _shiny.activated)
        {
            if (_shiny.activated)
                _goal.AddOneTowardsTarget();
            else
                _goal.RemoveOneTowardsTarget();

            _activated = _shiny.activated;
        }
      
    }
}
