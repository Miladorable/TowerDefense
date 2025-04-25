using UnityEngine;

// Cette annotation permet à la classe d'être visible dans l'inspecteur Unity
[System.Serializable]
public class Tower
{
    // Nom de la tour
    public string name;

    // Coût de la tour (par exemple, en monnaie du jeu)
    public int cost;

    // Préfabriqué (modèle de tour à instancier dans le jeu)
    public GameObject prefab;

    // Constructeur de la classe Tower : permet de créer une nouvelle tour avec un nom, un coût et un prefab
    public Tower(string _name, int _cost, GameObject _prefab)
    {
        name = _name;       // On attribue le nom donné à la propriété "name"
        cost = _cost;       // On attribue le coût donné à la propriété "cost"
        prefab = _prefab;   // On assigne le GameObject donné à la propriété "prefab"
    }
}