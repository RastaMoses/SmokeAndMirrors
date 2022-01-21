using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvr : MonoBehaviour
{
    public bool b;
    public L l;

    void Update()
    {
        l.enabled = b;

        if (Vector2.Distance(GameObject.Find("Player").transform.position, transform.position) < 1f)
        {
            if (Input.GetButtonDown("Interact"))
            {
                b = !b;
            }
        }
    }

}
