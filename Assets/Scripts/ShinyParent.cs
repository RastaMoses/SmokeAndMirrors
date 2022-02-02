using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinyParent : MonoBehaviour
{
    Shiny[] shinies;
    public Color actColor;
    public bool activated;
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
            if(gameObject.activeSelf) s.Invisibilize();


        }
        activated = false;

    }

    public void MassAct()
    {
        foreach (Shiny s in shinies)
        {
            if (gameObject.activeSelf) s.Visibilize();
        }
        activated = true;
        if (GetComponent<SFX>()) GetComponent<SFX>().Shinies();

    }
}
