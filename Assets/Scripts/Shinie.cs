using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

}
