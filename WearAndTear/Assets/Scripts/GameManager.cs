using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    [SerializeField] private TextMeshProUGUI waveUI;
    [SerializeField] private TextMeshProUGUI healthUI;
    [SerializeField] private TextMeshProUGUI nextWaveUI;
    [SerializeField] private int startLives = 10;
    [SerializeField] Animator nextWaveAnim;
    [SerializeField] Animator goldCountAnim;

    public Transform startPoint;
    public Transform[] path;
    public int lives;
    public int gold;
    public bool gameEnd = false;
    public GameObject gameOverUI;
    private float waveCounter;
    public bool nextWave;
    private bool lowGold = false;
    private float timer = 3f;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        lives = startLives;
        gameEnd = false;
        waveCounter = Summoner.main.waveDelay;
        nextWave = true;
        nextWaveAnim.SetBool("WaveDone", nextWave);
    }

    private void Update()
    {
        if (gameEnd) return;

        if (lives <= 0 || Input.GetKeyDown("escape"))
        {
            EndGame();
        }

        if (nextWave)
        {
            if (waveCounter <= 0f)
            {
                waveCounter = Summoner.main.waveDelay;
                nextWave = false;
            }
            else
            {
                nextWave = true;
                waveCounter -= Time.deltaTime;
            }
            nextWaveAnim.SetBool("WaveDone", nextWave);
            
        }

        if (lowGold)
        {
            if (timer  > 0f)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                goldCountAnim.SetBool("LowGold", false);
                lowGold = false;
                timer = 3f;
            }
        }
        
    }

    private void OnGUI()
    {
        waveUI.text = "Wave: " + Summoner.main.wave;
        healthUI.text = "Health: " + lives.ToString();
        nextWaveUI.text = "Next Wave In " + ((int) waveCounter).ToString();
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
            lowGold = true;
            goldCountAnim.SetBool("LowGold", true);

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
