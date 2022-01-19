using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartGameMenuOptionTrigger : MonoBehaviour
{
    public GameObject Spotlight;
    public HoldButton[] OptionButton;

    public UnityEvent[] OnTriggerOption;

    private bool _interactable;
    private string[] _gamePadbuttonKey = { "A", "Y" };

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
            for(int i = 0; i < OptionButton.Length; i++)
            {
                if (OptionButton[i].isActiveAndEnabled)
                {
                    if (OnTriggerOption[i] == null || i > _gamePadbuttonKey.Length - 1)
                        return;

                    if (OptionButton[i].CheckInput(_gamePadbuttonKey[i]))
                        OnTriggerOption[i].Invoke();
                }
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
        OptionButton[0].gameObject.SetActive(interactable);
        if (interactable)
        {
            //if is not first time playing
            OptionButton[1].gameObject.SetActive(interactable);
        }
        else
        {
            OptionButton[1].gameObject.SetActive(interactable);
        }
        _interactable = interactable;
    }
}
