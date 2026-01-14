using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;

    private const int MAX_ENTRIES = 10;

    public List<HighScoreEntry> highScores = new List<HighScoreEntry>();

    private const string SAVE_KEY = "HIGHSCORES";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScores();
    }

    // adaugi un scor nou
    public void AddScore(string name, float timePlayed, int score)
    {
        HighScoreEntry entry = new HighScoreEntry
        {
            playerName = name,
            timePlayed = timePlayed,
            score = score
        };

        highScores.Add(entry);

        SortScores();
        TrimList();
        SaveScores();
    }

    private void SortScores()
    {
        highScores.Sort((a, b) => b.score.CompareTo(a.score));
    }

    private void TrimList()
    {
        if (highScores.Count > MAX_ENTRIES)
        {
            highScores.RemoveRange(MAX_ENTRIES, highScores.Count - MAX_ENTRIES);
        }
    }

    private void SaveScores()
    {
        string json = JsonUtility.ToJson(new HighScoreWrapper { entries = highScores });
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    private void LoadScores()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return;

        string json = PlayerPrefs.GetString(SAVE_KEY);
        HighScoreWrapper wrapper = JsonUtility.FromJson<HighScoreWrapper>(json);

        if (wrapper != null && wrapper.entries != null)
        {
            highScores = wrapper.entries;
        }
    }

    // wrapper necesar pt JsonUtility
    [System.Serializable]
    private class HighScoreWrapper
    {
        public List<HighScoreEntry> entries;
    }
}
