using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music instance;
    public static bool isMusicPlaying;
    [SerializeField] public UnityEngine.Audio.AudioMixer am;
    [Range(0, 1)] [SerializeField] public float sV;
    [Range(0, 1)] [SerializeField] public float mV;
    private void Awake()
    {
        PlayerPrefs.SetFloat("sfxVolume", CustomSlider.Map(0,1,-80,0,sV));
        PlayerPrefs.SetFloat("musicVolume", CustomSlider.Map(0, 1, -80, 0, mV));
        int gameObjectCount = FindObjectsOfType<Music>().Length;
        if(instance != null && instance != this)
        {
            
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(instance.gameObject);

    }
    private void Start()
    {
        

        if (!isMusicPlaying)
        {
            GetComponent<AudioSource>().Play();
            isMusicPlaying = true;
        }
    }

    private void Update()
    {
        am.SetFloat("SFX", PlayerPrefs.GetFloat("sfxVolume"));
        am.SetFloat("Music", PlayerPrefs.GetFloat("musicVolume"));
    }
}
