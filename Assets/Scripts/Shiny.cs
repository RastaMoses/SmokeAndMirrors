using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiny : MonoBehaviour
{
    MeshRenderer mr;
    // [SerializeField] Material invis;
    // [SerializeField] Material vis;
    [SerializeField] Material mat;
    Collider2D col;

    void Awake()
    {
        if (GetComponent<MeshRenderer>())
        {
            mr = GetComponent<MeshRenderer>();
            mr.material = mat;
        }

        if (GetComponent<Collider2D>() != null)
        {
            col = GetComponent<Collider2D>();
        }

        Invisibilize();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Visibilize()
    {
        StopCoroutine("IS");
        StartCoroutine("VS");

        


    }

    public void Invisibilize()
    {
        if (GetComponent<MeshRenderer>())
        {
            StopCoroutine("VS");
            StartCoroutine("IS");
        }
        if (GetComponent<Collider2D>() != null)
        {
            col.enabled = false;
        }
    }

    public IEnumerator VS()
    {
        if (GetComponent<MeshRenderer>())
        {
            while (mr.material.GetFloat("DissolveAmt") < 1)
            {
                mr.material.SetFloat("DissolveAmt", Mathf.Lerp(mr.material.GetFloat("DissolveAmt"), 1, FindObjectOfType<NLC>().dS * Time.deltaTime));
                if (mr.material.GetFloat("DissolveAmt") > 0.9f)
                {
                    mr.material.SetFloat("DissolveAmt", 1);
                }
                yield return null;
            }
            if (GetComponent<Collider2D>() != null)
            {
                col.enabled = true;
            }
        }
        else
        {
            if (GetComponent<Collider2D>() != null)
            {
                col.enabled = true;
            }
        }
        
        yield return null;

    }

    public IEnumerator IS()
    {
        while (mr.material.GetFloat("DissolveAmt") >= 0.2f)
        {
            mr.material.SetFloat("DissolveAmt", Mathf.Lerp(mr.material.GetFloat("DissolveAmt"), 0.2f, FindObjectOfType<NLC>().dS * Time.deltaTime));
            yield return null;
        }
        yield return null;
    }
}
