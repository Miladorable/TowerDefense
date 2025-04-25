using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    // Instance statique pour accéder facilement à ce script depuis d'autres scripts
    public static LevelManager main;

    // Point de départ (peut servir à faire apparaître des ennemis, des unités, etc.)
    public Transform startpoint;

    // Chemin que suivront les ennemis ou unités (sous forme de tableau de points)
    public Transform[] path;

    // Monnaie du joueur (par exemple, pour acheter des tours)
    public int currency;

    // Cette méthode est appelée avant Start, utile pour initialiser des choses importantes
    private void Awake()
    {
        // On définit cette instance comme la "principale", pour y accéder avec LevelManager.main
        main = this;
    }

    // Cette méthode est appelée au début du jeu
    private void Start()
    {
        // On initialise la monnaie du joueur à 100
        currency = 100;
    }

    // Ajoute une certaine somme à la monnaie actuelle
    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    // Tente de retirer une somme de la monnaie : si assez d'argent, on paie et retourne true, sinon on affiche un message et retourne false
    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("You do not have enough"); // Message dans la console Unity
            return false;
        }
    }
}