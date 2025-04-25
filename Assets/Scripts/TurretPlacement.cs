using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    // Référence à la tour qui sera placée (s'il y en a une)
    private GameObject tower;

    // Couleur d'origine du Sprite (pour pouvoir la remettre quand la souris part)
    private Color startColor;

    private void Start()
    {
        // On stocke la couleur de départ au lancement du jeu
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        // Quand la souris survole l’objet, on change sa couleur
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        // Quand la souris quitte l’objet, on remet la couleur d’origine
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        // Si une tour est déjà placée ici, on ne fait rien
        if (tower != null) return;

        // On récupère la tour sélectionnée à construire depuis le BuildManager
        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        // Si le joueur n’a pas assez de monnaie, on affiche un message et on quitte
        if (towerToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("pas assez d'argent");//tu ne peux pas acheter la tourelle
            return;
        }

        // On dépense la monnaie pour construire la tour
        LevelManager.main.SpendCurrency(towerToBuild.cost);

        // On crée (instancie) la tour à l’endroit de l’objet
        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
    }
}