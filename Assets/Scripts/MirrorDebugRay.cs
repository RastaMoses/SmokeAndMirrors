using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MirrorDebugRay : MonoBehaviour
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

    }

    public void MakeRay(Vector2 origin, Vector2 direction)
    {
        ray = new Ray2D(origin, direction);
        rch = Physics2D.Raycast(ray.origin, ray.direction, 50f);
        if (rch)
        {
            Debug.DrawRay(ray.origin, ray.direction * rch.distance, Color.black);
            if(rch.collider.tag == "Mirror")
            {
                rch.collider.gameObject.GetComponent<MirrorDebugRay>().MakeRay(rch.point, Vector2.Reflect(rch.point-ray.origin,rch.normal));
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);
        }
    }
}
