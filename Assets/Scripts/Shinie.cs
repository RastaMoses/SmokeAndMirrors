using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
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
=======
public abstract class Shinie : MonoBehaviour
{
    public Color requiredColor;
    public Color currentColor;

    public abstract void MakeVisible();
    public abstract void MakeInvisible();

    public abstract void AddColor(Color color);
    public abstract void SubtractColor(Color color);

    public static bool MatchColor(Color a, Color b){
        return a.r == b.r && a.g == b.g && a.b == b.b;
    }

>>>>>>> PrezLights
}
