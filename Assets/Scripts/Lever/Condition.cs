using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : MonoBehaviour
{
    [SerializeField] private bool _exact; //if the condition target has to be met exactly or if it only has to be reached (minimum)

    [SerializeField] private int _start = 0;
    [SerializeField] private int _actual;
    [SerializeField] private int _target = 1;
    protected bool fullfilled => _exact ? _actual == _target : _actual >= _target;


    private void Awake()
    {
        InitializeValues();
    }

    virtual protected void InitializeValues(int target = 1, int start = 0)
    {
        _target = target;
        _start = start;
        _actual = start;
    }

    protected abstract void OnFullfilled();
    protected abstract void OnUnfullfilled();

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
        bool wasFullfilled = fullfilled;

        _actual++;

        if (wasFullfilled && !fullfilled)
        {
            OnUnfullfilled();
            return;
        }

        if (fullfilled && !wasFullfilled)
        {
            OnFullfilled();
        }
    }

    public void RemoveOneTowardsTarget()
    {
        bool wasFullfilled = fullfilled;
        
        _actual--;

        if (wasFullfilled && !fullfilled)
        {
            OnUnfullfilled();
            return;
        }

        if (fullfilled && !wasFullfilled)
        {
            OnFullfilled();
        }
    }

}
