using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Color requiredColor;
    public Color currentColor;
    public bool activated;
    // Start is called before the first frame update
    void Start()
    {
        currentColor = Color.black;
        GetComponent<SpriteRenderer>().color = requiredColor;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (MatchColor(currentColor, requiredColor))
        {
            activated = true;
        }
        else
        {
            activated = false;
        }

        if (activated)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

    }

    public void AddColor(Color c)
    {
        currentColor.r += c.r;
        currentColor.g += c.g;
        currentColor.b += c.b;

        currentColor.r = Mathf.Clamp(currentColor.r, 0, 1);
        currentColor.g = Mathf.Clamp(currentColor.g, 0, 1);
        currentColor.b = Mathf.Clamp(currentColor.b, 0, 1);
    }

    bool MatchColor(Color a, Color b)
    {
        return a.r == b.r && a.g == b.g && a.b == b.b;
    }
}
