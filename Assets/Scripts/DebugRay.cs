using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugRay : MonoBehaviour
{
    Ray2D ray;
    RaycastHit2D rch;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray2D(transform.position, transform.right);
        rch = Physics2D.Raycast(ray.origin, ray.direction, 10f, 1);

        if (rch)
        {
            Debug.DrawRay(ray.origin, ray.direction * rch.distance, Color.green);
            if (rch.collider.tag == "Mirror")
            {
                rch.collider.gameObject.GetComponent<MirrorDebugRay>().MakeRay(rch.point, Vector2.Reflect(rch.point, rch.normal));
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.blue);
        }
    }
}
