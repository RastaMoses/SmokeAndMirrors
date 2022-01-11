using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    Color lightColor;
    GameObject currentObject;
    bool reflecting;
    LineRenderer lr;
    EdgeCollider2D ec;
    List<RaycastHit2D> rch;
    [SerializeField] float lightRange;
    [SerializeField] ContactFilter2D contactFilter;
    List<Collider2D> results;
    GameObject originObject;
    Vector2 reflectOrigin;
    Vector2 reflectDirection;
    // Start is called before the first frame update
    void Start()
    {
        reflectOrigin = new Vector2(0, 0);
        reflectDirection = new Vector2(0, 0);
        rch = new List<RaycastHit2D>();
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position);
        ec = GetComponent<EdgeCollider2D>();
        ec.SetPoints(new List<Vector2> { new Vector2(0, 0), new Vector2(0, 0) });
        results = new List<Collider2D>();
        lr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (reflecting)
        {
            lr.enabled = true;
            lr.SetPositions(new Vector3[] { reflectOrigin, reflectOrigin + reflectDirection * 5f });
            Debug.DrawLine(reflectOrigin, reflectOrigin + reflectDirection * 5f, Color.black);
            ec.SetPoints(new List<Vector2> { transform.InverseTransformPoint(new Vector2(-transform.position.x + reflectOrigin.x, -transform.position.y + reflectOrigin.y)),
                                             transform.InverseTransformPoint(new Vector2(-transform.position.x + (reflectOrigin + reflectDirection * lightRange).x, -transform.position.y + (reflectOrigin + reflectDirection * lightRange).y)) });

            if (HitObject())
            {
                UpdateObject(currentObject);
            }
            else
            {
                RevertObject(currentObject);
            }
        }
        else
        {
            ec.SetPoints(new List<Vector2> { new Vector2(0, 0), new Vector2(0, 0) });
            lr.enabled = false;
            RevertObject(currentObject);
            lightColor = Color.black;
        }
    }

    void RevertObject(GameObject hit)
    {
        if (hit != null)
        {
            if (hit.tag == "Mirror" && hit.gameObject != this.gameObject)
            {
                hit.GetComponent<Mirror>().ReflectOff();
            }

            if (hit.tag == "Teleporter")
            {
                hit.GetComponent<Teleporter>().otherEnd.GetComponent<Teleporter>().lightColor = Color.black;
                hit.GetComponent<Teleporter>().otherEnd.GetComponent<Teleporter>().teleporting = false;
            }

            if (hit.tag == "Shiny")
            {
                hit.GetComponent<Shinie>().SubtractColor(lightColor);
            }

            currentObject = null;
        }
    }

    void UpdateObject(GameObject hit)
    {
        if (hit.tag == "Mirror" && hit.gameObject != this.gameObject)
        {
            hit.GetComponent<Mirror>().ReflectOn(gameObject, lightColor);
        }

        if (hit.tag == "Teleporter")
        {
            hit.GetComponent<Teleporter>().otherEnd.GetComponent<Teleporter>().lightColor = lightColor;
            hit.GetComponent<Teleporter>().otherEnd.GetComponent<Teleporter>().teleporting = true;
        }

        if (hit.tag == "Shiny")
        {
            hit.GetComponent<Shinie>().AddColor(lightColor);
        }
    }

    GameObject HitObject()
    {
        ec.OverlapCollider(contactFilter, results);
        EdgeCollider2D overlapCollider = DetectEdgeCollider(results);
        if (overlapCollider && overlapCollider.gameObject != currentObject && overlapCollider.gameObject != originObject)
        {

            Vector2 overlapPoint = FindOverlap(ec, overlapCollider);


            Vector2 bisectPoint = (transform.position + overlapCollider.transform.position) / 2;
            Vector2 bisectDirection = (overlapPoint - bisectPoint).normalized;

            Debug.DrawLine(overlapPoint, overlapPoint + bisectDirection * 5f, Color.black);

            lr.positionCount = 3;
            lr.SetPosition(1, overlapPoint);
            Physics2D.Raycast(overlapPoint, bisectDirection, contactFilter, rch, 5f);
            GameObject hitObject = FilterHitObject(rch);
            if (hitObject)
            {
                currentObject = hitObject;
                return hitObject;
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
            Physics2D.Raycast(reflectOrigin, reflectDirection, contactFilter, rch, 5f);
            GameObject hitObject = FilterHitObject(rch);

            if (hitObject)
            {
                currentObject = hitObject;
                return hitObject;
            }
            else
            {
                lr.SetPosition(1, reflectOrigin + reflectDirection * 5f);
                return null;
            }
        }
    }

    Vector2 FindOverlap(EdgeCollider2D thisEdge, EdgeCollider2D thatEdge)
    {
        Vector2 a = thisEdge.points[0] + (Vector2)thisEdge.transform.position;
        Vector2 b = thisEdge.transform.position + thisEdge.transform.right * thisEdge.points[1].x + thisEdge.transform.up * thisEdge.points[1].y;

        Vector2 c = thatEdge.points[0] + (Vector2)thatEdge.transform.position;
        Vector2 d = thatEdge.transform.position + thatEdge.transform.right * thatEdge.points[1].x + thatEdge.transform.up * thatEdge.points[1].y;

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
    private GameObject FilterHitObject(List<RaycastHit2D> rch)
    {
        foreach (RaycastHit2D rc in rch)
        {
            if (rc.collider.GetType() != typeof(EdgeCollider2D))
            {
                lr.SetPosition(lr.positionCount - 1, rc.point);
                return rc.collider.gameObject;
            }
        }
        return null;
    }

    public void ReflectOff()
    {
        reflecting = false;
        originObject = null;
    }

    public void ReflectOn(GameObject from, Color fromColor)
    {
        lightColor = fromColor;
        originObject = from;
        List<RaycastHit2D> rc = new List<RaycastHit2D>();

        Physics2D.Raycast(from.transform.position, from.transform.right, contactFilter, rc, lightRange);

        foreach (RaycastHit2D r in rc)
        {
            if (r.collider.GetType() != typeof(EdgeCollider2D))
            {
                reflectOrigin = r.point;
                reflectDirection = Vector2.Reflect(r.point - (Vector2)from.transform.position, r.normal).normalized;

                break;
            }
        }
        reflecting = true;
    }

}
