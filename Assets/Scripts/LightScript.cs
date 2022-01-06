using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public float lightRange;
    public List<Vector2> points;
    EdgeCollider2D ec;
    List<Collider2D> overlapColliders;
    List<EdgeCollider2D> overlapEdges;

    // Start is called before the first frame update
    void Start()
    {
        ec = GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
