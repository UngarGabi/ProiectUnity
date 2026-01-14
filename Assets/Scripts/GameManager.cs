using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject treePrefab;
    
    private float spawnZoneX;
    private float spawnZoneY;
    public float prefabCount;
    public float YRangeTrees = 400;
    public float xRangeTrees = 100;

    public GameObject pauseGame;
    private bool isGameOnPause = false;

    
    void Start()
    {
        //SpawnTrees();
      
    }

    void Update()
    {
        pauseMenu();
    }

    private void SpawnTrees()
    {
        for (int i = 0; i < prefabCount; i++) {
            spawnZoneX = Random.Range(xRangeTrees, YRangeTrees);
            spawnZoneY = Random.Range(xRangeTrees, YRangeTrees);

            Vector3 treePosition = new Vector3(spawnZoneX, 0, spawnZoneY);
            Instantiate(treePrefab, treePosition, treePrefab.GetComponent<Transform>().rotation);

        }
    }

    public void pauseMenu() // pauza la esc
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame.SetActive(true);
            Time.timeScale = 0.0f;
           
        }
       
    }

    public void resumeGame()
    {
        pauseGame.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void QuitGame() // iesi din aplicatie
    {
        Application.Quit();
    }

    public void GoToMainMenu() // te intorci la Main Menu
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Resetezi jocul
    }
}
