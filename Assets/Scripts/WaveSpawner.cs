using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    private enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    private class Wave
    {
        public GameObject[] enemies;
        public int[] spawnAmount;
        public string name;
        public float spawnRate;        
    }

    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private GameObject[] spawnPoints;

    [SerializeField]
    private TextMeshProUGUI waveNumberText;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private SpawnState state = SpawnState.COUNTING;
    [SerializeField]
    private float timeBetweenWaves = 3f;

    private float waveCountdown;
    private int waveNumber = 1;
    private int nextWave = 0;
    private float searchCountDown = 1.5f;

    
    

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        state = SpawnState.COUNTING;
    }

    private void Update()
    {
        if (!gameManager.isGameActive || gameManager.isGamePaused)
        {
            return;  // if game is over or pause ends the Update method
        }
        if (state == SpawnState.WAITING)
        {
            if (!EnemiesAlive())
            {
                WaveDone();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0 && state != SpawnState.SPAWNING)
        {
            StartCoroutine(WaveSpawn(waves[nextWave]));
        }    
        else if (state == SpawnState.COUNTING)
        {
            waveNumberText.SetText($"Wave Number: {waveNumber}");  // Inbetween waves announcement
            waveNumberText.gameObject.SetActive(true);
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveDone()
    {
        state = SpawnState.COUNTING; // Changes the game state, resets timer, and increments the waves
        Debug.Log("Wave Complete");
        waveCountdown = timeBetweenWaves;
        waveNumber++;
        nextWave++;
        nextWave = Mathf.Clamp(nextWave, 0, (waves.Length - 1)); // ensures the repeat waves don't go out of the array bounds
    }

    IEnumerator WaveSpawn(Wave _wave)
    {
        waveNumberText.gameObject.SetActive(false);  // disables the wave announcement
        state = SpawnState.SPAWNING;  // change game state
        Debug.Log("Spawn wave: " + waveNumber);
        for (int i = 0; i < _wave.spawnAmount.Length; i++)     // iterates through the first array
        {
            for (int j = 0; j < _wave.spawnAmount[i]; j++)     // iterates the number of times in the spawnAmount array
            {
                SpawnEnemy(_wave.enemies[i]);
                yield return new WaitForSeconds(1f / _wave.spawnRate);
            }
        }


        state = SpawnState.WAITING;
        yield break;
    }

    // ABSTRACTION
    private void SpawnEnemy(GameObject enemy)
    {
        Debug.Log("Spawning: " + enemy.name);
        int spawnPoint = Random.Range(1, 4);
                
        Instantiate(enemy, spawnPoints[spawnPoint].transform.position, enemy.transform.rotation); // Spawn enemies at a random spawn point from the array
    }

    // ABSTRACTION
    private bool EnemiesAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0 && GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            searchCountDown = 1.5f;
            return false;
        }
        return true;
    }
}