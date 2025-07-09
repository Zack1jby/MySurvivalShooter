using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public static int score;
    public static int highScore;
    public TextMeshProUGUI text;
    public TextMeshProUGUI textHighScore;

    private void Awake()
    {
        Instance = this;
        score = 0;
        LoadGameState();
    }

    public void SaveGameState()
    {
        // Save variable
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    public void LoadGameState()
    {
        // Load variable
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
            textHighScore.text = "High Score: " + highScore;

        }
        else
        {
            highScore = 0;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
    
    public void ShowScore()
    {
        text.text = "Score: " + score;
        if(score > highScore)
        {
            highScore = score;
            textHighScore.text = "High Score: " + highScore;
            SaveGameState();
        }
    }
}
