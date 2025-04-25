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
        // On s'abonne � l'�v�nement pour g�rer les ennemis tu�s
        enemyKilled.AddListener(EnemyKilled);
    }

    private void Start()
    {
        // On commence � g�n�rer la premi�re vague
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        // Si les ennemis ne sont pas en train d'�tre g�n�r�s, on ne fait rien
        if (!isSpawning) return;

        // On incr�mente le temps depuis le dernier spawn
        timeSinceLastSpawn += Time.deltaTime;

        // Si le temps est �coul� et qu'il reste des ennemis � g�n�rer
        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSwpan > 0)
        {
            // On g�n�re un nouvel ennemi
            SpawnEnemy();
            enemiesLeftToSwpan--; // On diminue le nombre d'ennemis restant � g�n�rer
            enemiesAlive++; // On augmente le nombre d'ennemis vivants
            timeSinceLastSpawn = 0f; // On r�initialise le temps �coul�
        }

        // Si tous les ennemis ont �t� tu�s et qu'il n'en reste plus � g�n�rer
        if (enemiesAlive == 0 && enemiesLeftToSwpan == 0)
        {
            // On termine la vague
            EndWave();
        }
    }

    // Cette m�thode est appel�e chaque fois qu'un ennemi est tu�
    private void EnemyKilled()
    {
        enemiesAlive--; // On diminue le nombre d'ennemis vivants
    }

    // Coroutine pour d�marrer une nouvelle vague apr�s un d�lai
    private IEnumerator StartWave()
    {
        // On attend un certain temps avant de commencer la vague
        yield return new WaitForSeconds(timeBetweenWaves);

        // On d�marre la g�n�ration d'ennemis
        isSpawning = true;
        enemiesLeftToSwpan = baseEnemies; // On r�initialise le nombre d'ennemis � g�n�rer
    }

    // M�thode pour finir la vague (lorsque tous les ennemis sont tu�s)
    private void EndWave()
    {
        isSpawning = false; // On arr�te la g�n�ration d'ennemis
        timeSinceLastSpawn = 0f; // On r�initialise le temps �coul�
        currentWave++; // On passe � la vague suivante
        StartCoroutine(StartWave()); // On lance la prochaine vague apr�s le d�lai
    }

    // M�thode pour g�n�rer un ennemi
    private void SpawnEnemy()
    {
        // On choisit quel pr�fabriqu� d'ennemi g�n�rer (ici, on prend le premier du tableau)
        GameObject prefabToSpawn = enemyPrefabs[0];

        // On instancie l'ennemi � la position de d�part du niveau
        Instantiate(prefabToSpawn, LevelManager.main.startpoint.position, Quaternion.identity);
    }

    // Calcul du nombre d'ennemis par vague, en fonction de la difficult�
    private int EnemiesPerWave()
    {
        // La difficult� augmente le nombre d'ennemis � chaque vague
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}

