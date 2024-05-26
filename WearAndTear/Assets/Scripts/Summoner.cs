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
    [SerializeField] private float waveDelay = 5f;
    [SerializeField] private float difficultyScale = 0.75f;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    public int wave = 1;
    private float timeLastSpawn;
    private int enemiesActive;
    private int enemiesRemain;
    private bool isSpawning = false;

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

        if (timeLastSpawn >= 1f/spawnDelay && enemiesRemain > 0)
        {
            SpawnEnemy();
            enemiesRemain--;
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
        yield return new WaitForSeconds(waveDelay);
        isSpawning = true;
        enemiesRemain = EnemiesPerWave();
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(wave, difficultyScale));
    }

    private void SpawnEnemy()
    {
        GameObject spawn = enemyPrefabs[0]; // choose type of enemy
        Instantiate(spawn, GameManager.main.startPoint.position, Quaternion.identity);
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
        StartCoroutine(StartWave());
    }

}
