using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb; 
    
    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    private Transform target;

    // Méthode appelée pour définir la cible de la balle
    public void SetTrarget(Transform _target)
    {
        target = _target;
    }

    // Utilisé pour le mouvement physique (appelé à intervalle fixe)
    private void FixedUpdate()
    {
        // Si la cible n'existe pas (plus), on ne fait rien
        if (!target) return;

        // On calcule la direction de la cible à partir de la position actuelle
        Vector2 direction = (target.position - transform.position).normalized;

        // On applique une vitesse dans cette direction
        rb.velocity = direction * bulletSpeed;
    }

    // Déclenché quand la balle entre en collision avec quelque chose
    private void OnCollisionEnter2D(Collision2D other)
    {
        // On inflige des dégâts à l’objet touché (s’il a un composant Health)
        other.gameObject.GetComponent<Health>()?.TakeDamage(bulletDamage);

        // On détruit la balle après la collision
        Destroy(gameObject);
    }
}
