using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] public Transform respawnPoint;
    public float transitionTime = 3f;
    [SerializeField] Animator curtainOpen;
    [SerializeField] Animator curtainClose;
    [SerializeField] bool playCurtain;
    public int lastLevel;

    SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader = GetComponent<SceneLoader>();

    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("HighestLevel") <= lastLevel) PlayerPrefs.SetInt("HighestLevel", lastLevel);
        //Set Player Startposition
        FindObjectOfType<PlayerController>().transform.position = respawnPoint.position;

        if (playCurtain)
        {
            StartCoroutine(CurtainOpen());
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
        if (playCurtain)
        {
            StartCoroutine(CurtainClose());
        }
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
        yield return new WaitForSeconds(transitionTime);
        sceneLoader.LoadNextLevel();
    }
}
