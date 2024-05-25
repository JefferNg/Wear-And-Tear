using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    public Transform startPoint;
    public Transform[] path;

    public int gold;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        gold = 100;
    }

    public void AddGold(int amt)
    {
        gold += amt;
    }

    public bool SpendGold(int amt)
    {
        if (amt <= gold)
        {
            gold -= amt;
            return true;
        }
        else
        {
            // add ui
            Debug.Log("Not enough gold");
            return false;
        }
    }
}
