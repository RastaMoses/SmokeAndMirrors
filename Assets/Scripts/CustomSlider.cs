using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class CustomSlider : MonoBehaviour
{
    public string v;
    public Sprite[] s;
    public Sprite[] i;
    public UnityEngine.UI.Image I;
    [Range(0,1)]public float f;
    // Start is called before the first frame update
    void Start()
    {
        f = PlayerPrefs.GetFloat(v);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<UnityEngine.UI.Image>().sprite = s[(int)(f * 8) % s.Length];
        PlayerPrefs.SetFloat(v, Map(0,1,-80,0,f));
    }

    float Map(float a, float b, float x, float y, float m)
    {
        return ((b-m)*(x-y))/(b-a);
    }
}
