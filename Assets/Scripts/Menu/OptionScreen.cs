using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionScreen : MonoBehaviour
{
    [SerializeField] private GameObject _optionScreen;

    // Start is called before the first frame update
    void Start()
    {
        _optionScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        StateManager.Pause();
        _optionScreen.SetActive(false);
    }
}
