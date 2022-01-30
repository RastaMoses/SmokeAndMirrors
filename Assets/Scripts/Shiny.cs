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
        Debug.Log("Vis" + name);
        if (GetComponent<MeshRenderer>())
        {
            StopCoroutine("IS");
            StartCoroutine("VS");
            
        }

        if (GetComponent<Collider2D>() != null)
        {
            col.enabled = true;
        }

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
        while (mr.material.GetFloat("DissolveAmt") <= 1)
        {
            mr.material.SetFloat("DissolveAmt", Mathf.Lerp(mr.material.GetFloat("DissolveAmt"), 1, FindObjectOfType<NLC>().dS));
            yield return null;
        }
        yield return null;

    }

    public IEnumerator IS()
    {
        while (mr.material.GetFloat("DissolveAmt") >= 0.3)
        {
            mr.material.SetFloat("DissolveAmt", Mathf.Lerp(mr.material.GetFloat("DissolveAmt"), 0.3f, FindObjectOfType<NLC>().dS));
            yield return null;
        }
        yield return null;
    }
}
