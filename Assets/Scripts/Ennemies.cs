using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ennemies : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Tilemap pathTilemap;

    private int currentHealth;
    private Vector3Int currentCell;
    private Vector3 targetWorldPos;

    void Start()
    {
        
        currentHealth = maxHealth;

        // On récupère la cellule de départ
        currentCell = pathTilemap.WorldToCell(transform.position);
        if (pathTilemap.GetTile(currentCell) == null)
        {
            Debug.Log("Pas sur une tuile de chemin !");
            Destroy(gameObject);
            return;
        }

        targetWorldPos = pathTilemap.GetCellCenterWorld(currentCell);
    }

    void Update()
    {
        // Avance vers la position cible
        Vector3 dir = (targetWorldPos - transform.position).normalized;
        rb.MovePosition(transform.position + dir * moveSpeed * Time.deltaTime);

        // Si on est arrivé à la cellule cible
        if (Vector3.Distance(transform.position, targetWorldPos) < 0.05f)
        {
            currentCell = pathTilemap.WorldToCell(transform.position);
            FindNextTile();
        }
    }

    void FindNextTile()
    {
        // Directions cardinales : haut, bas, gauche, droite
        Vector3Int[] directions = {
            Vector3Int.left,
            Vector3Int.right,
            Vector3Int.up,
            Vector3Int.down
        };

        foreach (var dir in directions)
        {
            Vector3Int next = currentCell + dir;
            if (pathTilemap.GetTile(next) != null && next != currentCell)
            {
                targetWorldPos = pathTilemap.GetCellCenterWorld(next);
                return;
            }
        }

        Debug.Log("Plus de chemin, arrêt de l'ennemi.");
        // Optionnel : stop, mort ou arrivée à destination
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}