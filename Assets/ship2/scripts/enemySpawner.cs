using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    private int spawnSide = 0;
    // Enemies to spawn per wave
    public int enemyPerWave = 1;
    // Remaining enemies to spawn
    private int enemiesToSpawn;

    // Delay between enemies spawn in wave
    public float enemySpawnDelay = 1f;
    // Delay between each wave
    public float waveSpawnTimer = 10f;
    // Control enemies in scene
    public int maxEnemies = 2;
    public int currentEnemies;
    
    public float damage = 20f;
    public float speed = 2f;

    public Camera cam;

    public int enemiesKilled = 0;
    public int requiredEnemiesKilled = 10;

    public int enemiesPerWaveIncrease;
    public int maxEnemiesIncrease;
    public int enemyDamageIncrease;
    public float enemySpeedIncrease;

    public GameObject[] enemies;

    void Start() {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        waveSpawnTimer -= Time.deltaTime;

        if (waveSpawnTimer <= 0 && enemiesToSpawn >= 0)
        {
            waveSpawnTimer = 1f;
            enemiesToSpawn = enemyPerWave;
            StartCoroutine(SpawnEnemies());
        }

        if(enemiesKilled >= requiredEnemiesKilled) {
            //requiredEnemiesKilled += requiredEnemiesKilled;
            enemiesKilled = 0;

            this.enemyPerWave += enemiesPerWaveIncrease;
            this.maxEnemies += maxEnemiesIncrease;
            this.damage += enemyDamageIncrease;
            this.speed += enemySpeedIncrease;
        }
    }

    private IEnumerator SpawnEnemies() {
        while (enemiesToSpawn > 0) {
            if (currentEnemies >= maxEnemies) {
                yield return new WaitForSeconds(enemySpawnDelay);
                break;
            }

            float prob = Random.Range(0f, 1f);
            int index;

            if(prob <= 0.7f) {
                index = 0;
            } else if(prob <= 0.9f) {
                index = 1;
            } else {
                index = 2;
            }

            GameObject enemy = enemies[index];
            GameObject newEnemy;

            Vector3 camPos = cam.transform.position;
            if (spawnSide == 0) { // Left
                newEnemy = Instantiate(enemy, new Vector3(camPos.x + Random.Range(-15, -30), 0.5f, camPos.z + Random.Range(-15, 15)), Quaternion.Euler(90, 0, 0));
            }
            else if (spawnSide == 1) { // Right
                newEnemy = Instantiate(enemy, new Vector3(camPos.x + Random.Range(15, 30), 0.5f, camPos.z + Random.Range(-15, 15)), Quaternion.Euler(90, 0, 0));
            }
            else if (spawnSide == 2) { // Up
                newEnemy = Instantiate(enemy, new Vector3(camPos.x + Random.Range(-15, 15), 0.5f, camPos.z + Random.Range(15, 30)), Quaternion.Euler(90, 0, 0));
            }
            else { // Down
                newEnemy = Instantiate(enemy, new Vector3(camPos.x + Random.Range(-15, 15), 0.5f, camPos.z + Random.Range(-15, -30)), Quaternion.Euler(90, 0, 0));
            }

            // Set new Enemy move speed and damage
            newEnemy.GetComponent<Enemy>().addDamage(this.damage);
            newEnemy.GetComponent<Enemy>().addMoveSpeed(this.speed);

            currentEnemies++;

            spawnSide = Random.Range(0, 4);
            newEnemy.transform.parent = gameObject.transform;
            enemiesToSpawn--;

            yield return new WaitForSeconds(enemySpawnDelay);
        }
    }
}
