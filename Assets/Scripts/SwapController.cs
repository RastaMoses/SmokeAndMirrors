using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapController : MonoBehaviour
{
    public LightScript[] swaps;
    // Start is called before the first frame update
    void Start()
    {
        swaps = new LightScript[2];
    }

    // Update is called once per frame
    void Update()
    {

        if (swaps[1] != null)
        {
            SwapColors(swaps[0], swaps[1]);
            swaps[0] = null;
            swaps[1] = null;
        }
    }

    void SwapColors(LightScript a, LightScript b)
    {
        Color swap = a.lightColor;
        a.lightColor = b.lightColor;
        b.lightColor = swap;
    }
}
