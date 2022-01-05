using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    [SerializeField] ContactFilter2D contactFilter;
    List<ContactPoint2D> contacts;
    [SerializeField] List<Collider2D> overlapColliders;
    EdgeCollider2D ec;
    // Start is called before the first frame update
    void Start()
    {
        ec = GetComponent<EdgeCollider2D>();
        contacts = new List<ContactPoint2D>();
        overlapColliders = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ec.OverlapCollider(contactFilter, overlapColliders);
        List<EdgeCollider2D> edgeColliders = HelperFunctions.FilterEdgeColliders(overlapColliders);

        List<Vector2> points = HelperFunctions.PointsToVectors(ec);
        Debug.DrawLine(ec.points[0], ec.points[1]);

        if (edgeColliders.Count > 0)
        {

            List<Vector2> ptv = HelperFunctions.PointsToVectors(ec);
            ptv = HelperFunctions.VectorsToPoints(ptv, gameObject);
            Debug.DrawLine(ptv[0], ptv[1]);

            print(transform.position);
            print(transform.right);
            print(transform.up);


        }
        else
        {
            ec.SetPoints(new List<Vector2> { new Vector2(0, 0), new Vector2(10, 0) });
        }
    }


}
