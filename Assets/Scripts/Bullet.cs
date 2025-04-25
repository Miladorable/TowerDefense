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

    // M�thode appel�e pour d�finir la cible de la balle
    public void SetTrarget(Transform _target)
    {
        target = _target;
    }

    // Utilis� pour le mouvement physique (appel� � intervalle fixe)
    private void FixedUpdate()
    {
        // Si la cible n'existe pas (plus), on ne fait rien
        if (!target) return;

        // On calcule la direction de la cible � partir de la position actuelle
        Vector2 direction = (target.position - transform.position).normalized;

        // On applique une vitesse dans cette direction
        rb.velocity = direction * bulletSpeed;
    }

    // D�clench� quand la balle entre en collision avec quelque chose
    private void OnCollisionEnter2D(Collision2D other)
    {
        // On inflige des d�g�ts � l�objet touch� (s�il a un composant Health)
        other.gameObject.GetComponent<Health>()?.TakeDamage(bulletDamage);

        // On d�truit la balle apr�s la collision
        Destroy(gameObject);
    }
}
