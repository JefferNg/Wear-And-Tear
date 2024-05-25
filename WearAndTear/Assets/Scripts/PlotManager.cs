using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        // tower already on tile
        if (tower != null) return;

        Tower towerBuild = TowerPlacement.main.GetTower();

        if (towerBuild.cost > GameManager.main.gold)
        {
            Debug.Log("Tower too expensive");
            return;
        }
        else
        {
            GameManager.main.SpendGold(towerBuild.cost);

            tower = Instantiate(towerBuild.prefab, transform.position, Quaternion.identity);
        }
        
    }

}
