using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public int ID;

    public void Init()
    {
        health = maxHealth;
    }
}
