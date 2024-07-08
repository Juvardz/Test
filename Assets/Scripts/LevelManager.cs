using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private int currentLives = 3;
    private int currentScore = 0;

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
        if (UIController.Instance != null)
        {
            UIController.Instance.UpdateLives(currentLives);
            UIController.Instance.UpdateScore(currentScore); // Actualiza el puntaje en la UI
        }
    }

    private void LoadLevel(int levelNo)
    {
        SceneManager.LoadScene(levelNo);
        if (UIController.Instance != null)
        {
            UIController.Instance.UpdateScore(currentScore); // Actualiza el puntaje en la UI
        }
    }

    public void FirstLevel()
    {
        LoadLevel(1); // Assuming 1 is the index for the first level
    }

    public void WelcomeLevel()
    {
        LoadLevel(0); // Assuming 0 is the index for the welcome scene
    }

    public void BossLevel()
    {
        LoadLevel(2); // Assuming 2 is the index for the boss level
    }

    public void GameWinnerLevel()
    {
        LoadLevel(3); // Assuming 3 is the index for the game winner scene
    }

    public void GameOverLevel()
    {
        LoadLevel(4); // Assuming 4 is the index for the game over scene
    }

    public void IncreaseScore(int points)
    {
        currentScore += points;
        if (UIController.Instance != null)
        {
            UIController.Instance.UpdateScore(currentScore);
        }

        if (currentScore >= 50 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            BossLevel();
        }
    }

    public void DecreaseLives()
    {
        currentLives--;
        if (currentLives <= 0)
        {
            GameOverLevel();
        }
        else
        {
            Reload();
        }
    }

    public void ResetLives()
    {
        currentLives = 3;
    }

    public int GetLives()
    {
        return currentLives;
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
