using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shinie : MonoBehaviour
{

    public abstract void MakeVisible();
    public abstract void MakeInvisible();

    public static bool MatchColor(Color a, Color b){
        return a.r == b.r && a.g == b.g && a.b == b.b;
    }

}
