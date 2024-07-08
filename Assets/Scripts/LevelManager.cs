using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadLevel(int levelNo)
    {
        SceneManager.LoadScene(levelNo);
    }

    public void FirstLevel()
    {
        int levelNo = 0;
        LoadLevel(levelNo);
    }

    public void LastLevel()
    {
        int levelNo = SceneManager.sceneCountInBuildSettings - 1;
        LoadLevel(levelNo);
    }

    public void NextLevel()
    {
        int levelNo = SceneManager.GetActiveScene().buildIndex + 1;
        if (levelNo > SceneManager.sceneCountInBuildSettings - 1)
        {
            levelNo = SceneManager.sceneCountInBuildSettings - 1;
        }
        LoadLevel(levelNo);
    }

    public void PreviousLevel()
    {
        int levelNo = SceneManager.GetActiveScene().buildIndex - 1;
        if (levelNo < 0)
        {
            levelNo = 0;
        }
        LoadLevel(levelNo);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
