using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private List<GameObject> enemyList;

    public bool isGameActive = true;
    public bool isGamePaused { get; private set; } // ENCAPSULATION - really over the entire thing,
                                                   // getters/setters seem like poor form so mostly used private/protected variables and serialized ones needed

    void Start()
    {
        isGamePaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameActive)
        {
            isGamePaused = !isGamePaused;
            PauseGame();
        }
    }

    private void PauseGame()
    {
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    // ABSTRACTION
    public void GameOver()
    {
        // Destroy objects to clear screen
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
        Destroy(GameObject.FindGameObjectWithTag("Player"));

        isGameActive = false;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
