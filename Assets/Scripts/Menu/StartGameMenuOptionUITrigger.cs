using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameMenuOptionUITrigger : MenuOptionUITrigger
{
    protected override void SetButtonsActive(bool interactable)
    {
        OptionButton[0].gameObject.SetActive(interactable);
        if (interactable)
        {
            //this is the "New game" button
            //if is not first time playing
            OptionButton[1].gameObject.SetActive(interactable);
        }
        else
        {
            OptionButton[1].gameObject.SetActive(interactable);
        }
    }
}
