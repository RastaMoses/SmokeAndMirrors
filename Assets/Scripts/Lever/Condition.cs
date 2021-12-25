using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : MonoBehaviour
{
    private int _start = 0;
    private int _actual;
    private int _target = 1;
    protected bool fullfilled => _actual == _target;

    public Condition(int target, int start = 0)
    {
        _target = target;
        _start = start;
        _actual = start;
    }

    public void Reset()
    {
        _actual = _start;
    }

    public void AddToTarget(int i)
    {
        _target += i;
    }

    public void RemoveFromTarget(int i)
    {
        _target -= i;
    }

    public void AddOneTowardsTarget()
    {
        bool wasFullfilled = false;
        if (fullfilled)
            wasFullfilled = true;

        _actual++;

        if (wasFullfilled)
        {
            OnUnfullfilled();
            return;
        }

        if (fullfilled)
        {
            OnFullfilled();
        }
    }

    protected abstract void OnFullfilled();
    protected abstract void OnUnfullfilled();

    public void RemoveOneTowardsTarget()
    {
        bool wasFullfilled = false;
        if (fullfilled)
            wasFullfilled = true;
        
        _actual--;

        if (wasFullfilled)
        {
            OnUnfullfilled();
            return;
        }

        if (fullfilled)
        {
            OnFullfilled();
        }
    }

}
