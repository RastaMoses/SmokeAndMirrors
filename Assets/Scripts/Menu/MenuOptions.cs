using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] private GameObject _optionScreen;
    [SerializeField] private GameObject _optionTriggerZone;

    public void ExitGame()
    {
        Debug.Log("Exit");
    }

    public void EnterGame()
    {
        Debug.Log("Enter");
    }

    public void NewGame()
    {
        Debug.Log("New Game");
    }

    public void OpenOptionPanel()
    {
        Debug.Log("Options");
        _optionTriggerZone.SetActive(false);
        _optionScreen.SetActive(true);
        StateManager.Pause();
    }
}
