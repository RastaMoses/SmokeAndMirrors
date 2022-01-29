using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLever : MonoBehaviour
{
    public GameObject platform;
    public Transform endOne;
    public Transform endTwo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (Input.GetKey(KeyCode.K))
            {
                if (Vector2.Distance(platform.transform.position, endTwo.position) > 0.001f)
                {
                    platform.transform.position = Vector3.Lerp(platform.transform.position, endTwo.position, 0.02f);
                }
            }

            if (Input.GetKey(KeyCode.J))
            {
                if (Vector2.Distance(platform.transform.position, endOne.position) > 0.001f)
                {
                    platform.transform.position = Vector3.Lerp(platform.transform.position, endOne.position, 0.02f);
                }
            }
        }
    }
}
