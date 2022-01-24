using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class NL : MonoBehaviour
{
    public float lRng;
    public Color lC;
    [HideInInspector]public Vector3 d;
    [HideInInspector]public bool iR, iB;
    [HideInInspector]public NL sL, cL;
    [HideInInspector]public RaycastHit2D rch;
    [HideInInspector]public ShinyParent sO;
    [HideInInspector]public LineRenderer lr;
    // Start is called before the first frame update
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.startColor = lC;
        lr.endColor = lC;
        lr.startWidth = 0.2f;
        lr.endWidth = 0.2f;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
