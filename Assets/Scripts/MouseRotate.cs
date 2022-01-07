using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    bool rotating;
    public float degreesPerSec = 360f;
    // Start is called before the first frame update
    void Start()
    {
        rotating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotating)
        {
            float rotAmount = degreesPerSec * Time.deltaTime;
            float curRot = transform.localRotation.eulerAngles.z;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot + rotAmount));
        }
    }

    void OnMouseDown()
    {
        rotating = true;
    }

    void OnMouseUp()
    {
        rotating = false;
    }
}
