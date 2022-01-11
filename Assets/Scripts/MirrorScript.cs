using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MirrorScript : MonoBehaviour
{
    LineRenderer lr;
    public Color lightColor;
    RaycastHit2D rch;
    public LayerMask layer;
    [SerializeField] float lightRange;
    GameObject currentObject, previousObject;
    public bool swappable;
    SwapController swap;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        swap = FindObjectOfType<SwapController>();
    }

    // Update is called once per frame
    void Update()
    {
        // GetComponent<SpriteRenderer>().color = lightColor;
        lr.startColor = lightColor;
        lr.endColor = lightColor;
        lr.SetPosition(0, transform.position);
        GameObject hitObject = Hit();
        if (hitObject)
        {
            currentObject = hitObject;
            if (previousObject != null && previousObject != currentObject)
            {
                RevertObject(previousObject);
                previousObject = currentObject;
            }
            else
            {
                previousObject = currentObject;
            }
            
            UpdateObject(hitObject);
        }
        else
        {
            if (currentObject != null)
            {
                RevertObject(currentObject);
            }
        }
    }

    private void RevertObject(GameObject obj)
    {
        if (obj.tag == "Platform")
        {
            obj.GetComponent<Platform>().currentColor = Color.black;
            return;
        }
        if (obj.tag == "Teleporter")
        {
            obj.GetComponent<Teleporter>().otherEnd.lightColor = Color.white;
            obj.GetComponent<Teleporter>().teleporting = false;
            return;
        }
        if (obj.tag == "Mirror")
        {
            obj.GetComponent<Mirror>().reflecting = false;
            return;
        }
        if (obj.tag == "Smoke")
        {
            print("Smoke");
            return;
        }
    }

    private void UpdateObject(GameObject obj)
    {
        if (obj.tag == "Platform")
        {
            obj.GetComponent<Platform>().AddColor(lightColor);
            return;
        }
        if (obj.tag == "Teleporter" && obj != gameObject)
        {
            obj.GetComponent<Teleporter>().otherEnd.lightColor = lightColor;
            obj.GetComponent<Teleporter>().teleporting = true;
            return;
        }
        if (obj.tag == "Mirror" && obj != gameObject)
        {
            obj.GetComponent<LightScript>().lightColor = lightColor;
            obj.GetComponent<Mirror>().reflecting = true;
            return;
        }
        if (obj.tag == "Smoke")
        {
            print("Smoke");
            return;
        }
    }

    private GameObject Hit()
    {
        rch = Physics2D.Raycast(transform.position, transform.up * lightRange, lightRange, layer);
        if (rch)
        {
            if (rch.collider.tag == "Platform" && !rch.collider.GetComponent<Platform>().activated)
            {
                lr.SetPosition(1, transform.position + transform.up * lightRange);
            }
            else
            {
                lr.SetPosition(1, rch.point);
            }
            Debug.DrawLine(transform.position, transform.position + transform.up * lightRange, Color.red);
            return rch.collider.gameObject;
        }
        else
        {
            lr.SetPosition(1, transform.position + transform.up * lightRange);
            Debug.DrawLine(transform.position, transform.position + transform.up * lightRange, Color.black);
            return null;
        }
    }
}