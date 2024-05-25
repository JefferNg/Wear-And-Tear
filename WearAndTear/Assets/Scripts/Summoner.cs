using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Summoner : MonoBehaviour
{

    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float spawnDelay = 0.5f;
    [SerializeField] private float waveDelay = 5f;
    [SerializeField] private float difficultyScale = 0.75f;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int wave = 1;
    private float timeLastSpawn;
    private int enemiesActive;
    private int enemiesRemain;
    private bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
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

    /*public static List<Enemy> enemiesInGame;
    public static Dictionary<int, GameObject> enemyPrefabs;
    public static Dictionary<int, Queue<Enemy>> enemyQueues;

    private static bool initialized;
    public static void Init()
    {
        if (!initialized)
        {
            enemyPrefabs = new Dictionary<int, GameObject>();
            enemyQueues = new Dictionary<int, Queue<Enemy>>();
            enemiesInGame = new List<Enemy>();

            EnemySummonData[] enemies = Resources.LoadAll<EnemySummonData>("Enemies");

            foreach (EnemySummonData enemy in enemies)
            {
                enemyPrefabs.Add(enemy.enemyID, enemy.enemyPrefab);
                enemyQueues.Add(enemy.enemyID, new Queue<Enemy>());
            }
            initialized = true;
        }
        else
        {
            Debug.Log("Already Initialized");
        }
        
    }

    public static Enemy SummonEnemy(int EnemyID)
    {
        Enemy SummonedEnemy = null;

        if (enemyPrefabs.ContainsKey(EnemyID))
        {
            Queue<Enemy> ReferencedQueue = enemyQueues[EnemyID];
            if (ReferencedQueue.Count > 0)
            {
                SummonedEnemy = ReferencedQueue.Dequeue();
                SummonedEnemy.Init();

                SummonedEnemy.gameObject.SetActive(true);
            }
            else
            {
                GameObject NewEnemy = Instantiate(enemyPrefabs[EnemyID], new Vector3(-9.5f, -3.5f), Quaternion.identity);
                SummonedEnemy = NewEnemy.GetComponent<Enemy>();
                SummonedEnemy.Init();
            }
        }
        else
        {
            Debug.Log("Enemy ID does not exist");
            return null;
        }
        return SummonedEnemy;
    }

    public static void RemoveEnemy(Enemy enemy)
    {
        enemiesInGame.Remove(enemy);
        enemyQueues[enemy.ID].Enqueue(enemy);
        enemy.gameObject.SetActive(false);
    }*/

}
