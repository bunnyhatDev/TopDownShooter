using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Transform[] enemySpawners;
    public GameObject[] enemyTypes;
    public float spawnTimer;

    private float cooldown;

    private void Start() {
        cooldown = spawnTimer;
    }

    private void Update() {
        if(spawnTimer <= 0) {
            SpawnEnemy(0, enemySpawners.Length);
            spawnTimer = cooldown;
        }
        spawnTimer -= Time.deltaTime;
        
    }

    void SpawnEnemy(int min, int max) {
        int spawnpoint = Random.Range(min, max);
        int enemy = Random.Range(min, enemyTypes.Length);
        Instantiate(enemyTypes[enemy], enemySpawners[spawnpoint].position, enemySpawners[spawnpoint].rotation);
    }

}
