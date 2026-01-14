using System.Collections.Generic;
using UnityEngine;

public class WorldGoalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goalPrefab; //prefab monument
    [SerializeField] private Transform[] spawnPoints; //cele 10 puncte
    [SerializeField] private int goalsToSpawn = 5; // cate monumente

    private void Start()
    {
        //verificari 
        if (goalPrefab == null)
        {
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            return;
        }

        if (goalsToSpawn > spawnPoints.Length)
        {
            goalsToSpawn = spawnPoints.Length;
        }

        List<int> chosen = PickUniqueRandomIndexes(spawnPoints.Length, goalsToSpawn); // luam un array cu index random

        // spunem managerului cate trebuie completate
        WinManager.Instance.SetTotalGoals(goalsToSpawn);

        //pentru fiecare index random ii vom da spawn
        for (int i = 0; i < chosen.Count; i++)
        {
            Transform sp = spawnPoints[chosen[i]];
            GameObject obj = Instantiate(goalPrefab, sp.position, sp.rotation);

            WorldGoal goal = obj.GetComponent<WorldGoal>();
            if (goal != null)
            {
                goal.SetIndex(i); // index unic pentru progres (0..goalsToSpawn-1)
            }
        }
    }

    private List<int> PickUniqueRandomIndexes(int maxExclusive, int count) // functie care iti returneaza o lista cu indexsi randomi
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < maxExclusive; i++)
            indexes.Add(i);

        // shuffle
        for (int i = indexes.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = indexes[i];
            indexes[i] = indexes[j];
            indexes[j] = temp;
        }

        // luam primele care ne trebuie
        List<int> chosen = new List<int>();
        for (int i = 0; i < count; i++)
            chosen.Add(indexes[i]);

        return chosen;
    }
}
