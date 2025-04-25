using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    // Instance statique pour acc�der facilement � ce script depuis d'autres scripts
    public static LevelManager main;

    // Point de d�part (peut servir � faire appara�tre des ennemis, des unit�s, etc.)
    public Transform startpoint;

    // Chemin que suivront les ennemis ou unit�s (sous forme de tableau de points)
    public Transform[] path;

    // Monnaie du joueur (par exemple, pour acheter des tours)
    public int currency;

    // Cette m�thode est appel�e avant Start, utile pour initialiser des choses importantes
    private void Awake()
    {
        // On d�finit cette instance comme la "principale", pour y acc�der avec LevelManager.main
        main = this;
    }

    // Cette m�thode est appel�e au d�but du jeu
    private void Start()
    {
        // On initialise la monnaie du joueur � 100
        currency = 100;
    }

    // Ajoute une certaine somme � la monnaie actuelle
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