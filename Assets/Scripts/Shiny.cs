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
        mr = GetComponent<MeshRenderer>();
        col = GetComponent<Collider2D>();
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
        mr.material = vis;
        col.isTrigger = false;
    }

    public void Invisibilize()
    {
        mr.material = invis;
        col.isTrigger = true;
    }
}
