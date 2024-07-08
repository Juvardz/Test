using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreTextbox;

    [SerializeField]
    Transform livesContainer;

    [SerializeField]
    float gameOverSleep;

    private static UIController _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static UIController Instance
    {
        get { return _instance; }
    }

#region Score
    public void IncreaseScore(float points)
    {
        float score = float.Parse(scoreTextbox.text);
        score += points;
        scoreTextbox.text = score.ToString();
    }

    public void UpdateScore(int score)
    {
        scoreTextbox.text = score.ToString();
    }
#endregion

#region Lives
    public void UpdateLives(int lives)
    {
        Image[] liveImages = livesContainer.GetComponentsInChildren<Image>();
        for (int i = 0; i < liveImages.Length; i++)
        {
            liveImages[i].enabled = i < lives;
        }
    }

    public void DecreaseLives()
    {
        LevelManager.Instance.DecreaseLives();
    }

    public bool HasLives()
    {
        return LevelManager.Instance.GetLives() > 0;
    }
#endregion
    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(gameOverSleep);
        LevelManager.Instance.GameOverLevel();
    }

    public void GameOver()
    {
        StartCoroutine(EndGame());
    }
}
