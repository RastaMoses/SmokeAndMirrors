using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinyParent : MonoBehaviour
{
    Shiny[] shinies;
    public Color actColor;
    // Start is called before the first frame update
    void Start()
    {
        shinies = GetComponentsInChildren<Shiny>();
        if (GetComponent<Collider2D>() != null)
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
        
    }

    // Update is called once per frame
    public void MassDeact()
    {
        foreach (Shiny s in shinies)
        {
            s.Invisibilize();
        }
    }

    public void MassAct()
    {
        foreach (Shiny s in shinies)
        {
            s.Visibilize();
        }
    }
}
