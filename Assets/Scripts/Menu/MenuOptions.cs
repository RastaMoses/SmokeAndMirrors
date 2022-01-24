using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] private GameObject _optionScreen;

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
        _optionScreen.SetActive(true);
        StateManager.Pause();
    }
}
