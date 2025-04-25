using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;


    [Header("References")]
    [SerializeField] private Tower[] towers;

    // Index de la tour actuellement sélectionnée (dans le tableau "towers")
    private int selectedTower = 0;

    private void Awake()
    {
        // On définit cette instance comme la "principale", pour pouvoir y accéder via BuildManager.main
        main = this;
    }

    // Retourne la tour actuellement sélectionnée
    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    // Change la tour sélectionnée, en modifiant l’index
    public void SetSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
    }
}
