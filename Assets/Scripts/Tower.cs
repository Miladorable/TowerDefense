using UnityEngine;

// Cette annotation permet � la classe d'�tre visible dans l'inspecteur Unity
[System.Serializable]
public class Tower
{
    // Nom de la tour
    public string name;

    // Co�t de la tour (par exemple, en monnaie du jeu)
    public int cost;

    // Pr�fabriqu� (mod�le de tour � instancier dans le jeu)
    public GameObject prefab;

    // Constructeur de la classe Tower : permet de cr�er une nouvelle tour avec un nom, un co�t et un prefab
    public Tower(string _name, int _cost, GameObject _prefab)
    {
        name = _name;       // On attribue le nom donn� � la propri�t� "name"
        cost = _cost;       // On attribue le co�t donn� � la propri�t� "cost"
        prefab = _prefab;   // On assigne le GameObject donn� � la propri�t� "prefab"
    }
}