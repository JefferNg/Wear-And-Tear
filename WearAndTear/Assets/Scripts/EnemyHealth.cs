using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 2;
    [SerializeField] private int reward = 20;

    private bool isDestroyed = false;

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0 && !isDestroyed)
        {
            Summoner.onEnemyDestroy.Invoke();
            GameManager.main.AddGold(reward);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
