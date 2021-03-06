using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSwitch : MonoBehaviour
{
    [SerializeField] private bool _onAtStart;
    private bool _on; //on is "right", off "left"
    private bool _playerInReach;
    private bool _turnable;
    private string _buttonKey = "Fire1";
    private string _gamePadbuttonKey = "A";

    [SerializeField] private List<SwitchCondition> _leftTrackSwitchables;
    [SerializeField] private List<SwitchCondition> _rightTrackSwitchables;
    [SerializeField] private GameObject _leverOutline;
    List<GameObject> _leftSwitchableOutlines = new List<GameObject>();
    List<GameObject> _rightSwitchableOutlines = new List<GameObject>();
    //[SerializeField] private List<GameObject> _leftOuts;
    //[SerializeField] private List<GameObject> _rightOuts;
    [SerializeField] private Material _leftOutlineMaterial;
    [SerializeField] private Material _rightOutlineMaterial;

    [SerializeField] private AnimatedHoldButton _switchButton;

    [SerializeField] private GameObject _handle;
    private Vector3 _handleTargetRotation = new Vector3(0, 0, 30);
    private float _turnSpeed = 150;

    //Cached Comp Configuration
    SFX sfx;

    private void Awake()
    {
        sfx = GetComponent<SFX>();
    }


    // Start is called before the first frame update
    void Start()
    {
        _on = _onAtStart;

        if (_on)
        {
            //turn "right" side on
            foreach (SwitchCondition switchable in _rightTrackSwitchables)
            {
                switchable.AddOneTowardsTarget();
            }
        }
        else
        {
            //turn "left" side on
            foreach (SwitchCondition switchable in _leftTrackSwitchables)
            {
                switchable.AddOneTowardsTarget();
            }
        }

        _switchButton.gameObject.SetActive(false);
        foreach (SwitchCondition switchable in _leftTrackSwitchables)
        {
            foreach (Transform child in switchable.transform)
            {
                if (child.CompareTag("Outline"))
                    _leftSwitchableOutlines.Add(child.gameObject);
            }
        }
        foreach (SwitchCondition switchable in _rightTrackSwitchables)
        {
            foreach (Transform child in switchable.transform)
            {
                if (child.CompareTag("Outline"))
                    _rightSwitchableOutlines.Add(child.gameObject);
            }
        }
        ActivateOutlines(false);


        _handle.transform.rotation = _on ? Quaternion.Euler(_handleTargetRotation) : Quaternion.Euler(-_handleTargetRotation);
    }


    private void ActivateOutlines(bool activate)
    {
        if (activate)
        {
            if (_on)
            {
                _leverOutline.GetComponent<MeshRenderer>().material = _rightOutlineMaterial;
                _leverOutline.SetActive(activate);
                foreach (GameObject g in _rightSwitchableOutlines)
                {
                    g.GetComponent<MeshRenderer>().material = _rightOutlineMaterial;
                    g.SetActive(activate);
                }
            }
            else
            {
                _leverOutline.GetComponent<MeshRenderer>().material = _leftOutlineMaterial;
                _leverOutline.SetActive(activate);
                foreach (GameObject g in _leftSwitchableOutlines)
                {
                    g.GetComponent<MeshRenderer>().material = _leftOutlineMaterial;
                    g.SetActive(activate);
                }
            }
               
            
        }
        else
        {
            _leverOutline.SetActive(activate);
            foreach (GameObject g in _leftSwitchableOutlines)
            {
                g.SetActive(activate);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerInReach)
        {
            if (_switchButton.CheckInput(_buttonKey) || _switchButton.CheckInput(_gamePadbuttonKey))
            {
                //Animation Set Trigger
                FindObjectOfType<PlayerMovement>().SetLeverPulling(transform.position.x);
                sfx.Lever();

                _on = !_on;

                PerformSwitch();
            }
        }
    }

    private void PerformSwitch(bool immediate = false)
    {
        if (immediate)
        {
            _handle.transform.rotation = _on ? Quaternion.Euler(_handleTargetRotation) : Quaternion.Euler(-_handleTargetRotation);
            TriggerSwitchInSwitchables();
        }
        else
        {
            SetTurnable(false);
            StartCoroutine(TurnHandleAnimation());
        }
    }

    public void SwitchSides()
    {
        _on = !_on;
        TriggerSwitchInSwitchables();
    }

    private void TriggerSwitchInSwitchables()
    {
        //turn "right" side on and "left" off
        if (_on)
        {
            foreach (SwitchCondition switchable in _rightTrackSwitchables)
            {
                switchable.AddOneTowardsTarget();
            }
            foreach (SwitchCondition switchable in _leftTrackSwitchables)
            {
                switchable.RemoveOneTowardsTarget();
            }
        }
        //turn "left" side on and "right" off
        else
        {
            foreach (SwitchCondition switchable in _leftTrackSwitchables)
            {
                switchable.AddOneTowardsTarget();
            }
            foreach (SwitchCondition switchable in _rightTrackSwitchables)
            {
                switchable.RemoveOneTowardsTarget();
            }
        }
    }

    private void SetTurnable(bool turnable)
    {
        _turnable = turnable;
        if (!_turnable)
        {
            _switchButton.Deactivate();
        }
        else if (_playerInReach)
        {
            _switchButton.Activate(_gamePadbuttonKey);
        }
    }

    IEnumerator TurnHandleAnimation()
    {
        yield return new WaitForSeconds(0.4f); //wait until animation is at the handle pulling/pushing point
        
        if(_handle.transform.rotation != Quaternion.Euler(_handleTargetRotation))
        {
            while (_handle.transform.rotation != Quaternion.Euler(_handleTargetRotation))
            {
                //turn the switch handle on the "On" side
                _handle.transform.rotation = Quaternion.RotateTowards(_handle.transform.rotation, Quaternion.Euler(_handleTargetRotation), _turnSpeed * Time.deltaTime);
                yield return null;
            }
        }
        else
        {
            while (_handle.transform.rotation != Quaternion.Euler(-_handleTargetRotation))
            {
                //turn the switch handle on the "Off" side
                _handle.transform.rotation = Quaternion.RotateTowards(_handle.transform.rotation, Quaternion.Euler(-_handleTargetRotation), _turnSpeed * Time.deltaTime);
                yield return null;
            }
        }
        
        //if (_on)
        //{
        //    while (_handle.transform.rotation != Quaternion.Euler(_handleTargetRotation))
        //    {
        //        //turn the switch handle on the "On" side
        //        _handle.transform.rotation = Quaternion.RotateTowards(_handle.transform.rotation, Quaternion.Euler(_handleTargetRotation), _turnSpeed * Time.deltaTime);
        //        yield return null;
        //    }
        //}
        //else
        //{
        //    while (_handle.transform.rotation != Quaternion.Euler(-_handleTargetRotation))
        //    {
        //        //turn the switch handle on the "Off" side
        //        _handle.transform.rotation = Quaternion.RotateTowards(_handle.transform.rotation, Quaternion.Euler(-_handleTargetRotation), _turnSpeed * Time.deltaTime);
        //        yield return null;
        //    }
        //}
        TriggerSwitchInSwitchables();
        yield return new WaitForSeconds(0.4f); //wait until player pull animation is finished
        SetTurnable(true);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        //show button to use
        _switchButton.gameObject.SetActive(true);
        ActivateOutlines(false);
        //check for input
        _playerInReach = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (FindObjectOfType<SelectableObjController>()._inSelectionMode) _leverOutline.SetActive(false);

        if (!FindObjectOfType<SelectableObjController>()._inSelectionMode) ActivateOutlines(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        //stop show button to use
        _switchButton.gameObject.SetActive(false);
        ActivateOutlines(false);
        //stop check for input
        _playerInReach = false;
    }
}
