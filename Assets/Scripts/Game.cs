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
    [Header("Inputs")]
    [SerializeField] bool creditsLevel;
    [SerializeField] bool playCurtain;
    [SerializeField] public bool unlockedMagic;
    [SerializeField] public bool unlockedSwap;


   

    SceneLoader sceneLoader;

    private void Awake()
    {

        sceneLoader = GetComponent<SceneLoader>();
        

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
        
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("HighestLevel", 1);
    }



    public void LevelComplete()
    {
        if (creditsLevel)
        {
            StartCoroutine(PlayCurtainAndLevel(0));
        }
        else if (playCurtain)
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

    public IEnumerator QuitGame()
    {
        curtainClose.gameObject.SetActive(true);
        FindObjectOfType<PlayerController>().movementEnabled = false;
        curtainClose.SetTrigger("curtainClose");
        yield return new WaitForSeconds(transitionTime + 2);
        SceneLoader.QuitGame();
        #if UNITY_EDITOR
             UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
