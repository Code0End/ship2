using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    private int spawnSide = 0;
    // Enemies to spawn per wave
    private int enemyPerWave = 1;
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

    public GameObject enemy;
    public Camera cam;

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
    }

    private IEnumerator SpawnEnemies() {
        while (enemiesToSpawn > 0) {
            if(currentEnemies >= maxEnemies) {
                yield return new WaitForSeconds(enemySpawnDelay);
                break;
            }
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
            //newEnemy.GetComponent<Enemy>().setDamage(this.damage);
            //newEnemy.GetComponent<Enemy>().setMoveSpeed(this.speed);

            currentEnemies++;

            spawnSide = Random.Range(0, 4);
            newEnemy.transform.parent = gameObject.transform;
            enemiesToSpawn--;

            yield return new WaitForSeconds(enemySpawnDelay);
        }
    }

    public void SetEnemyPerWave(int enemies) {
        this.enemyPerWave = enemies;
    }

    public void SetMaxEnemies(int enemies) {
        this.maxEnemies = enemies;
    }

    public void SetDamage(int damage) {
        this.damage = damage;
    }

    public void SetSpeed(int speed) {
        this.speed = speed;
    }
}
