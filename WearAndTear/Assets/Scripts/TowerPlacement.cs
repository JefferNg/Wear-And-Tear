using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{

    public static TowerPlacement main;

    [SerializeField] private Tower[] towers;

    private Tower towerToBuild;


    private void Awake()
    {
        main = this;
    }

    public Tower GetTower()
    {
        return towerToBuild;
    }

    public void SetTower(int tower)
    {
        if (tower == -1)
        {
            towerToBuild = null;
            return;
        }
        towerToBuild = towers[tower];
    }

}
