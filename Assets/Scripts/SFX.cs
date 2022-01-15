using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    //Serialize Parameters

    [Header("Player")]
    [SerializeField] List<AudioClip> footsteps;
    [SerializeField] [Range(0, 1f)] float footstepsVolume = 0.06f;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip magicMode;
    [SerializeField] AudioClip landing;

    [Header("Interactables")]
    [SerializeField] AudioClip lever;
    [SerializeField] AudioClip rails;
    [SerializeField] float railsFadeTime = 0.2f;
    [SerializeField] AudioClip railsEndHit;
    [SerializeField] AudioClip shinies;

    [Header("Lights")]
    [SerializeField] AudioClip lightsOn;
    [SerializeField] AudioClip lightsOff;
    [SerializeField] AudioClip rotating;
    [SerializeField] float rotatingFadeTime = 0.2f;
    [SerializeField] AudioClip selectBulb;
    [SerializeField] AudioClip switchBulb;

    //State

    bool pausedRails;
    bool pausedRotating;
    float startVolume;


    //Cached Component Configure
    AudioSource audioSource;
    Coroutine railsCoroutine;
    Coroutine rotatingCoroutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        startVolume = audioSource.volume;
    }


    #region Player
    public void Footsteps()
    {
        AudioClip clip = footsteps[Random.Range(0,footsteps.Count -1)];
        audioSource.PlayOneShot(clip, footstepsVolume);
    }

    public void Jump()
    {
        audioSource.clip = jump;
        audioSource.Play();
    }
    public void MagicMode()
    {
        audioSource.clip = magicMode;
        audioSource.Play();
    }
    public void Landing()
    {
        audioSource.clip = landing;
        audioSource.Play();
    }
    #endregion

    #region Interactables
    public void Lever()
    {
        audioSource.clip = lever;
        audioSource.Play();
    }
    public void PlayRails()
    {
        if (!pausedRails)
        {
            audioSource.clip = rails;
        }
        if (!audioSource.isPlaying)
        {
            railsCoroutine = StartCoroutine(RailsFadeIn());
            audioSource.Play();
        }
    }

    IEnumerator RailsFadeIn()
    {
        float currentTime = 0;
        float start = 0;

        while(currentTime < railsFadeTime)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, startVolume, currentTime / railsFadeTime);
            yield return null;
        }
        yield break;
    }

    public void PauseRails()
    {
        StopCoroutine(railsCoroutine);
        audioSource.Pause();
        pausedRails = true;
    }
    public void RailsEndHit()
    {
        audioSource.clip = railsEndHit;
        audioSource.Play();
    }
    public void Shinies()
    {
        audioSource.clip = shinies;
        audioSource.Play();
    }



    #endregion

    #region Lights

    public void LightsOn()
    {
        audioSource.clip = lightsOn;
        audioSource.Play();
    }
    public void LightsOff()
    {
        audioSource.clip = lightsOff;
        audioSource.Play();
    }
    public void PlayRotating()
    {
        if (!pausedRotating)
        {
            audioSource.clip = rotating;
        }
        if (!audioSource.isPlaying)
        {
            rotatingCoroutine = StartCoroutine(RotatingFadeIn());
            audioSource.Play();
        }
    }
    IEnumerator RotatingFadeIn()
    {
        float currentTime = 0;
        float start = 0;

        while (currentTime < rotatingFadeTime)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, startVolume, currentTime / rotatingFadeTime);
            yield return null;
        }
        yield break;
    }
    public void PauseRotating()
    {
        StopCoroutine(rotatingCoroutine);
        audioSource.Pause();
        pausedRotating = true;
    }
    public void SelectBulb()
    {
        audioSource.clip = selectBulb;
        audioSource.Play();
    }
    public void SwitchBulb()
    {
        audioSource.clip = switchBulb;
        audioSource.Play();
    }
    #endregion
}
