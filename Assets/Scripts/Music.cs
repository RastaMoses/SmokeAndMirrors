using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music instance;
    public static bool isMusicPlaying;
    private void Awake()
    {
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
}
