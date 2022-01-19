using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject _pauseScreen;

    private string _pauseButtonKey = "Start";

    void Start()
    {
        _pauseScreen.SetActive(false);
    }

    void Update()
    {
        //check for input to pause
        if (Input.GetButtonDown(_pauseButtonKey))
        {
            if (StateManager.GamePaused)
                return;

            //Open pause screen
            _pauseScreen.SetActive(true);
            StateManager.Pause();
        }
    }

    public void Unpause()
    {
        StateManager.Pause();
        _pauseScreen.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("return to main");
        //SceneLoader.LoadMainMenu();
    }
}
