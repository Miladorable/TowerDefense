using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;

    // Vérifie si l'ennemi a déjà été détruit (pour éviter de le détruire plusieurs fois)
    private bool isDestroyed = false;

    // Méthode pour recevoir des dégâts
    public void TakeDamage(int dmg)
    {
        // On réduit les points de vie en fonction des dégâts reçus
        hitPoints -= dmg;

        // Si les points de vie sont inférieurs ou égaux à 0 et que l'ennemi n'a pas encore été détruit
        if (hitPoints <= 0 && !isDestroyed)
        {
            // On déclenche l'événement "enemyKilled" pour informer que l'ennemi est tué
            EnemySpawner.enemyKilled.Invoke();

            // On augmente la monnaie du joueur avec la récompense de l'ennemi
            LevelManager.main.IncreaseCurrency(currencyWorth);

            // On marque l'ennemi comme détruit pour éviter de le détruire plusieurs fois
            isDestroyed = true;

            // On détruit l'ennemi du jeu
            Destroy(gameObject);
        }
    }
}
