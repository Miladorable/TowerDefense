using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class TurretScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint; 
    
    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float shootSpeed = 1f; // balles par secondes

    private Transform target; // L’ennemi actuellement visé
    private float timeUntilFire; // Temps écoulé depuis le dernier tir

    private void Update()
    {
        // Si aucune cible, on cherche une cible
        if (target == null)
        {
            FindTarget();
            return;
        }

        // Sinon, on tourne vers la cible
        RotateTowardsTarget();

        // Si la cible est sortie de portée, on l'oublie
        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            // On accumule le temps pour savoir quand tirer
            timeUntilFire += Time.deltaTime;

            // Si le délai de tir est atteint, on tire
            if (timeUntilFire >= 1f / shootSpeed)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        // On instancie une balle au point de tir, avec la rotation actuelle de la tourelle
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, turretRotationPoint.rotation);

        // On récupère le script de la balle pour lui définir une cible
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTrarget(target); // ⚠️ petite faute ici : "SetTrarget" → "SetTarget"
    }

    private void FindTarget()
    {
        // On détecte tous les ennemis dans un cercle autour de la tourelle
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange,
            (Vector2)transform.position, 0f, enemyMask);

        // Si au moins un ennemi est trouvé, on prend le premier
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        // Vérifie que la cible est toujours à portée
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        // Calcul de l’angle entre la tourelle et la cible
        float angle = Mathf.Atan2(target.position.y - transform.position.y,
                                  target.position.x - turretRotationPoint.position.x)
                                  * Mathf.Rad2Deg - 90f;

        // On crée la rotation souhaitée
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        // On fait tourner doucement la tourelle vers la rotation cible
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation,
                                                                 targetRotation,
                                                                 rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        // Affiche dans la scène l'aire de portée de la tourelle quand elle est sélectionnée
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, targetingRange);
    }
}
