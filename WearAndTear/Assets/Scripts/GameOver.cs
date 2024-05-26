using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public static GameOver main;

    [SerializeField] private TextMeshProUGUI waveCounter;

    private int waves;


    private void Awake()
    {
        main = this;
    }

    private void OnGUI()
    {
        waveCounter.text = waves.ToString();
    }

    public void SetWaves(int _waves)
    {
        waves = _waves;
    }
}
