using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject _pauseScreen;

    string pauseButtonKey;

    void Update()
    {
        //check for input to pause
        if (Input.GetButtonDown(pauseButtonKey))
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
        _pauseScreen.SetActive(false);
        StateManager.Pause();
    }
}
