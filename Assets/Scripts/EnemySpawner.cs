using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public CameraController cameraController;
    public GameObject blueEnemyPrefab;
    public GameObject blackEnemyPrefab;
    public GameObject victoryScreen;
    public TextMeshProUGUI waveMessage;
    public GameObject ground;

    private int enemiesPerWave = 5;
    private float spawnInterval = 0; // Tiempo entre cada spawn
    private float timeBetweenWaves = 5f; 
    private int waveNumber = 0;
    private int maxWaves = 3;
    private float messageTime = 2.5f;

    void Start()
    {
        victoryScreen.SetActive(false);
        waveMessage.text = "Survive all 3 waves!";

        // Esperar a que el mensaje desaparezca antes de iniciar las oleadas
        Invoke(nameof(StartSpawningWaves), messageTime);
        Invoke(nameof(HideWaveMessage), messageTime);
    }

    void StartSpawningWaves()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (waveNumber<maxWaves) 
        {
            waveNumber++;
            
    
            Debug.Log("Iniciando Oleada #" + waveNumber);

            for (int i = 0; i < enemiesPerWave ; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnInterval);
            }

            yield return StartCoroutine(WaitForEnemiesToBeDefeated());
            ShowWaveMessage();

            if (waveNumber!=maxWaves)
            {
                Debug.Log("Oleada #" + waveNumber + " terminada. Esperando...");
                yield return new WaitForSeconds(timeBetweenWaves);
            }
            else
            {
                ShowVictoryScreen();
            }

            enemiesPerWave += enemiesPerWave;
            
        }
        
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject enemyPrefab = Random.value > 0.5f ? blueEnemyPrefab : blackEnemyPrefab;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomSpawnPosition()
    {
        if (ground == null)
        {
            return Vector3.zero;
        }

        BoxCollider groundCollider = ground.GetComponent<BoxCollider>();
        if (groundCollider == null)
        {
            return Vector3.zero;
        }

        Vector3 groundSize = groundCollider.bounds.size;
        Vector3 groundCenter = groundCollider.bounds.center;

        float spawnMargin = 1.5f;

        float spawnX, spawnZ;
        Vector3 spawnPosition = Vector3.zero;

        spawnX = Random.Range(
         groundCenter.x - groundSize.x / 2 + spawnMargin,
         groundCenter.x + groundSize.x / 2 - spawnMargin
     );

        spawnZ = Random.Range(
            groundCenter.z - groundSize.z / 2 + spawnMargin,
            groundCenter.z + groundSize.z / 2 - spawnMargin
        );

        spawnPosition = new Vector3(spawnX, groundCollider.bounds.max.y + 0.1f, spawnZ);

        return spawnPosition;
    }

    IEnumerator WaitForEnemiesToBeDefeated()
    {
        while (FindObjectsOfType<Enemy>().Length > 0) // Mientras haya enemigos vivos
        {
            yield return new WaitForSeconds(1f); // Espera 1 segundo antes de volver a comprobar
        }
    }
    void ShowVictoryScreen()
    {
        Debug.Log("¡VICTORIA! Todas las oleadas completadas.");
        victoryScreen.SetActive(true);
        Time.timeScale = 0f; 
        cameraController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerShooting.isMenuActive = true;

    }

    void ShowWaveMessage()
    {
        if (waveMessage == null) return;

        if (waveNumber < maxWaves-1)
        {
            waveMessage.text = "Wave " + waveNumber + " complete. Next wave soon...";
        }
        else
        {
            waveMessage.text = "Wave " + waveNumber + " complete. Last wave coming...";
        }

        waveMessage.gameObject.SetActive(true); 
        Invoke("HideWaveMessage", messageTime); 
    }

    void HideWaveMessage()
    {
        if (waveMessage != null)
        {
            waveMessage.gameObject.SetActive(false);
        }
    }
}


