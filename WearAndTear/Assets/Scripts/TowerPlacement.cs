using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{

    public static TowerPlacement main;

    [SerializeField] private GameObject[] towers;

    private int towerIndex = 0;


    private void Awake()
    {
        main = this;
    }

    public GameObject GetTower()
    {
        return towers[towerIndex];
    }

    /*[SerializeField] private Camera playerCamera;
    private GameObject currentTower;

    

    // Update is called once per frame
    void Update()
    {
        if (currentTower != null)
        {
            Ray camray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(camray, out RaycastHit hitInfor, 100f))
            {
                currentTower.transform.position = hitInfor.point;
            }

            if (Input.GetMouseButtonDown(0))
            {
                currentTower = null;
            }
        }
    }

    public void SetTowerToPlace(GameObject tower)
    {
        currentTower = Instantiate(tower, Vector3.zero, Quaternion.identity);
    }*/
}
