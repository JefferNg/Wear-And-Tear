using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    public static Menu main;

    [SerializeField] TextMeshProUGUI goldUI;
    [SerializeField] Animator anim;

    private bool isMenuOpen = true;

    private void Awake()
    {
        main = this;
    }

    private void OnGUI()
    {
        goldUI.text = GameManager.main.gold.ToString();
    }

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

}
