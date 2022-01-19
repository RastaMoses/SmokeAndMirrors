using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightScript : MonoBehaviour
{
    public Vector3 direction;
    public float lightRange;
    public GameObject childLight;
    public LightScript spouseLight;
    public bool isParent;
    public Color lightColor;
    public LineRenderer lr;
    public Material lightMaterial;

    protected RaycastHit2D rch;

    void Awake()
    {
        isParent = false;
        lr = GetComponent<LineRenderer>();
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lr.startColor = lightColor;
        lr.endColor = lightColor;
        lr.material = lightMaterial;
        lr.startWidth = 0.24f;
        lr.endWidth = 0.24f;

        lr.SetPosition(0, transform.position);
        direction = transform.right;

        if (!isParent)
        {
            rch = Physics2D.Raycast(transform.position, direction, lightRange);
            if (rch)
            {
                Debug.DrawLine(transform.position, rch.point, lightColor);
                if (rch.collider.tag == "Mirror")
                {
                    lr.SetPosition(1, rch.point);
                    if (childLight == null)
                    {
                        childLight = new GameObject("Mirror Child");
                        childLight.transform.position = rch.point;
                        FindObjectOfType<LightController>().lights.Add(childLight.AddComponent<ChildLight>());
                        childLight.GetComponent<ChildLight>().direction = Vector2.Reflect(rch.point - (Vector2)transform.position, rch.normal);
                        childLight.GetComponent<ChildLight>().lightRange = lightRange;
                        childLight.GetComponent<ChildLight>().lightColor = lightColor;
                        childLight.GetComponent<ChildLight>().lightMaterial = lightMaterial;
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
                    lr.SetPosition(1, rch.point);
                    if (childLight == null)
                    {
                        childLight = new GameObject("Teleporter Child");
                        childLight.transform.position = rch.collider.GetComponent<Teleporter>().otherSide.transform.position;
                        FindObjectOfType<LightController>().lights.Add(childLight.AddComponent<ChildLight>());
                        childLight.GetComponent<ChildLight>().direction = rch.collider.GetComponent<Teleporter>().otherSide.transform.right;
                        childLight.GetComponent<ChildLight>().lightRange = lightRange;
                        childLight.GetComponent<ChildLight>().lightColor = lightColor;
                        childLight.GetComponent<ChildLight>().lightMaterial = lightMaterial;
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
                Debug.DrawLine(transform.position, transform.position + direction * lightRange, lightColor);
                lr.SetPosition(1, transform.position + direction * lightRange);
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
            Debug.DrawLine(transform.position, childLight.transform.position, lightColor);
            lr.SetPosition(1, childLight.transform.position);
        }
    }
}
