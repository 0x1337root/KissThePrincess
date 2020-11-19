using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text gameOverText;

    [SerializeField]
    private Text victoryText;

    [SerializeField]
    private Text LevelText;

    public bool victory = false;

    public bool gameOver = false;

    private int score;

    Scene mScene;

    private void Start() {

        LevelText.text = SceneManager.GetActiveScene().name;
    }

    public void AddScore(int amount)
    {
        score += amount;

        scoreText.text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = "Game Over!";
    }

    public void Victory()
    {
        victory = true;
        victoryText.text = "VICTORY";
    }
}