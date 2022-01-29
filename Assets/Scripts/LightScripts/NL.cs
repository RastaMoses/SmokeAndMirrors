using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class NL : MonoBehaviour
{
    public float lRng;
    public Color lC;
    public Vector3 d;
    public bool iR, iB;
    public NL sL, cL;
    public RaycastHit2D rch;
    public List<RaycastHit2D> rchl;
    public ShinyParent sO;
    public LineRenderer lr;
    // Start is called before the first frame update
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.startColor = lC;
        lr.endColor = lC;
        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;
        rchl = new List<RaycastHit2D>();
    }

    private void OnEnable()
    {
        if (iR) GetComponent<SFX>().LightsOn();
    }

    private void OnDisable()
    {
        if(iR) GetComponent<SFX>().LightsOff();
    }
}
