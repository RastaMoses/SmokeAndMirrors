using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] private GameObject _optionScreen;
    [SerializeField] private GameObject _mainMenuScreen;

    [SerializeField] private GameObject _optionTriggerZone;

    public void ExitGame()
    {
        Debug.Log("Exit");
        SceneLoader.QuitGame();
        #if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void EnterGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("HighestLevel"));
        Debug.Log("Enter");
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("HighestLevel", 1);
        // put 1 here if level build indxes are in sequences and 0 is main menu
        SceneManager.LoadScene(1);
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
