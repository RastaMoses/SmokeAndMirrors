using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public List<LightScript> lights;
    // Start is called before the first frame update
    void Start()
    {
        lights = FindObjectsOfType<LightScript>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            LightScript m = lights[i];
            for (int j = 0; j < lights.Count; j++)
            {
                if (i == j)
                {
                    continue;
                }

                LightScript n = lights[j];


                Marriage(m, n);
                ChildWelfare(m, n);
                Divorce(m, n);
                Affair(m, n);
            }
        }
    }



    private void Affair(LightScript m, LightScript n)
    {
        if (F.IsIntersecting(m, n) && m.isParent && !n.isParent)
        {
            Vector2 overlap = F.GetOverlap(m, n);
            Vector2 direction = F.FindBisection(m.transform.position, n.transform.position, overlap);

            if (Vector2.Distance(m.transform.position, overlap) < Vector2.Distance(m.transform.position, m.childLight.transform.position) - 0.05f)
            {
                print("Affair");
                LightScript o = m.spouseLight;

                if (m.childLight != null)
                {
                    LightScript l = m.childLight.GetComponent<ChildLight>();
                    if (lights.Contains(l))
                    {
                        lights.Remove(l);
                    }
                    Destroy(m.childLight);
                }

                if (o.childLight != null)
                {
                    LightScript l = o.childLight.GetComponent<ChildLight>();
                    if (lights.Contains(l))
                    {
                        lights.Remove(l);
                    }
                    Destroy(m.childLight);
                }

                m.childLight = null;
                o.childLight = null;

                m.isParent = false;
                o.isParent = false;

                Marriage(m, n);
            }
        }
    }

    private void Divorce(LightScript m, LightScript n)
    {
        if (!F.IsIntersecting(m, n) && m.isParent && n.isParent && m.spouseLight == n && n.spouseLight == m)
        {
            if (m.childLight != null)
            {
                LightScript l = m.childLight.GetComponent<ChildLight>();
                if (lights.Contains(l))
                {
                    lights.Remove(l);
                }
                Destroy(m.childLight);
            }

            if (n.childLight != null)
            {
                LightScript l = n.childLight.GetComponent<ChildLight>();
                if (lights.Contains(l))
                {
                    lights.Remove(l);
                }
                Destroy(n.childLight);
            }

            m.spouseLight = null;
            n.spouseLight = null;

            m.childLight = null;
            n.childLight = null;

            m.isParent = false;
            n.isParent = false;
        }
    }

    private static void ChildWelfare(LightScript m, LightScript n)
    {
        if (F.IsIntersecting(m, n) && m.isParent && n.isParent && m.spouseLight == n && n.spouseLight == m)
        {
            Vector2 overlap = F.GetOverlap(m, n);
            Vector2 direction = F.FindBisection(m.transform.position, n.transform.position, overlap);

            m.childLight.transform.position = overlap;

            m.childLight.GetComponent<ChildLight>().direction = direction;
            m.childLight.GetComponent<ChildLight>().lightRange = m.lightRange;
        }
    }

    private void Marriage(LightScript m, LightScript n)
    {
        if (F.IsIntersecting(m, n) && !m.isParent && !n.isParent)
        {
            if (m.childLight != null)
            {
                lights.Remove(m.childLight.GetComponent<LightScript>());
                Destroy(m.childLight);
                m.childLight = null;
            }

            if (n.childLight != null)
            {
                lights.Remove(n.childLight.GetComponent<LightScript>());
                Destroy(n.childLight);
                n.childLight = null;
            }
            
            m.isParent = true;
            n.isParent = true;

            m.spouseLight = n;
            n.spouseLight = m;

            Vector2 overlap = F.GetOverlap(m, n);
            Vector2 direction = F.FindBisection(m.transform.position, n.transform.position, overlap);

            if (m.childLight == null && n.childLight == null)
            {
                GameObject g = new GameObject("ChildLight");
                m.childLight = g;
                n.childLight = g;

                m.childLight.transform.position = overlap;
                m.childLight.AddComponent<ChildLight>();
                m.childLight.GetComponent<ChildLight>().direction = direction;
                m.childLight.GetComponent<ChildLight>().lightRange = m.lightRange;

                LightScript l = m.childLight.GetComponent<ChildLight>();
                if (!lights.Contains(l))
                {
                    lights.Add(l);
                }
            }
        }
    }
}
