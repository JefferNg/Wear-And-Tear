using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditsPage;

    private void Start()
    {
        mainMenu.SetActive(true);
        creditsPage.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenCredits()
    {
        mainMenu.SetActive(false);
        creditsPage.SetActive(true);
    }

    public void CloseCredits()
    {
        mainMenu.SetActive(true);
        creditsPage.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
    }
}
