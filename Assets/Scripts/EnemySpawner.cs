using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent enemyKilled = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSwpan;
    private bool isSpawning = false;

    private void Awake()
    {
        // On s'abonne à l'événement pour gérer les ennemis tués
        enemyKilled.AddListener(EnemyKilled);
    }

    private void Start()
    {
        // On commence à générer la première vague
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        // Si les ennemis ne sont pas en train d'être générés, on ne fait rien
        if (!isSpawning) return;

        // On incrémente le temps depuis le dernier spawn
        timeSinceLastSpawn += Time.deltaTime;

        // Si le temps est écoulé et qu'il reste des ennemis à générer
        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSwpan > 0)
        {
            // On génère un nouvel ennemi
            SpawnEnemy();
            enemiesLeftToSwpan--; // On diminue le nombre d'ennemis restant à générer
            enemiesAlive++; // On augmente le nombre d'ennemis vivants
            timeSinceLastSpawn = 0f; // On réinitialise le temps écoulé
        }

        // Si tous les ennemis ont été tués et qu'il n'en reste plus à générer
        if (enemiesAlive == 0 && enemiesLeftToSwpan == 0)
        {
            // On termine la vague
            EndWave();
        }
    }

    // Cette méthode est appelée chaque fois qu'un ennemi est tué
    private void EnemyKilled()
    {
        enemiesAlive--; // On diminue le nombre d'ennemis vivants
    }

    // Coroutine pour démarrer une nouvelle vague après un délai
    private IEnumerator StartWave()
    {
        // On attend un certain temps avant de commencer la vague
        yield return new WaitForSeconds(timeBetweenWaves);

        // On démarre la génération d'ennemis
        isSpawning = true;
        enemiesLeftToSwpan = baseEnemies; // On réinitialise le nombre d'ennemis à générer
    }

    // Méthode pour finir la vague (lorsque tous les ennemis sont tués)
    private void EndWave()
    {
        isSpawning = false; // On arrête la génération d'ennemis
        timeSinceLastSpawn = 0f; // On réinitialise le temps écoulé
        currentWave++; // On passe à la vague suivante
        StartCoroutine(StartWave()); // On lance la prochaine vague après le délai
    }

    // Méthode pour générer un ennemi
    private void SpawnEnemy()
    {
        // On choisit quel préfabriqué d'ennemi générer (ici, on prend le premier du tableau)
        GameObject prefabToSpawn = enemyPrefabs[0];

        // On instancie l'ennemi à la position de départ du niveau
        Instantiate(prefabToSpawn, LevelManager.main.startpoint.position, Quaternion.identity);
    }

    // Calcul du nombre d'ennemis par vague, en fonction de la difficulté
    private int EnemiesPerWave()
    {
        // La difficulté augmente le nombre d'ennemis à chaque vague
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}

