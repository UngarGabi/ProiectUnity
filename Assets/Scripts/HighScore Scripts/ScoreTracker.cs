using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker Instance;

    private int enemyKills;
    private float timePlayed;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(gameObject); 
            return; 
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        timePlayed += Time.deltaTime;
    }

    public void AddKill()
    {
        enemyKills++;
    }

    public int CalculateFinalScore()
    {
        int killScore = enemyKills * 10;
        int timeBonus = GetTimeBonus(timePlayed);
        return killScore + timeBonus;
    }

    private int GetTimeBonus(float seconds)
    {
        if (seconds <= 180f) return 500; // sub 3 min
        if (seconds <= 300f) return 300; // sub 5 min
        if (seconds <= 480f) return 150; // sub 8 min
        return 0;
    }

    public int GetKills() 
    { 
        return enemyKills; 
    }
    public float GetTimePlayed() 
    { 
        return timePlayed; 
    }

    public int CalculateScore_NoWinBonus()
    {
        // doar kills * 10 (sau ce mai ai)
        return enemyKills * 10;
    }

    public int CalculateScore_OnWin()
    {
        int killScore = enemyKills * 10;
        int timeBonus = GetTimeBonus(timePlayed);
        return killScore + timeBonus;
    }

}
