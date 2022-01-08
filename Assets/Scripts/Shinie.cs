using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shinie : MonoBehaviour
{
    SpriteRenderer sr;
    Collider2D col;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        MakeInvisible();
    }

    void Update()
    {

    }

    public void MakeVisible(){
        sr.enabled = true;
        col.isTrigger = false;
    }

    public void MakeInvisible(){
        col.isTrigger = true;
        sr.enabled = false;
        // col.enabled = false;
    }
}
