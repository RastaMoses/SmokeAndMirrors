using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOptionUITrigger : MonoBehaviour
{
    public GameObject Spotlight;
    public UIOverlayHoldButton[] OptionButton;

    private bool _interactable;

    // Start is called before the first frame update
    void Start()
    {
        SetInteractable(false);
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
        SetButtonsActive(interactable);
        _interactable = interactable;
    }

    protected virtual void SetButtonsActive(bool interactable)
    {
        for (int i = 0; i < OptionButton.Length; i++)
        {
            OptionButton[i].gameObject.SetActive(interactable);
        }
    }
}
