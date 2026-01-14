using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject highScorePanel;

    [Header("GameOver UI")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_InputField nameInput;

    [Header("HighScore UI")]
    [SerializeField] private TMP_Text highScoreListText;

    private int lastScore;
    private float lastTime;

    private void Start()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (highScorePanel != null) highScorePanel.SetActive(false);
    }

    public void ShowGameOver(int score, float timePlayed)
    {
        lastScore = score;
        lastTime = timePlayed;

        if (scoreText != null)
            scoreText.text = "Your Score: " + score;

        if (nameInput != null)
        {
            nameInput.text = "";
            nameInput.characterLimit = 3;
            nameInput.ActivateInputField();
        }

        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (highScorePanel != null) highScorePanel.SetActive(false);
    }

    
    public void OnClickSave()
    {
        if (nameInput == null) return;

        string name = nameInput.text.Trim().ToUpper();

        // asigurare 3 caractere 
        if (name.Length == 0) name = "AAA";
        if (name.Length > 3) name = name.Substring(0, 3);
        if (name.Length < 3) name = name.PadRight(3, 'A');

        HighScoreManager.Instance.AddScore(name, lastTime, lastScore);

        ShowHighScores();
    }

    public void ShowHighScores()
    {
        if (highScoreListText != null)
            highScoreListText.text = BuildHighScoreText();

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (highScorePanel != null) highScorePanel.SetActive(true);
    }

    private string BuildHighScoreText()
    {
        var list = HighScoreManager.Instance.highScores;

        string s = "";
        for (int i = 0; i < list.Count; i++)
        {
            string t = FormatTime(list[i].timePlayed);
            s += (i + 1) + ". " + list[i].playerName + "   " + t + "   " + list[i].score + "\n";
        }
        return s;
    }

    private string FormatTime(float seconds)
    {
        int total = Mathf.FloorToInt(seconds);
        int min = total / 60;
        int sec = total % 60;
        return min.ToString("00") + ":" + sec.ToString("00");
    }
}
