using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObj : MonoBehaviour
{
    public float DotToPlayer;
    public GameObject SelectionIndicator;
    public bool Selected = false;

    public float Det;
    public float Angle;


    // Start is called before the first frame update
    void Start()
    {
        SelectionIndicator.SetActive(false);
    }

    public void Select()
    {
        Selected = true;
        SelectionIndicator.SetActive(true);
    }

    public void DeSelect()
    {
        Selected = false;
        SelectionIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
