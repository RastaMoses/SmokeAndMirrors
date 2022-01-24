using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiny : MonoBehaviour
{
    MeshRenderer mr;
    [SerializeField] Material invis;
    [SerializeField] Material vis;
    Collider2D col;

    void Awake()
    {
        if (GetComponent<MeshRenderer>())
        {
            mr = GetComponent<MeshRenderer>();
        }
        
        if (GetComponent<Collider2D>() != null)
        {
            col = GetComponent<Collider2D>();
        }
        
        Invisibilize();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Visibilize()
    {
        if (GetComponent<MeshRenderer>())
        {
            mr.material = vis;
        }
        
        if (GetComponent<Collider2D>() != null)
        {
            col.isTrigger = false;
        }
        
    }

    public void Invisibilize()
    {
        if (GetComponent<MeshRenderer>())
        {
            mr.material = invis;
        }
        if (GetComponent<Collider2D>() != null)
        {
            col.isTrigger = true;
        }
    }
}
