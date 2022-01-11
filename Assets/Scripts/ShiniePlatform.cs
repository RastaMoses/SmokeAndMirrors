using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShiniePlatform : Shinie
{
    [SerializeField] bool debugVisible;
    
    // Start is called before the first frame update
    void Start()
    {
        MakeInvisible();
        //GetComponent<SpriteRenderer>().color = requiredColor;
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
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public override void MakeVisible(){
        debugVisible = true;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().isTrigger = false;
    }

    public override void AddColor(Color color)
    {
        currentColor.r += color.r;
        currentColor.g += color.g;
        currentColor.b += color.b;

        currentColor.r = Mathf.Clamp(currentColor.r, 0, 1);
        currentColor.g = Mathf.Clamp(currentColor.g, 0, 1);
        currentColor.b = Mathf.Clamp(currentColor.b, 0, 1);       
    }

    public override void SubtractColor(Color color)
    {
        currentColor.r -= color.r;
        currentColor.g -= color.g;
        currentColor.b -= color.b;

        currentColor.r = Mathf.Clamp(currentColor.r, 0, 1);
        currentColor.g = Mathf.Clamp(currentColor.g, 0, 1);
        currentColor.b = Mathf.Clamp(currentColor.b, 0, 1);
    }
}
