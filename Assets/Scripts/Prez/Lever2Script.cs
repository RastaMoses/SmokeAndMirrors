using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever2Script : MonoBehaviour
{
    [SerializeField] Material leverOnMat;
    [SerializeField] Material leverOffMat;
    public bool leverSwitch;
    public GameObject teleporter;
    public GameObject enableObj;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.Euler(0,0,leverSwitch ? -130 : -40);
        teleporter.transform.rotation = rotation;

        enableObj.GetComponent<LightScript>().enabled = leverSwitch;
        enableObj.GetComponent<LineRenderer>().enabled = leverSwitch;
        if (Vector2.Distance(player.position, transform.position) < 2f)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                leverSwitch = !leverSwitch;
            }
        }
        if (leverSwitch)
        {
            GetComponent<MeshRenderer>().material = leverOnMat;
        }
        else
        {
            GetComponent<MeshRenderer>().material = leverOffMat;
        }
    }
}
