using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] private GameObject _optionScreen;
    [SerializeField] private GameObject _mainMenuScreen;

    [SerializeField] private GameObject _exitTriggerZone;
    [SerializeField] private GameObject _optionTriggerZone;
    [SerializeField] private GameObject _enterTriggerZone;

    private void ActivateMenuOptions(bool activate)
    {
        _exitTriggerZone.gameObject.SetActive(activate);
        _optionTriggerZone.gameObject.SetActive(activate);
        _enterTriggerZone.gameObject.SetActive(activate);
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        ActivateMenuOptions(false);
        StartCoroutine(FindObjectOfType<Game>().QuitGame());
    }

    public void EnterGame()
    {
        Debug.Log("Enter");
        ActivateMenuOptions(false);
        StartCoroutine(FindObjectOfType<Game>().PlayCurtainAndLevel(Mathf.Max(PlayerPrefs.GetInt("HighestLevel"), 1)));
    }

    public void NewGame()
    {
        ActivateMenuOptions(false);
        PlayerPrefs.SetInt("HighestLevel", 1);
        // put 1 here if level build indxes are in sequences and 0 is main menu
        StartCoroutine(FindObjectOfType<Game>().PlayCurtainAndLevel(1));
        Debug.Log("New Game");
    }

    public void OpenOptionPanel()
    {
        Debug.Log("Options");
        _optionScreen.SetActive(true);
        // _mainMenuScreen.SetActive(false);
        _optionTriggerZone.SetActive(false);
        StateManager.Pause();
    }
}
