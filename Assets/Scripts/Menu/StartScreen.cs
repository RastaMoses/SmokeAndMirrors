using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public GameObject[] MenuOptions;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject menuOption in MenuOptions)
        {
            menuOption.SetActive(false);
        }
        StateManager.Pause();
    }

    public void EnterStartMenu()
    {
        Debug.Log("Start");

        foreach (GameObject menuOption in MenuOptions)
        {
            menuOption.SetActive(true);
        }
        StateManager.Pause();
        gameObject.SetActive(false);
    }
}
