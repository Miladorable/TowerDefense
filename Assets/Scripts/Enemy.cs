using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [HeaderAttribute("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target; // Cible actuelle de l'ennemi (le prochain point du chemin)
    private int pathIndex = 0; // Index du prochain point de chemin que l'ennemi doit atteindre

    private void Start()
    {
        // On définit la première cible comme étant le premier point du chemin
        target = LevelManager.main.path[pathIndex];
    }

    private void Update()
    {
        // Si l'ennemi est assez proche du point de destination (petit seuil de 0.1)
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            // On passe au point suivant du chemin
            pathIndex++;

            // Si on a atteint la fin du chemin (plus de points), l'ennemi est "tué"
            if (pathIndex == LevelManager.main.path.Length)
            {
                // Appel d'un événement pour signaler que l'ennemi est tué
                EnemySpawner.enemyKilled.Invoke();

                // On détruit l'ennemi
                Destroy(gameObject);
                return;
            }

            // Sinon, on définit la nouvelle cible
            target = LevelManager.main.path[pathIndex];
        }
    }

    private void FixedUpdate()
    {
        // On calcule la direction entre la position actuelle et la cible
        Vector2 direction = (target.position - transform.position).normalized;

        // On applique la vitesse au Rigidbody2D pour déplacer l'ennemi dans cette direction
        rb.velocity = direction * moveSpeed;
    }
}
