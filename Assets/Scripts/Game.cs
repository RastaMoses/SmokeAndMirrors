using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] public Transform respawnPoint;
    public float transitionTime = 3f;
    [SerializeField] Animator curtainOpen;
    [SerializeField] Animator curtainClose;
    [SerializeField] bool playCurtain;
    [SerializeField] public bool unlockedMagic;
    [SerializeField] public bool unlockedSwap;
    [SerializeField] public UnityEngine.Audio.AudioMixer am;
    [Range(0, 1)] [SerializeField] public float sV;
    [Range(0, 1)] [SerializeField] public float mV;



    SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader = GetComponent<SceneLoader>();
        PlayerPrefs.SetFloat("musicVolume", mV);
        PlayerPrefs.SetFloat("sfxVolume", sV);

    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("HighestLevel") <= SceneManager.GetActiveScene().buildIndex) PlayerPrefs.SetInt("HighestLevel", SceneManager.GetActiveScene().buildIndex);
        //Set Player Startposition
        FindObjectOfType<PlayerController>().transform.position = respawnPoint.position;

        if (playCurtain && curtainOpen != null)
        {
            StartCoroutine(CurtainOpen());
        }
        if (unlockedMagic)
        {
            FindObjectOfType<PlayerController>().gameObject.GetComponent<Animator>().SetFloat("magic", 1);
        }
        else
        {

            FindObjectOfType<PlayerController>().gameObject.GetComponent<Animator>().SetFloat("magic", 0);
        }
    }


    // Update is called once per frame
    void Update()
    {
        am.SetFloat("SFX", PlayerPrefs.GetFloat("sfxVolume"));
        am.SetFloat("Music", PlayerPrefs.GetFloat("musicVolume"));

    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("HighestLevel", 1);
    }



    public void LevelComplete()
    {
        if (playCurtain)
        {
            StartCoroutine(CurtainClose());
        }
        else GetComponent<SceneLoader>().LoadNextLevel();
    }

    public IEnumerator CurtainOpen()
    {
        curtainOpen.gameObject.SetActive(true);
        FindObjectOfType<PlayerController>().movementEnabled = false;
        curtainOpen.SetTrigger("curtainOpen");
        yield return new WaitForSeconds(transitionTime);
        FindObjectOfType<PlayerController>().movementEnabled = true;
        curtainOpen.gameObject.SetActive(false);

    }
    public IEnumerator CurtainClose()
    {
        curtainClose.gameObject.SetActive(true);
        FindObjectOfType<PlayerController>().movementEnabled = false;
        curtainClose.SetTrigger("curtainClose");
        yield return new WaitForSeconds(transitionTime + 2);
        GetComponent<SceneLoader>().LoadNextLevel();
    }

    public IEnumerator PlayCurtainAndLevel(int bS)
    {
        StartCoroutine(CurtainClose());
        yield return new WaitForSeconds(transitionTime+2);
        SceneLoader.LoadLevel(bS);
    }
}
