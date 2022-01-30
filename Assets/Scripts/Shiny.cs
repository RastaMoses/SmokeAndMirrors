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
            while (mr.material.GetFloat("Dissolve") <= 1)
            {
                mr.material.SetFloat("Dissolve", Mathf.Lerp(mr.material.GetFloat("Dissolve"), 1, FindObjectOfType<NLC>().dS));
                yield return null;
            }
        }
        if (GetComponent<Collider2D>() != null)
        {
            col.enabled = true;
        }
        yield return null;

    }

    public IEnumerator IS()
    {
        while (mr.material.GetFloat("Dissolve") >= 0.3)
        {
            mr.material.SetFloat("Dissolve", Mathf.Lerp(mr.material.GetFloat("Dissolve"), 0.3f, FindObjectOfType<NLC>().dS * Time.deltaTime));
            yield return null;
        }
        yield return null;
    }
}
