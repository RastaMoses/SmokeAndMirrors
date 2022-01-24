using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class L : MonoBehaviour
{
    public Vector3 direction;
    public float lightRange;
    public L spouseLight;
    public L childLight;
    public bool isParent;
    public bool isChild;
    public bool tm;
    public LineRenderer lr;
    public Color lightColor;
    public Material lightMaterial;
    GameObject shinyObj;
    public RaycastHit2D rch;
    public LayerMask lm;

    void Awake()
    {
        isParent = false;
        isChild = false;

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

        if (!isChild) direction = transform.up;

        lr.SetPosition(0, transform.position);

        if (spouseLight == null)
        {
            rch = Physics2D.Raycast(transform.position, direction, lightRange, lm);
            if (rch)
            {
                if (rch.collider.tag == "Mirror")
                {
                    tm = true;
                    isParent = true;
                    lr.SetPosition(1, rch.point);
                    if (childLight == null)
                    {
                        GameObject g = new GameObject("Mirror Child");
                        childLight = g.AddComponent<L>();
                        childLight.transform.position = rch.point;
                        childLight.isChild = true;
                        childLight.direction = Vector2.Reflect(rch.point - (Vector2)transform.position, rch.normal).normalized;
                        childLight.lightRange = lightRange;
                        childLight.lightColor = lightColor;
                        childLight.lightMaterial = lightMaterial;
                        childLight.transform.parent = rch.collider.transform;
                        childLight.lm = lm;
                        FindObjectOfType<LC>().ls.Add(childLight);
                    }
                    else
                    {
                        childLight.transform.position = rch.point;
                        childLight.direction = Vector2.Reflect(rch.point - (Vector2)transform.position, rch.normal).normalized;
                    }
                }
                else if (rch.collider.tag == "Teleporter")
                {
                    tm = true;
                    isParent = true;
                    lr.SetPosition(1, rch.point);
                    if (childLight == null)
                    {
                        GameObject g = new GameObject("Teleporter Child");
                        childLight = g.AddComponent<L>();
                        childLight.transform.position = rch.collider.GetComponent<Teleporter>().otherSide.transform.position;
                        childLight.isChild = true;
                        childLight.direction = rch.collider.GetComponent<Teleporter>().otherSide.transform.up;
                        childLight.lightRange = lightRange;
                        childLight.lightColor = lightColor;
                        childLight.lightMaterial = lightMaterial;
                        childLight.transform.parent = rch.collider.transform;
                        childLight.lm = lm;
                        FindObjectOfType<LC>().ls.Add(childLight);
                    }
                    else
                    {
                        childLight.transform.position = rch.collider.GetComponent<Teleporter>().otherSide.transform.position;
                        childLight.direction = rch.collider.GetComponent<Teleporter>().otherSide.transform.up;
                    }
                }
                else if (rch.collider.tag == "ShinyParent")
                {
                    tm = false;
                    // isParent = true;
                    if (shinyObj != rch.collider.gameObject)
                    {
                        if (shinyObj != null) shinyObj.GetComponent<ShinyParent>().MassDeact();
                        shinyObj = rch.collider.gameObject;
                    }

                    if (shinyObj.GetComponent<ShinyParent>().actColor == lightColor)
                    {
                        shinyObj.GetComponent<ShinyParent>().MassAct();
                        lr.SetPosition(1, rch.point);
                    }
                    else
                    {
                        lr.SetPosition(1, transform.position + direction * lightRange);
                    }
                }
                else
                {
                    tm = false;
                    isParent = false;
                    lr.SetPosition(1, transform.position + direction * lightRange);
                }
            }
            else
            {
                tm = false;
                lr.SetPosition(1, transform.position + direction * lightRange);
                if (shinyObj != null)
                {
                    shinyObj.GetComponent<ShinyParent>().MassDeact();
                    shinyObj = null;
                }
                if (childLight != null)
                {
                    isParent = false;
                    FindObjectOfType<LC>().ls.Remove(childLight);
                    Destroy(childLight.gameObject);
                    childLight = null;
                }
            }
        }
        else
        {
            tm = false;
            lr.SetPosition(1, childLight.transform.position);
        }
    }


    public void AddToSwap()
    {
        if (FindObjectOfType<LC>().swapCon[0] == null) FindObjectOfType<LC>().swapCon[0] = this;
        else FindObjectOfType<LC>().swapCon[1] = this;
    }

    public void CancelSwap()
    {
        LC lc = FindObjectOfType<LC>();
        if (lc.swapCon.Contains(this))
        {
            lc.CancelSwap();
        }
    }

    void OnEnable()
    {
        lr.enabled = true;
    }

    void OnDisable()
    {
        if (shinyObj != null)
        {
            shinyObj.GetComponent<ShinyParent>().MassDeact();
            shinyObj = null;
        }
        lr.enabled = false;

    }

    void OnDestroy()
    {
        if (FindObjectOfType<LC>() != null)
        {
            if (spouseLight != null) FindObjectOfType<LC>().Divorce(this, spouseLight);
            FindObjectOfType<LC>().KillChildren(this);
        }

        
    }
}
