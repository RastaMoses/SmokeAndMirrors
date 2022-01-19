using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public float transitionTime = 1f;
    public Animator transition;

    public void ReloadLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log(Time.timeScale);
    }

    public void LoadNextLevel()
    {
        Debug.Log("Load Next Scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //StartCoroutine(LoadLevelTransition(SceneManager.GetActiveScene().buildIndex + 1));
    }

    static public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadLevelTransition(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    static public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    static public void QuitGame()
    {
        Application.Quit();
    }
}
