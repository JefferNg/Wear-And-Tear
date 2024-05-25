using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour
{
    public static List<Enemy> enemiesInGame;
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
    }
    
}
