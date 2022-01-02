using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class LightScript : MonoBehaviour
{
    RaycastHit2D rch;
    [SerializeField] float lightRange;
    [SerializeField] LayerMask rayFilter;
    LineRenderer lr;
    EdgeCollider2D ec;
    ContactFilter2D cf;
    List<Collider2D> results;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        ec = GetComponent<EdgeCollider2D>();
        results = new List<Collider2D>();
        ec.SetPoints(new List<Vector2> { new Vector2(0, 0), new Vector2(lightRange, 0) });
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, transform.position);

        GameObject hit = HitObject();
        if (hit)
        {
            print(hit);
        }






    }

    Vector2 FindOverlap(EdgeCollider2D thisEdge, EdgeCollider2D thatEdge)
    {
        Vector2 a = thisEdge.points[0] + (Vector2)thisEdge.transform.position;
        Vector2 b = thisEdge.transform.position + thisEdge.transform.right * thisEdge.points[1].x;

        Vector2 c = thatEdge.points[0] + (Vector2)thatEdge.transform.position;
        Vector2 d = thatEdge.transform.position + thatEdge.transform.right * thisEdge.points[1].x;

        float a1 = b.y - a.y;
        float b1 = a.x - b.x;
        float c1 = a1 * a.x + b1 * a.y;

        float a2 = d.y - c.y;
        float b2 = c.x - d.x;
        float c2 = a2 * c.x + b2 * c.y;

        float det = a1 * b2 - a2 * b1;

        if (det == 0)
        {
            return Vector2.zero;
        }
        else
        {
            float x = (b2 * c1 - b1 * c2) / det;
            float y = (a1 * c2 - a2 * c1) / det;
            return new Vector2(x, y);
        }
    }

    EdgeCollider2D DetectEdgeCollider(List<Collider2D> results)
    {
        foreach (Collider2D col in results)
        {
            if (col.GetType() == typeof(EdgeCollider2D))
            {
                return (EdgeCollider2D)col;
            }
        }
        return null;
    }

    GameObject HitObject()
    {
        ec.OverlapCollider(cf, results);
        EdgeCollider2D overlapCollider = DetectEdgeCollider(results);
        if (overlapCollider)
        {

            Vector2 overlapPoint = FindOverlap(ec, overlapCollider);


            Vector2 bisectPoint = (transform.position + overlapCollider.transform.position) / 2;
            Vector2 bisectDirection = (overlapPoint - bisectPoint).normalized;

            Debug.DrawLine(overlapPoint, overlapPoint + bisectDirection * 5f, Color.black);

            lr.positionCount = 3;
            lr.SetPosition(1, overlapPoint);
            rch = Physics2D.Raycast(overlapPoint, bisectDirection, 5f, rayFilter);

            if (rch)
            {
                lr.SetPosition(2, rch.point);
                return rch.collider.gameObject;
            }
            else
            {
                lr.SetPosition(2, overlapPoint + bisectDirection * 5f);
                return null;
            }
        }
        else
        {
            lr.positionCount = 2;
            rch = Physics2D.Raycast(transform.position, transform.right, lightRange, rayFilter);
            
            if (rch)
            {
                lr.SetPosition(1, rch.point);
                return rch.collider.gameObject;
            }
            else
            {
                lr.SetPosition(1, transform.position + transform.right * lightRange);
                return null;
            }
        }


    }
}
