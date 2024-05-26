using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Summoner : MonoBehaviour
{
    public static Summoner main;

    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float spawnDelay = 0.5f;
    [SerializeField] public float waveDelay = 5f;
    [SerializeField] private float difficultyScale = 0.75f;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    public int wave = 1;
    private float timeLastSpawn;
    private int enemiesActive;
    private int enemiesRemain;
    private int lightEnemiesRemain;
    private int heavyEnemiesRemain;
    public bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
        main = this;
    }

    private void Start()
    {
        StartCoroutine(StartWave());
        // temporary
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeLastSpawn += Time.deltaTime;

        if (timeLastSpawn >= 1f/spawnDelay && lightEnemiesRemain > 0) // spawn light enemies
        {
            SpawnEnemy(enemyPrefabs[0]);
            enemiesRemain--;
            lightEnemiesRemain--;
            enemiesActive++;
            timeLastSpawn = 0f;
        }

        if (timeLastSpawn >= 1f / spawnDelay && heavyEnemiesRemain > 0) // spawn heavy enemies
        {
            SpawnEnemy(enemyPrefabs[1]);
            enemiesRemain--;
            heavyEnemiesRemain--;
            enemiesActive++;
            timeLastSpawn = 0f;
        }

        if (enemiesActive == 0 && enemiesRemain == 0)
        {
            EndWave();
        }
    }

    private IEnumerator StartWave()
    {
        GameManager.main.nextWave = true;
        yield return new WaitForSeconds(waveDelay);
        isSpawning = true;
        enemiesRemain = EnemiesPerWave();
        if (wave > 2)
        {
            lightEnemiesRemain = (int)(enemiesRemain * 0.75);
            heavyEnemiesRemain = enemiesRemain - lightEnemiesRemain;
        }
        else
        {
            lightEnemiesRemain = enemiesRemain;
        }
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(wave, difficultyScale));
    }

    private void SpawnEnemy(GameObject spawn)
    {
        Instantiate(spawn, GameManager.main.startPoint.position, Quaternion.Euler(0f, 0f, -90f));
    }

    private void EnemyDestroyed()
    {
        enemiesActive--;
    }

    private void EndWave()
    {
        isSpawning = false;
        timeLastSpawn = 0f;
        wave++;
        spawnDelay += 0.1f;
        EnemyMovement.main.speed += 0.1f;
        StartCoroutine(StartWave());
    }

}
