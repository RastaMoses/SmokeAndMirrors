using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class NLC : MonoBehaviour
{
    public List<NL> ls;
    public Material lM;
    public ContactFilter2D cF;
    public NL[] sC;
    [Range(0, 0.1f)] public float dS;

    private SelectableObjController _selectableObjController;

    // Start is called before the first frame update
    void Start()
    {
        ls = FindObjectsOfType<NL>().ToList();
        sC = new NL[2];
        foreach (NL nl in ls) nl.iR = true;
        foreach (NL nl in ls) nl.lr.material = lM;

        _selectableObjController = FindObjectOfType<SelectableObjController>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (NL l in ls) if (l.iR) if (l.GetComponent<SwitchCondition>()) l.enabled = l.GetComponent<SwitchCondition>().on;

        //Set root directions to up
        foreach (NL nl in ls) if (nl.iR) nl.d = nl.transform.up;

        //Procreate
        for (int i = 0; i < ls.Count; i++)
        {
            NL m = ls[i];
            if (!m.enabled)
            {

                DL(m);
                continue;
            }

            for (int j = 0; j < ls.Count; j++)
            {
                NL n = ls[j];
                SR(m);
                SR(n);
                if (i == j) continue;
                if (!n.enabled)
                {
                    DL(n);
                    continue;
                }


                if (m.cL == n || n.cL == m) continue;
                if (H2.II(m, n) && !m.sL && !n.sL) W(m, n);
                if (H2.II(m, n) && m.sL == n && n.sL == m) BS(m, n);
                if (!H2.II(m, n) && m.sL == n && n.sL == m) D(m, n);
                if (H2.II(m, n) && m.sL && !n.sL) A(m, n);
            }

            if (!m.sL && m.rch)
            {
                if (m.rch.collider.tag == "Mirror" || m.rch.collider.tag == "Teleporter")
                {
                    Vector2 o = m.rch.collider.tag == "Mirror" ? m.rch.point : (Vector2)TT(m.rch.collider.transform).position;
                    Vector2 d = m.rch.collider.tag == "Mirror" ? Vector2.Reflect(m.rch.point - (Vector2)m.transform.position, m.rch.normal).normalized : (Vector2)TT(m.rch.collider.transform).up;

                    if (!m.cL) m.cL = MB(m, o, d);
                    else
                    {
                        m.cL.transform.position = o;
                        m.cL.d = d;
                    }
                }
                else
                {
                    CS(m);
                }
                if (m.rch.collider.tag == "ShinyParent" && m.rch.collider.GetComponent<ShinyParent>().actColor == m.lC)
                {
                    if (m.sO && m.rch.collider.GetComponent<ShinyParent>() != m.sO) m.sO.MassDeact();
                    m.sO = m.rch.collider.GetComponent<ShinyParent>();
                    if (!m.sO.activated) m.sO.MassAct();
                }
                else if (m.sO) m.sO.MassDeact();
            }

            if (!m.sL && !m.rch)
            {
                if (m.sO) m.sO.MassDeact();
                m.sO = null;
                CS(m);
            }
        }

        //Draw lines
        foreach (NL nl in ls)
        {
            float c = nl.cL ? Vector2.Distance(nl.transform.position, nl.cL.transform.position) : Mathf.Infinity;
            float r = nl.rch ? nl.rch.distance : Mathf.Infinity;

            if (nl.rch && nl.rch.collider.tag == "ShinyParent" && !nl.rch.collider.GetComponent<ShinyParent>().activated && nl.rch.collider.GetComponent<ShinyParent>().actColor != nl.lC) r = Mathf.Infinity;

            float f = Mathf.Min(nl.lRng, Mathf.Min(c, r));

            if (nl.enabled) Debug.DrawLine(nl.transform.position, nl.transform.position + nl.d * f, nl.lC);

            nl.lr.SetPosition(0, nl.transform.position);
            nl.lr.SetPosition(1, nl.transform.position + nl.d * f);

            nl.lr.enabled = nl.enabled;
        }
        //Swap
        if (sC[0] && sC[1])
        {
            Color a = sC[0].lC;
            Color b = sC[1].lC;
            sC[0].lC = b;
            sC[1].lC = a;
            sC[0].lr.startColor = b;
            sC[0].lr.endColor = b;
            sC[1].lr.startColor = a;
            sC[1].lr.endColor = a;


            Material c = sC[0].transform.Find("Bulb").GetComponent<MeshRenderer>().material;
            Material d = sC[1].transform.Find("Bulb").GetComponent<MeshRenderer>().material;
            sC[0].transform.Find("Bulb").GetComponent<MeshRenderer>().material = d;
            sC[1].transform.Find("Bulb").GetComponent<MeshRenderer>().material = c;

            // Mesh e = sC[0].transform.Find("Housing").GetComponent<MeshFilter>().mesh;
            // Mesh f = sC[1].transform.Find("Housing").GetComponent<MeshFilter>().mesh;
            // sC[0].transform.Find("Housing").GetComponent<MeshFilter>().mesh = f;
            // sC[1].transform.Find("Housing").GetComponent<MeshFilter>().mesh = e;

            Material g = sC[0].transform.Find("Housing").GetComponent<MeshRenderer>().material;
            Material h = sC[0].transform.Find("Housing").GetComponent<MeshRenderer>().material;
            sC[0].transform.Find("Housing").GetComponent<MeshRenderer>().material = h;
            sC[1].transform.Find("Housing").GetComponent<MeshRenderer>().material = g;

            sC[0] = null;
            sC[1] = null;
            Debug.Log("Swap");
            GetComponent<SFX>().SwitchBulb();

        }

    }
    public void CSW()
    {
        sC[0] = null;
        sC[1] = null;

        if (_selectableObjController != null)
            _selectableObjController.ActivateLightBulbIcon(false, Color.white);

        GetComponent<SFX>().CancelSelect();
        Debug.Log("Cancel Swap");
    }

    public void ATS(NL l)
    {
        if (!sC[0])
        {
            sC[0] = l;
            if (_selectableObjController != null)
                _selectableObjController.ActivateLightBulbIcon(true, l.lC);
        }
        else if (sC[0] != l)
        {
            sC[1] = l;
            if (_selectableObjController != null)
                _selectableObjController.ActivateLightBulbIcon(false, l.lC);
        }
        GetComponent<SFX>().SelectBulb();
    }

    private void SR(NL m)
    {
        Physics2D.Raycast(m.transform.position, m.d, cF, m.rchl, m.lRng);
        if(m.rchl.Count > 0) for (int i = 0; i < m.rchl.Count; i++)
        {
            if (m.rchl.Count > 0 && m.rchl[0].collider.tag == "ShinyParent" && !m.rchl[0].collider.GetComponent<ShinyParent>().activated && m.rchl[0].collider.GetComponent<ShinyParent>().actColor != m.lC) m.rchl.Remove(m.rchl[0]);
            if (m.rchl.Count > 0 && m.rchl[0].collider.tag == "Glass") m.rchl.Remove(m.rchl[0]);
        }

        if (m.rchl.Count > 0) m.rch = m.rchl[0];
        else m.rch = new RaycastHit2D();


    }

    private void DL(NL m)
    {
        if (m.sL) D(m, m.sL);
        if (m.sO) m.sO.MassDeact();
        m.sO = null;
        CS(m);
    }

    private Transform TT(Transform g)
    {
        return g.parent.GetChild(0) == g ? g.parent.GetChild(1) : g.parent.GetChild(0);
    }

    private NL MB(NL m, Vector2 o, Vector2 d)
    {
        GameObject g = new GameObject("BL", typeof(NL));
        g.transform.parent = m.rch.collider.transform;
        NL cL = g.GetComponent<NL>();
        cL.transform.position = o;
        cL.d = d;
        cL.lRng = m.lRng;
        cL.lC = m.lC;
        cL.lr.material = lM;
        cL.lr.startColor = m.lC;
        cL.lr.endColor = m.lC;
        if (!ls.Contains(cL)) ls.Add(cL);
        return cL;
    }

    private void A(NL m, NL n)
    {
        Vector2 o = H2.O(m, n);
        Vector2 d = H2.B(m.transform.position, n.transform.position, o);

        if (Vector2.Distance(m.transform.position, o) < Vector2.Distance(m.transform.position, m.cL.transform.position))
        {
            D(m, m.sL);
            W(m, n);
        }
    }

    //Divorce
    private void D(NL m, NL n)
    {
        CS(m);
        CS(n);

        m.sL = null;
        n.sL = null;
    }

    //Child slaughter
    private void CS(NL l)
    {
        if (!l.cL) return;
        CS(l.cL);

        if (l.cL.sL)
        {
            l.cL.sL.cL = null;
            l.cL.sL.sL = null;
        }

        if (l.cL.sO) l.cL.sO.MassDeact();
        if (ls.Contains(l.cL)) ls.Remove(l.cL);
        if (l.cL.gameObject) Destroy(l.cL.gameObject);
        l.cL = null;
    }

    private void BS(NL m, NL n)
    {
        Vector2 o = H2.O(m, n);
        Vector2 d = H2.B(m.transform.position, n.transform.position, o);

        NL cL = m.cL;
        cL.transform.position = o;
        cL.d = d;


        if (m.rch) if (Vector2.Distance(m.transform.position, m.rch.point) < Vector2.Distance(m.transform.position, o)) D(m, n);
        if (n.rch) if (Vector2.Distance(n.transform.position, n.rch.point) < Vector2.Distance(n.transform.position, o)) D(m, n);
    }

    private void W(NL m, NL n)
    {
        if (MB(m, n))
        {
            m.sL = n;
            n.sL = m;
        }
    }

    private NL MB(NL m, NL n)
    {
        if (CC(m, n)) return null;
        Vector2 o = H2.O(m, n);
        Vector2 d = H2.B(m.transform.position, n.transform.position, o);
        if (m.rch) if (Vector2.Distance(m.transform.position, m.rch.point) < Vector2.Distance(m.transform.position, o)) return null;
        if (n.rch) if (Vector2.Distance(n.transform.position, n.rch.point) < Vector2.Distance(n.transform.position, o)) return null;

        if (m.sO) m.sO.MassDeact();
        if (n.sO) n.sO.MassDeact();
        m.sO = null;
        n.sO = null;

        CS(m);
        CS(n);

        GameObject g = new GameObject("CL", typeof(NL));
        g.transform.parent = transform;
        NL cL = g.GetComponent<NL>();
        cL.transform.position = o;
        cL.d = d;
        cL.lRng = m.lRng > n.lRng ? m.lRng : n.lRng;
        cL.lC = H2.AddColor(m, n);
        cL.lr.material = lM;
        cL.lr.startColor = H2.AddColor(m, n);
        cL.lr.endColor = H2.AddColor(m, n);
        if (!ls.Contains(cL)) ls.Add(cL);

        m.cL = cL;
        n.cL = cL;

        return cL;
    }

    private bool CC(NL m, NL n)
    {
        if (m.cL && m.cL.cL) CC(m.cL.cL, n);
        if (n.cL && n.cL.cL) CC(n.cL.cL, m);
        if ((m.cL && m.cL == n) || (n.cL && n.cL == m)) return true;
        return false;
    }
}
