using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByLever : MonoBehaviour
{
    [SerializeField] Transform endPoint;
    [SerializeField] bool lerpMovement;
    [SerializeField] float lerpSpeed = 0.5f;

    bool alreadyMoved;
    void Update()
    {
        if (alreadyMoved)
        {
            return;
        }
        if (GetComponent<SwitchCondition>().on)
        {
            if (lerpMovement)
            {
                transform.position = Vector3.Lerp(transform.position, endPoint.position, lerpSpeed); 
            }
            else
            {
                transform.position = endPoint.position;
                transform.rotation = endPoint.rotation;
            }
            if (transform == endPoint)
            {
                alreadyMoved = true;
            }
        }
    }
}
