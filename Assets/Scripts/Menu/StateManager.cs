using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static bool GamePaused { get; private set; }

    public static PlayerController _playerController;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    public static void Pause()
    {
        GamePaused = !GamePaused;
        if (GamePaused)
        {
            Time.timeScale = 0f;
            _playerController.Freeze(GamePaused);
        }
        else{
            Time.timeScale = 1f;
            _playerController.Freeze(GamePaused);
        }
    }
}
