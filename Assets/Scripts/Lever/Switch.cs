using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private bool _on;
    private KeyCode _buttonCode = KeyCode.L;
    private bool _turnable;

    public List<GameObject> Switchables;
    public GameObject ButtonIndicatorUI;

    private Color32 _offColour = Color.red;
    private Color32 _onColour = Color.green;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject switchable in Switchables)
        {
            switchable.GetComponent<SwitchCondition>().AddToTarget(1);
        }
        ButtonIndicatorUI.SetActive(false);

        SetVisualKeyForState();
    }

    private void SetVisualKeyForState()
    {
        if (_on)
        {
            GetComponent<SpriteRenderer>().color = _onColour;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = _offColour;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_turnable)
        {
            if (Input.GetKeyDown(_buttonCode))
            {
                _on = !_on;
                //foreach switchable, notify 1 switch on/off
                foreach(GameObject switchable in Switchables)
                {
                    if(_on)
                        switchable.GetComponent<SwitchCondition>().AddOneTowardsTarget();
                    else
                        switchable.GetComponent<SwitchCondition>().RemoveOneTowardsTarget();
                }
                SetVisualKeyForState();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        //show button to use
        ButtonIndicatorUI.SetActive(true);
        //get key information 
        _turnable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        //stop show button to use
        ButtonIndicatorUI.SetActive(false);
        //stop get key information 
        _turnable = false;
    }
}
