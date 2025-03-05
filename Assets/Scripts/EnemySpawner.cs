using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject blueEnemyPrefab;
    public GameObject blackEnemyPrefab;
    public Transform[] spawnPoints; // Lugares donde aparecerán los enemigos

    private int enemiesPerWave = 5;
    private float spawnInterval = 0; // Tiempo entre cada spawn
    private float timeBetweenWaves = 5f; // Tiempo entre oleadas
    private int waveNumber = 0;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (waveNumber<3) 
        {
            waveNumber++;
            Debug.Log("Iniciando Oleada #" + waveNumber);

            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnInterval);
            }

            // Esperar hasta que todos los enemigos sean eliminados antes de la siguiente oleada
            yield return StartCoroutine(WaitForEnemiesToBeDefeated());

            Debug.Log("Oleada #" + waveNumber + " terminada. Esperando...");
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // Punto aleatorio
        GameObject enemyPrefab = Random.value > 0.5f ? blueEnemyPrefab : blackEnemyPrefab; // Enemigo aleatorio

        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    IEnumerator WaitForEnemiesToBeDefeated()
    {
        while (FindObjectsOfType<Enemy>().Length > 0) // Mientras haya enemigos vivos
        {
            yield return new WaitForSeconds(1f); // Espera 1 segundo antes de volver a comprobar
        }
    }
}
