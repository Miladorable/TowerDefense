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


    // Cette méthode est appelée automatiquement par Unity pour afficher l'interface graphique
    private void OnGUI()
    {
        // On met à jour le texte de l'interface avec la valeur actuelle de la monnaie
        currencyUI.text = LevelManager.main.currency.ToString();
    }

    // Cette méthode est vide pour l’instant — peut être utilisée pour gérer la sélection d’un élément de menu
    public void SetSelected()
    {

    }

    // Cette méthode active ou désactive le menu : si le menu est ouvert, il le ferme, sinon il l’ouvre
    public void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }

    // Cette méthode affiche le menu (le rend actif)
    public void ShowMenu()
    {
        menuPanel.SetActive(true);
    }
}

