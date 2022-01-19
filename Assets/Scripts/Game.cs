using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] public Transform respawnPoint;

    SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader = GetComponent<SceneLoader>();
    }
    private void Start()
    {
        //Set Player Startposition
        FindObjectOfType<PlayerController>().transform.position = respawnPoint.position;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void LevelComplete()
    {
        sceneLoader.LoadNextLevel();
    }
}
