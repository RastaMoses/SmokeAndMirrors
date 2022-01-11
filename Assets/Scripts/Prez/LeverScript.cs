using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public List<GameObject> objs;
    public List<MouseRotate> mr;
    public Transform player;
    public bool leverSwitch;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in objs)
        {
            obj.SetActive(leverSwitch);
        }

        foreach (MouseRotate m in mr)
        {
            m.enabled = leverSwitch;
        }

        if (Vector2.Distance(player.position, transform.position) < 2f)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                leverSwitch = !leverSwitch;
            }
        }
    }
}
