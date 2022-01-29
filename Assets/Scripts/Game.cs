using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] public Transform respawnPoint;
    public float transitionTime = 5f;
    [SerializeField] Animator curtainOpen;
    [SerializeField] Animator curtainClose;

    SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader = GetComponent<SceneLoader>();
    }
    private void Start()
    {
        //Set Player Startposition
        FindObjectOfType<PlayerController>().transform.position = respawnPoint.position;

        StartCoroutine(CurtainOpen());
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void LevelComplete()
    {
        StartCoroutine(CurtainClose());
    }

    public IEnumerator CurtainOpen()
    {
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
