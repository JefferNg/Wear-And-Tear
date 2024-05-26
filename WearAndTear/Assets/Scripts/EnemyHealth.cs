using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 2;
    [SerializeField] private int reward = 20;

    private bool isDestroyed = false;

    private void Update()
    {
        if (gameObject.layer == 8)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0 && !isDestroyed)
        {
            Summoner.onEnemyDestroy.Invoke();
            GameManager.main.AddGold(reward);
            isDestroyed = true;
            gameObject.layer = 8;
            //Destroy(gameObject);
        }
    }
}
