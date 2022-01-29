using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionScreen : MonoBehaviour
{
    [SerializeField] private GameObject _optionScreen;
    [SerializeField] private GameObject _optionTriggerZone;
    [SerializeField] private Slider _sfxVolume;
    [SerializeField] private Slider _musicVolume;
    

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
        _optionTriggerZone.SetActive(true);
        _optionScreen.SetActive(false);
        PlayerPrefs.SetFloat("sfxVolume", _sfxVolume.value);
        PlayerPrefs.SetFloat("musicVolume", _musicVolume.value);

    }
}
