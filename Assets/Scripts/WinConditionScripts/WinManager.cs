using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public static WinManager Instance;

    [SerializeField] private WinUI ui;
    [SerializeField] private GameObject doorObject; // initial SetActive(false)

    private int totalGoals;
    private int completedGoals;
    private bool[] completedFlags;
    private WorldGoal currentGoal;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(gameObject); 
            return; 
        }
        Instance = this;
    }

    public void SetTotalGoals(int total)
    {
        totalGoals = total;
        completedGoals = 0;
        completedFlags = new bool[totalGoals];

        if (doorObject != null)
            doorObject.SetActive(false);

        if (ui != null)
        {
            ui.SetProgress(completedGoals, totalGoals);
            ui.SetHoldProgress(0f);
        }
    }

    public void GoalCompleted(int index)
    {
        if (index < 0 || index >= completedFlags.Length) 
            return;
        
        if (completedFlags[index]) 
            return;

        completedFlags[index] = true;
        completedGoals++;

        if (ui != null)
            ui.SetProgress(completedGoals, totalGoals);

        if (completedGoals >= totalGoals)
        {
            OpenDoor();
        }
    }

    public void SetHoldProgress(float t01)
    {
        if (ui != null)
            ui.SetHoldProgress(t01);
    }

    private void OpenDoor()
    {
        if (doorObject != null)
            doorObject.SetActive(true);
    }

    public void SetCurrentGoal(WorldGoal goal)
    {
        currentGoal = goal;
        SetHoldProgress(0f);
    }

    public bool IsCurrentGoal(WorldGoal goal)
    {
        return currentGoal == goal;
    }

    public void ClearCurrentGoal(WorldGoal goal)
    {
        if (currentGoal == goal)
        {
            currentGoal = null;
            SetHoldProgress(0f);
        }
    }

    public void ShowHoldBar(bool show)
    {
        if (ui != null)
            ui.ShowHoldBar(show);
    }

}
