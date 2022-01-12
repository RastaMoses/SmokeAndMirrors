using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Color requiredColor;
    public Color currentColor;
    Color startColor;
    public bool activated;
    [SerializeField] GameObject transparentVersion;
    // Start is called before the first frame update
    void Start()
    {
        currentColor = Color.black;
        startColor = Color.black;
        if (GetComponent<SpriteRenderer>())
        {

            GetComponent<SpriteRenderer>().color = requiredColor;
            GetComponent<SpriteRenderer>().enabled = false;
        }
        if (GetComponent<MeshRenderer>())
        {

            GetComponent<MeshRenderer>().enabled = false;
        }
        GetComponent<BoxCollider2D>().isTrigger = true;
        foreach (Transform child in transform)
        {
            if (child.GetComponent<SpriteRenderer>())
            {

                child.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (child.GetComponent<MeshRenderer>())
            {

                child.GetComponent<MeshRenderer>().enabled = false;
            }
            if (child.GetComponent<BoxCollider2D>())
            {
                child.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
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
            transparentVersion.SetActive(false); 
            if (GetComponent<SpriteRenderer>())
            {

                GetComponent<SpriteRenderer>().enabled = true;
            }
            if (GetComponent<MeshRenderer>())
            {

                GetComponent<MeshRenderer>().enabled = true;
            }
            GetComponent<BoxCollider2D>().isTrigger = false;
            foreach (Transform child in transform)
            {
                if (child.GetComponent<SpriteRenderer>())
                {

                    child.GetComponent<SpriteRenderer>().enabled = true;
                }
                if (child.GetComponent<BoxCollider2D>())
                {
                    child.GetComponent<BoxCollider2D>().isTrigger = false;
                }
                if (child.GetComponent<MeshRenderer>())
                {
                    child.GetComponent<MeshRenderer>().enabled = true;
                }
                
            }

        }
        else
        {
            transparentVersion.SetActive(true);
            if (GetComponent<SpriteRenderer>())
            {

                GetComponent<SpriteRenderer>().enabled = false;
            }
            if (GetComponent<MeshRenderer>())
            {

                GetComponent<MeshRenderer>().enabled = false;
            }
            GetComponent<BoxCollider2D>().isTrigger = true;
            foreach (Transform child in transform)
            {
                if (child.GetComponent<SpriteRenderer>())
                {

                    child.GetComponent<SpriteRenderer>().enabled = false;
                }
                if (child.GetComponent<BoxCollider2D>())
                {
                    child.GetComponent<BoxCollider2D>().isTrigger = true;
                }
                if (child.GetComponent<MeshRenderer>())
                {
                    child.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }

    }

    public void AddColor(Color c)
    {
        /*
        currentColor.r += c.r;
        currentColor.g += c.g;
        currentColor.b += c.b;

        currentColor.r = Mathf.Clamp(currentColor.r, 0, 1);
        currentColor.g = Mathf.Clamp(currentColor.g, 0, 1);
        currentColor.b = Mathf.Clamp(currentColor.b, 0, 1);
        */
        if (currentColor != startColor)
        {
            return;
        }
        else
        {
            currentColor = c;
        }

    }

    bool MatchColor(Color a, Color b)
    {
        return a.r == b.r && a.g == b.g && a.b == b.b;
    }
}
