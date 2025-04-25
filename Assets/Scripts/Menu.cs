using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] private GameObject menuPanel;


    // Cette m�thode est appel�e automatiquement par Unity pour afficher l'interface graphique
    private void OnGUI()
    {
        // On met � jour le texte de l'interface avec la valeur actuelle de la monnaie
        currencyUI.text = LevelManager.main.currency.ToString();
    }

    // Cette m�thode est vide pour l�instant � peut �tre utilis�e pour g�rer la s�lection d�un �l�ment de menu
    public void SetSelected()
    {

    }

    // Cette m�thode active ou d�sactive le menu : si le menu est ouvert, il le ferme, sinon il l�ouvre
    public void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }

    // Cette m�thode affiche le menu (le rend actif)
    public void ShowMenu()
    {
        menuPanel.SetActive(true);
    }
}

