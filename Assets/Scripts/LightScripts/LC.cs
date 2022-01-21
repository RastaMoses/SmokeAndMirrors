using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class LC : MonoBehaviour
{
    public List<L> ls;
    public L[] swapCon;
    // Start is called before the first frame update
    void Start()
    {
        ls = FindObjectsOfType<L>().ToList();
        swapCon = new L[2];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ls.Count; i++)
        {
            L m = ls[i];
            for (int j = 0; j < ls.Count; j++)
            {
                if (i == j)
                {
                    continue;
                }

                L n = ls[j];

                if (H.IsIntersecting(m, n) && !m.isParent && !n.isParent)
                {
                    Wedding(m, n);
                }

                if (H.IsIntersecting(m, n) && m.isParent && n.isParent && m.spouseLight == n && n.spouseLight == m)
                {
                    MarriedLife(m, n);
                }

                if (!H.IsIntersecting(m, n) && m.isParent && n.isParent && m.childLight == n.childLight)
                {
                    Divorce(m, n);
                }

                if (H.IsIntersecting(m, n) && m.isParent && !n.isParent)
                {
                    Affair(m, n);
                }
            }
        }

        if (swapCon[1] != null)
        {
            Color a = swapCon[0].lightColor;
            Color b = swapCon[1].lightColor;
            swapCon[0].lightColor = b;
            swapCon[1].lightColor = a;
            
            Material c = swapCon[0].transform.Find("Bulb").GetComponent<MeshRenderer>().material;
            Material d = swapCon[1].transform.Find("Bulb").GetComponent<MeshRenderer>().material;
            swapCon[0].transform.Find("Bulb").GetComponent<MeshRenderer>().material = d;        
            swapCon[1].transform.Find("Bulb").GetComponent<MeshRenderer>().material = c;

            Mesh e = swapCon[0].transform.Find("Housing").GetComponent<MeshFilter>().mesh;
            Mesh f = swapCon[1].transform.Find("Housing").GetComponent<MeshFilter>().mesh;
            swapCon[0].transform.Find("Housing").GetComponent<MeshFilter>().mesh = f;
            swapCon[1].transform.Find("Housing").GetComponent<MeshFilter>().mesh = e;

            swapCon[0] = null;
            swapCon[1] = null;
        }
    }

    private void Affair(L m, L n)
    {
        Vector2 o = H.O(m, n);
        Vector2 d = H.Bi(m.transform.position, n.transform.position, o);
        if (m.spouseLight != null)
        {
            if (Vector2.Distance(m.transform.position, o) < Vector2.Distance(m.transform.position, m.childLight.transform.position))
            {
                Divorce(m, m.spouseLight);
                Wedding(m, n);
            }
        }
        else
        {
            if (Vector2.Distance(m.transform.position, o) < Vector2.Distance(m.transform.position, m.rch.collider.transform.position))
            {
                Wedding(m, n);
            }
        }
    }

    private void Divorce(L m, L n)
    {
        KillChildren(m);
        KillChildren(n);

        m.childLight = null;
        n.childLight = null;

        m.spouseLight = null;
        n.spouseLight = null;

        m.isParent = false;
        n.isParent = false;
    }

    private static void MarriedLife(L m, L n)
    {
        Vector2 o = H.O(m, n);
        Vector2 d = H.Bi(m.transform.position, n.transform.position, o);

        m.childLight.transform.position = o;
        m.childLight.direction = d;
    }

    private void Wedding(L m, L n)
    {

        KillChildren(m);
        KillChildren(n);

        m.spouseLight = n;
        n.spouseLight = m;

        m.isParent = true;
        n.isParent = true;

        Vector2 o = H.O(m, n);
        Vector2 d = H.Bi(m.transform.position, n.transform.position, o);

        if (m.childLight == null && n.childLight == null)
        {
            GameObject g = new GameObject("Child Light");
            g.AddComponent<L>();

            m.childLight = g.GetComponent<L>();
            n.childLight = g.GetComponent<L>();

            m.childLight.isChild = true;
            m.childLight.transform.position = o;
            m.childLight.direction = d;
            m.childLight.lightRange = m.lightRange;
            m.childLight.lightColor = H.AddColor(m, n);
            m.childLight.lightMaterial = m.lightMaterial;

            if (!ls.Contains(m.childLight)) ls.Add(m.childLight);
        }
    }

    void KillChildren(L l)
    {
        if (l.childLight == null) return;
        KillChildren(l.childLight);
        if (l.childLight.spouseLight != null) l.childLight.spouseLight.childLight = null;
        if (l.childLight.spouseLight != null) l.childLight.spouseLight.spouseLight = null;
        if (l.childLight.spouseLight != null) l.childLight.spouseLight.isParent = false;
        if (ls.Contains(l.childLight)) ls.Remove(l.childLight);
        if (l.childLight.gameObject != null) Destroy(l.childLight.gameObject);
        l.childLight = null;
    }
}
