using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByLever : MonoBehaviour
{
    //Serialize Params
    [SerializeField] Transform endPoint;
    [SerializeField] bool lerpMovement;
    [SerializeField] float lerpSpeed = 0.5f;
    // [SerializeField] float time = 2f;

    //State
    Vector3 originalTransform;
    Vector3 goalTransform;
    bool canMove = true;
    private void Start()
    {
        originalTransform = transform.position;
        
    }
    void Update()
    {
        if (GetComponent<SwitchCondition>().on)
        {
            goalTransform = endPoint.position;
        }
        if (!GetComponent<SwitchCondition>().on)
        {
            goalTransform = originalTransform;
        }
        
        if (canMove)
        {
            if (lerpMovement)
            {
                transform.position = Vector3.Lerp(transform.position, goalTransform, lerpSpeed * Time.deltaTime); 
            }
            else
            {
                transform.position = goalTransform;
            }
            
        }
    }

    public void MoveToOriginal()
    {
        goalTransform = endPoint.position;
        canMove = true;
    }

    

    

}
