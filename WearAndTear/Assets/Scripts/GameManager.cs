using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    [SerializeField] private TextMeshProUGUI waveUI;
    [SerializeField] private TextMeshProUGUI healthUI;
    [SerializeField] private int startLives = 10;

    public Transform startPoint;
    public Transform[] path;
    public int lives;
    public int gold;
    public bool gameEnd = false;
    public GameObject gameOverUI;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        gold = 100;
        lives = startLives;
        gameEnd = false;
    }

    private void Update()
    {
        if (gameEnd) return;

        if (lives <= 0 || Input.GetKeyDown("escape"))
        {
            EndGame();
        }
    }

    private void OnGUI()
    {
        waveUI.text = "Wave: " + Summoner.main.wave;
        healthUI.text = "Health: " + lives.ToString();
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

    public void ReduceHealth()
    {
        if (lives <= 0) return;

        lives--;
    }

    public void EndGame()
    {
        gameEnd = true;
        gameOverUI.SetActive(true);
        GameOver.main.SetWaves(Summoner.main.wave);
    }
}
