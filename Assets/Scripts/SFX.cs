using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    //Serialize Parameters

    [Header("Player")]
    [SerializeField] List<AudioClip> footsteps;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip magicMode;
    [SerializeField] AudioClip landing;

    [Header("Interactables")]
    [SerializeField] AudioClip lever;
    [SerializeField] AudioClip rails;
    [SerializeField] AudioClip railsEndHit;
    [SerializeField] AudioClip shinies;

    [Header("Lights")]
    [SerializeField] AudioClip lightsOn;
    [SerializeField] AudioClip lightsOff;
    [SerializeField] AudioClip rotating;
    [SerializeField] AudioClip selectBulb;
    [SerializeField] AudioClip switchBulb;

    //State

    bool pausedRails;
    bool pausedRotating;


    //Cached Component Configure
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    #region Player
    public void Footsteps()
    {
        AudioClip clip = footsteps[Random.Range(0,footsteps.Count -1)];
        audioSource.PlayOneShot(clip);
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
        audioSource.Play();
    }
    public void PauseRails()
    {
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
        audioSource.Play();
    }
    public void PauseRotating()
    {
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
