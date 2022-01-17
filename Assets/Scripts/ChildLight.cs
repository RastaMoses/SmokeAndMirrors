using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildLight : LightScript
{
    void Update()
    {
        if (!isParent)
        {
            rch = Physics2D.Raycast(transform.position, direction, lightRange);
            if (rch)
            {
                Debug.DrawLine(transform.position, rch.point);
                if (rch.collider.tag == "Mirror")
                {
                    if (childLight == null)
                    {
                        childLight = new GameObject("Mirror Child");
                        childLight.transform.position = rch.point;
                        FindObjectOfType<LightController>().lights.Add(childLight.AddComponent<ChildLight>());
                        childLight.GetComponent<ChildLight>().direction = Vector2.Reflect(rch.point - (Vector2)transform.position, rch.normal);
                        childLight.GetComponent<ChildLight>().lightRange = lightRange;
                        childLight.transform.parent = rch.collider.transform;
                    }
                    else
                    {
                        childLight.transform.position = rch.point;
                        childLight.GetComponent<ChildLight>().direction = Vector2.Reflect(rch.point - (Vector2)transform.position, rch.normal);
                    }
                }
                if (rch.collider.tag == "Teleporter")
                {
                    if (childLight == null)
                    {
                        childLight = new GameObject("Teleporter Child");
                        childLight.transform.position = rch.collider.GetComponent<Teleporter>().otherSide.transform.position;
                        FindObjectOfType<LightController>().lights.Add(childLight.AddComponent<ChildLight>());
                        childLight.GetComponent<ChildLight>().direction = rch.collider.GetComponent<Teleporter>().otherSide.transform.right;
                        childLight.GetComponent<ChildLight>().lightRange = lightRange;
                        childLight.transform.parent = rch.collider.transform;
                    }
                    else
                    {
                        childLight.transform.position = rch.collider.GetComponent<Teleporter>().otherSide.transform.position;
                        childLight.GetComponent<ChildLight>().direction = rch.collider.GetComponent<Teleporter>().otherSide.transform.right;
                    }
                }
                if (rch.collider.tag == "Platform")
                {
                    
                }
            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + direction * lightRange);
                if (childLight != null)
                {
                    FindObjectOfType<LightController>().lights.Remove(childLight.GetComponent<LightScript>());
                    Destroy(childLight);
                    childLight = null;
                }
            }
        }
        else
        {
            Debug.DrawLine(transform.position, childLight.transform.position);
        }
    }
}
