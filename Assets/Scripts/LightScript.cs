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
        ec.OverlapCollider(contactFilter,overlapColliders);
        overlapColliders = FilterEdgeColliders(overlapColliders);
    }

    static List<Collider2D> FilterEdgeColliders(List<Collider2D> colliderList)
    {
        List<Collider2D> outputList = new List<Collider2D>();
        foreach (Collider2D col in colliderList)
        {
            if (col.GetType() == typeof(EdgeCollider2D))
            {
                outputList.Add(col);
            }
        }
        return outputList;
    }
}
