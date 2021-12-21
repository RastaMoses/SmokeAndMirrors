using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DebugRay : MonoBehaviour
{
    List<GameObject> shinieObjects;
    Ray2D ray;
    RaycastHit2D rch;
    // Start is called before the first frame update
    void Start()
    {
        shinieObjects = Enumerable.ToList(GameObject.FindGameObjectsWithTag("Shiny"));
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
            if(shinieObjects.Contains(rch.collider.gameObject))
            {
                rch.collider.gameObject.GetComponent<Shinie>().MakeVisible();
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.blue);
            foreach (GameObject shiny in shinieObjects)
            {
                shiny.GetComponent<Shinie>().MakeInvisible();
            }
        }
    }
}
