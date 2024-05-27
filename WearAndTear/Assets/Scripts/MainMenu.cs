using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditsPage;
    [SerializeField] private GameObject howToPage;

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

    public void OpenHowTo()
    {
        mainMenu.SetActive(false);
        howToPage.SetActive(true);
    }

    public void ReturnToMenu()
    {
        mainMenu.SetActive(true);
        creditsPage.SetActive(false);
        howToPage.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
