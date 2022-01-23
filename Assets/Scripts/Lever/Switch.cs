using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private bool _onAtStart;
    private bool _on;
    private bool _turnable;
    private string _buttonKey = "Fire1";
    private string _gamePadbuttonKey = "A";

    [SerializeField] private List<SwitchCondition> _switchables;
    [SerializeField] HoldButton _switchButton;

    [SerializeField] private GameObject _handle;
    private Vector3 _handleTargetRotation = new Vector3(0,0,30);
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
        foreach (SwitchCondition switchable in _switchables)
        {
            if (_on)
                switchable.AddOneTowardsTarget();
        }

        _switchButton.gameObject.SetActive(false);

        SetVisualKeyForState(true);
    }

    private void SetVisualKeyForState(bool immediate = false)
    {
        if (immediate)
        {
            _handle.transform.rotation = _on ? Quaternion.Euler(_handleTargetRotation) : Quaternion.Euler(-_handleTargetRotation);
        }
        else
        {
            StartCoroutine(TurnHandleAnimation());
        }
    }

    IEnumerator TurnHandleAnimation()
    {
        yield return new WaitForSeconds(0.4f); //wait until animation is at the handle pulling/pushing point
        if (_on)
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
    }

    // Update is called once per frame
    void Update()
    {
        if (_turnable)
        {
            if (_switchButton.CheckInput(_buttonKey) || _switchButton.CheckInput(_gamePadbuttonKey))
            {
                //Animation Set Trigger
                FindObjectOfType<PlayerMovement>().SetLeverPulling(transform.position.x);
                sfx.Lever();

                _on = !_on;
                //foreach switchable, notify 1 switch on/off
                foreach (SwitchCondition switchable in _switchables)
                {
                    if (_on)
                        switchable.AddOneTowardsTarget();
                    else
                        switchable.RemoveOneTowardsTarget();
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
        _switchButton.gameObject.SetActive(true);
        //check for input
        _turnable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        //stop show button to use
        _switchButton.gameObject.SetActive(false);
        //stop check for input
        _turnable = false;
    }
}
