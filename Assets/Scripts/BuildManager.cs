using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;


    [Header("References")]
    [SerializeField] private Tower[] towers;

    // Index de la tour actuellement s�lectionn�e (dans le tableau "towers")
    private int selectedTower = 0;

    private void Awake()
    {
        // On d�finit cette instance comme la "principale", pour pouvoir y acc�der via BuildManager.main
        main = this;
    }

    // Retourne la tour actuellement s�lectionn�e
    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    // Change la tour s�lectionn�e, en modifiant l�index
    public void SetSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
    }
}
