using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShiniePlatform : Shinie
{
    [SerializeField] Color requiredColor;
    public Color currentColor;
    [SerializeField] bool debugVisible;
    
    // Start is called before the first frame update
    void Start()
    {
        MakeInvisible();
        GetComponent<SpriteRenderer>().color = requiredColor;
        currentColor = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (MatchColor(currentColor, requiredColor))
        {
            MakeVisible();
        }
        else
        {
            MakeInvisible();
        }
    }

    public override void MakeInvisible(){
        debugVisible = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public override void MakeVisible(){
        debugVisible = true;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
