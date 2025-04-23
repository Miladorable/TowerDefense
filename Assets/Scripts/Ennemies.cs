using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Tilemap pathTilemap;

    private int currentHealth;
    private Vector3Int currentCell;
    private Vector3 targetPos;

    private List<Vector3Int> visitedCells = new List<Vector3Int>();

    void Start()
    {
        currentHealth = maxHealth;
        currentCell = pathTilemap.WorldToCell(transform.position);

        if (!IsPathTile(currentCell))
        {
            Debug.LogWarning("L'ennemi n'est pas sur une tuile de chemin.");
            Destroy(gameObject);
            return;
        }

        visitedCells.Add(currentCell);
        targetPos = pathTilemap.GetCellCenterWorld(currentCell);
    }

    void Update()
    {
        MoveToTarget();

        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
            currentCell = pathTilemap.WorldToCell(transform.position);
            SetNextTarget();
        }
    }

    void MoveToTarget()
    {
        Vector3 direction = (targetPos - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void SetNextTarget()
    {
        Vector3Int[] directions = {
            Vector3Int.right,
            Vector3Int.left,
            Vector3Int.up,
            Vector3Int.down
        };

        foreach (var dir in directions)
        {
            Vector3Int nextCell = currentCell + dir;

            // Ne va pas vers une cellule déjà visitée
            if (IsPathTile(nextCell) && !visitedCells.Contains(nextCell))
            {
                visitedCells.Add(nextCell);
                targetPos = pathTilemap.GetCellCenterWorld(nextCell);
                return;
            }
        }

        Debug.Log("Fin du chemin ou aucune tuile valide non visitée !");
    }

    bool IsPathTile(Vector3Int cellPos)
    {
        return pathTilemap.GetTile(cellPos) != null;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
}