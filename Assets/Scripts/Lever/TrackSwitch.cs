using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSwitch : MonoBehaviour
{
    private bool _on; //on is "right", off "left"
    private bool _turnable;
    private string _buttonKey = "Fire1";
    private string _gamePadbuttonKey = "A";

    public List<GameObject> LeftTrackSwitchables;
    public List<GameObject> RightTrackSwitchables;

    public GameObject Button;

    private HoldButton _switchButton;

    private Color32 _offColour = Color.blue;
    private Color32 _onColour = Color.cyan;


    // Start is called before the first frame update
    void Start()
    {
        _switchButton = Button.gameObject.GetComponent<HoldButton>();

        //this code adds the switch to the switchCondition and has to be taken out if 1 switch on is enough to have a light on:
        foreach (GameObject switchable in LeftTrackSwitchables)
        {
            switchable.GetComponent<SwitchCondition>().AddToTarget(1);
        }
        foreach (GameObject switchable in RightTrackSwitchables)
        {
            switchable.GetComponent<SwitchCondition>().AddToTarget(1);
        }

        //turn "right" side on
        if (_on)
        {
            foreach (GameObject switchable in RightTrackSwitchables)
            {
                switchable.GetComponent<SwitchCondition>().AddOneTowardsTarget();
            }
        }
        //turn "left" side on
        else
        {
            foreach (GameObject switchable in LeftTrackSwitchables)
            {
                switchable.GetComponent<SwitchCondition>().AddOneTowardsTarget();
            }
        }

        Button.SetActive(false);

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
            if (_switchButton.CheckInput(_buttonKey) || _switchButton.CheckInput(_gamePadbuttonKey))
            {
                _on = !_on;
                //turn "right" side on and "left" off
                if (_on)
                {
                    foreach (GameObject switchable in RightTrackSwitchables)
                    {
                        switchable.GetComponent<SwitchCondition>().AddOneTowardsTarget();
                    }
                    foreach (GameObject switchable in LeftTrackSwitchables)
                    {
                        switchable.GetComponent<SwitchCondition>().RemoveOneTowardsTarget();
                    }
                }
                //turn "left" side on and "right" off
                else
                {
                    foreach (GameObject switchable in LeftTrackSwitchables)
                    {
                        switchable.GetComponent<SwitchCondition>().AddOneTowardsTarget();
                    }
                    foreach (GameObject switchable in RightTrackSwitchables)
                    {
                        switchable.GetComponent<SwitchCondition>().RemoveOneTowardsTarget();
                    }
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
        Button.SetActive(true);
        //check for input
        _turnable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        //stop show button to use
        Button.SetActive(false);
        //stop check for input
        _turnable = false;
    }
}
