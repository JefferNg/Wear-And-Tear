using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 2;

    private bool isDestroyed = false;

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0 && !isDestroyed)
        {
            Summoner.onEnemyDestroy.Invoke();
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
