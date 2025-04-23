using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private Tilemap buildTilemap; // Référence à la tilemap autorisée
    private bool isPlaced = false;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (isPlaced) return;

        // Suivre la souris
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        transform.position = mouseWorldPos;

        if (Input.GetMouseButtonDown(0))
        {
            // Convertir la position souris -> cellule de la tilemap
            Vector3Int cell = buildTilemap.WorldToCell(mouseWorldPos);
            if (buildTilemap.GetTile(cell) != null)
            {
                // Snap au centre de la cellule et placer définitivement
                transform.position = buildTilemap.GetCellCenterWorld(cell);
                isPlaced = true;
            }
            else
            {
                Debug.Log("Tuile non valide pour placer la tourelle.");
            }
        }
    }
}