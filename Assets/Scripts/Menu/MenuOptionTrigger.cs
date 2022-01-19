using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuOptionTrigger : MonoBehaviour
{
    public GameObject Spotlight;
    public HoldButton OptionButton;

    public UnityEvent OnTriggerOption;

    private bool _interactable;
    private string _gamePadbuttonKey = "A";

    // Start is called before the first frame update
    void Start()
    {
        SetInteractable(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_interactable)
        {
            if (OptionButton.CheckInput(_gamePadbuttonKey))
            {
                if (OnTriggerOption != null)
                    OnTriggerOption.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        SetInteractable(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        SetInteractable(false);
    }

    private void SetInteractable(bool interactable)
    {
        Spotlight.SetActive(interactable);
        OptionButton.gameObject.SetActive(interactable);
        _interactable = interactable;
    }
}
