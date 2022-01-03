using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(LineRenderer))]
public class DebugRay : MonoBehaviour
{
    List<GameObject> shinieObjects;
    Ray2D ray;
    RaycastHit2D rch;
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        shinieObjects = Enumerable.ToList(GameObject.FindGameObjectsWithTag("Shiny"));
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        ray = new Ray2D(transform.position, transform.right);
        rch = Physics2D.Raycast(ray.origin, ray.direction, 10f, 1);

        if (rch)
        {
            lineRenderer.SetPosition(1, ray.direction * rch.distance);
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
            lineRenderer.SetPosition(1, ray.direction * 10f);
            foreach (GameObject shiny in shinieObjects)
            {
                shiny.GetComponent<Shinie>().MakeInvisible();
            }
        }
    }
}
