using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;

    // V�rifie si l'ennemi a d�j� �t� d�truit (pour �viter de le d�truire plusieurs fois)
    private bool isDestroyed = false;

    // M�thode pour recevoir des d�g�ts
    public void TakeDamage(int dmg)
    {
        // On r�duit les points de vie en fonction des d�g�ts re�us
        hitPoints -= dmg;

        // Si les points de vie sont inf�rieurs ou �gaux � 0 et que l'ennemi n'a pas encore �t� d�truit
        if (hitPoints <= 0 && !isDestroyed)
        {
            // On d�clenche l'�v�nement "enemyKilled" pour informer que l'ennemi est tu�
            EnemySpawner.enemyKilled.Invoke();

            // On augmente la monnaie du joueur avec la r�compense de l'ennemi
            LevelManager.main.IncreaseCurrency(currencyWorth);

            // On marque l'ennemi comme d�truit pour �viter de le d�truire plusieurs fois
            isDestroyed = true;

            // On d�truit l'ennemi du jeu
            Destroy(gameObject);
        }
    }
}
