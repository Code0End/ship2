using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    private int spawnSide = 0;
    private int enemyWaveCount;
    private int wave = 1;

    public float enemySpawnDelay = 1f;
    public float waveSpawnTimer = 10f;
    public int maxEnemies;
    public GameObject enemy;
    public Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        waveSpawnTimer -= Time.deltaTime;

        if (waveSpawnTimer <= 0 && enemyWaveCount <= 0)
        {
            waveSpawnTimer = 1f;
            enemyWaveCount = wave;
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        GameObject newEnemy;

        while (enemyWaveCount > 0)
        {
            Vector3 camPos = cam.transform.position;
            if (spawnSide == 0)
            { // Left
                newEnemy = Instantiate(enemy, new Vector3(camPos.x + Random.Range(-15, -30), 0.5f, camPos.z + Random.Range(-15, 15)), Quaternion.Euler(90, 0, 0));
            }
            else if (spawnSide == 1)
            { // Right
                newEnemy = Instantiate(enemy, new Vector3(camPos.x + Random.Range(15, 30), 0.5f, camPos.z + Random.Range(-15, 15)), Quaternion.Euler(90, 0, 0));
            }
            else if (spawnSide == 2)
            { // Up
                newEnemy = Instantiate(enemy, new Vector3(camPos.x + Random.Range(-15, 15), 0.5f, camPos.z + Random.Range(15, 30)), Quaternion.Euler(90, 0, 0));
            }
            else
            { // Down
                newEnemy = Instantiate(enemy, new Vector3(camPos.x + Random.Range(-15, 15), 0.5f, camPos.z + Random.Range(-15, -30)), Quaternion.Euler(90, 0, 0));
            }

            spawnSide = Random.Range(0, 4);
            newEnemy.transform.parent = gameObject.transform;
            enemyWaveCount--;

            yield return new WaitForSeconds(enemySpawnDelay);
        }
        wave++;
    }
}
