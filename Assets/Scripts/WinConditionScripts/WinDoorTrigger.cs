using UnityEngine;

public class WinDoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("AI CASTIGAT!");

            // stop joc
            Time.timeScale = 0f;
            int finalScore = ScoreTracker.Instance.CalculateScore_OnWin();
            float time = ScoreTracker.Instance.GetTimePlayed();

            FindObjectOfType<GameOverUI>().ShowGameOver(finalScore, time);

            HighScoreManager.Instance.AddScore("ABC", time, finalScore);



        }
    }
}
