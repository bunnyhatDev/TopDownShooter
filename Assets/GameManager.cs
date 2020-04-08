using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GAME_STATE {
    ENEMY_WAVE, BOSS_WAVE
}

public class GameManager : MonoBehaviour {
    public Transform[] enemySpawners;
    public GameObject[] enemyTypes;
    public float spawnTimer;

    public GAME_STATE gameState;

    public int killCount;

    [Header("UI Elements")]
    public Slider levelProgress;

    private float cooldown;

    private void Start() {
        cooldown = spawnTimer;

        gameState = GAME_STATE.ENEMY_WAVE;
    }

    private void Update() {
        levelProgress.value = killCount;

        switch(gameState) {
            case GAME_STATE.ENEMY_WAVE:
                if(spawnTimer <= 0) {
                    SpawnEnemy(0, enemySpawners.Length);
                    spawnTimer = cooldown;
                }
                spawnTimer -= Time.deltaTime;

                if(killCount >= 5) {
                    gameState = GAME_STATE.BOSS_WAVE;
                }
                break;

            case GAME_STATE.BOSS_WAVE:
                break;
        }
    }

    void SpawnEnemy(int min, int max) {
        int spawnpoint = Random.Range(min, max);
        int enemy = Random.Range(min, enemyTypes.Length);
        Instantiate(enemyTypes[enemy], enemySpawners[spawnpoint].position, enemySpawners[spawnpoint].rotation);
    }

}
