using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class TurretScript : MonoBehaviour
{
    [SerializeField] private float shootSpeed = 2.5f;
    private Collider2D collider;
    [SerializeField] private Tilemap grassTilemap; // La tilemap sur laquelle on peut construire
    [SerializeField] private GameObject bulletPrefab; // Optionnel si tu veux tirer des projectiles

    private float shootCooldown = 0f;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //PLacer la tourelle sur "grass" uniquement et elle bouge plus
        // Convertir la position du monde en cellule de la tilemap
        Vector3Int gridPosition = grassTilemap.WorldToCell(transform.position);
        TileBase tile = grassTilemap.GetTile(gridPosition);

        if (tile == null)
        {
            Debug.Log("Tu ne peux pas placer ici ! La tourelle est détruite.");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Tourelle placée avec succès sur la tile 'grass'.");
        }

    }

    // Update is called once per frames
    void Update()
    {
        //On Verifie a chaque frame si il y a un ennemi dans le trigger de la tourelle
        
    }
}
