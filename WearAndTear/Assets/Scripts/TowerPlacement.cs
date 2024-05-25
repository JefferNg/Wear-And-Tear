using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{

    public static TowerPlacement main;

    //[SerializeField] private GameObject[] towers;
    [SerializeField] private Tower[] towers;

    private int towerIndex = 0;


    private void Awake()
    {
        main = this;
    }

    public Tower GetTower()
    {
        
        return towers[towerIndex];
    }

    public void SetTower(int tower)
    {
        towerIndex = tower;
    }
}
