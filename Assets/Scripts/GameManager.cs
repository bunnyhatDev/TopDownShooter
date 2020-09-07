using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GAME_STATE {
    //Enemies spawn here and travel down towards turret
    ENEMY_WAVE, 
    //Once number of enemies have reached to a certain level progression, enemies stop
    // spawning and a Boss will spawn.
    SPAWN_BOSS,
    BOSS_WAVE, 
    //Once boss is defeated, level += 1, give XP, increase enemy count
    // and will change state to enemey wave.
    NEXT_LEVEL, 
    //Pausing a game will timeScale = 0, unpause will put it back in it's previous state
    // [EXPERIMENT]
    PAUSE
}

public class GameManager : MonoBehaviour {
    public Transform[] enemySpawners;
    public GameObject[] enemyTypes;
    public float spawnTimer;
    public GameObject[] bossTypes;

    public GAME_STATE gameState;

    public int world, level;
    public int coins;
    public float exp, dps;
    //public float messageTimer = 2.5f;

    [Header("UI Elements")]
    public Slider levelProgress;
    public TextMeshProUGUI stateMessage;
    public TextMeshProUGUI levelText, coinsText, expText, dpsText;

    private float cooldown;
    private int spawnedEnemies;

    private void Start() {
        cooldown = spawnTimer;

        gameState = GAME_STATE.ENEMY_WAVE;
    }

    private void Update() {
        //Debug.Log("Kill count = " + killCount + " | " + "GState = " + gameState);

        levelText.text = "World: " + world + '\n' + "Level: " + level;
        coinsText.text = coins.ToString();
        expText.text = exp.ToString();
        dpsText.text = dps.ToString() + " dps";

        switch (gameState) {
            case GAME_STATE.ENEMY_WAVE:
                //levelProgress.value = killCount;
                //DisplayText("Enemy Wave");
                if (spawnedEnemies != levelProgress.maxValue) {
                    if (spawnTimer <= 0) {
                        SpawnEnemy(0, enemySpawners.Length);
                        spawnTimer = cooldown;
                    }
                }
                spawnTimer -= Time.deltaTime;
                //Debug.Log("level progress: " + levelProgress.value);
                Debug.Log(spawnedEnemies + " spawned enemies");

                if(levelProgress.value == levelProgress.maxValue) {
                    gameState = GAME_STATE.SPAWN_BOSS;
                }
                break;

            case GAME_STATE.SPAWN_BOSS:
                SpawnBoss();
                break;

            case GAME_STATE.BOSS_WAVE:
                //Damage to boss happens here. If boss HP = 0 then go to Next Wave game state.
                Debug.Log("boss getting damaged here!");
                break;

            case GAME_STATE.NEXT_LEVEL:
                Debug.Log("next enemy wave incoming!!");
                spawnedEnemies = 0;
                level += 1;
                levelProgress.value = levelProgress.minValue;
                levelProgress.maxValue += 2;
                gameState = GAME_STATE.ENEMY_WAVE;
                break;
        }
    }

    void SpawnEnemy(int min, int max) {
        int spawnpoint = Random.Range(min, max);
        int enemy = Random.Range(min, enemyTypes.Length);

        if(levelProgress.value != levelProgress.maxValue) {
            Instantiate(enemyTypes[enemy], enemySpawners[spawnpoint].position, enemySpawners[spawnpoint].rotation);
            spawnedEnemies += 1;
        }
    }

    void SpawnBoss() {
        //Spawn the type of boss, set it's HP depending on level here.
        Debug.Log("Boss Wave");

        int boss = Random.Range(0, bossTypes.Length);

        Instantiate(bossTypes[boss], enemySpawners[5].position, enemySpawners[5].rotation);
        gameState = GAME_STATE.BOSS_WAVE;
    }

    //void DisplayText(string messageType) {
    //    Debug.Log(messageType + " " + messageTimer);

    //    messageTimer -= Time.deltaTime;

    //    if(messageType == "Enemy Wave") {
    //        stateMessage.text = "////////// INCOMING WAVE \\\\\\\\\\";
    //    } else if(messageType == "Boss Wave") {
    //        stateMessage.text = "////////// BOSS WAVE \\\\\\\\\\";
    //    } else if(messageType == "Paused") {
    //        stateMessage.text = "////////// PAUSED \\\\\\\\\\";
    //    }

    //    if(messageTimer <= 0) {
    //        stateMessage.text = null;
    //        messageTimer = 2.5f;
    //    }
    //}

}
