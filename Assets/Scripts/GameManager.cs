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
    //If wave fails at enemy wave or boss wave from them reaching the turret, then level must restart
    RESTART_WAVE, 
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
    public int totalCoins;
    public float exp, dps;
    //public float messageTimer = 2.5f;

    [Header("UI Elements")]
    public Slider levelProgress;
    public TextMeshProUGUI stateMessage;
    public TextMeshProUGUI levelText, counterText, coinsText, expText, dpsText;

    [Header("Shop Menu")]
    public Toggle shopToggle;
    public GameObject shopWindow;

    private float cooldown;
    private int spawnedEnemies;
    
    private void Start() {
        SimplePool.Preload(enemyTypes[0], 10);
        cooldown = spawnTimer;

        gameState = GAME_STATE.ENEMY_WAVE;
    }

    private void Update() {
        //Debug.Log("Kill count = " + killCount + " | " + "GState = " + gameState);

        levelText.text = "World: " + world + '\n' + "Level: " + level;
        coinsText.text = totalCoins.ToString();
        expText.text = exp.ToString();
        //dps = GameObject.FindGameObjectWithTag("Bullet").GetComponent<Bullet>().bulletDamage;
        dpsText.text = dps.ToString() + " dps";

        switch (gameState) {
            case GAME_STATE.ENEMY_WAVE:
                //levelProgress.value = killCount;
                //DisplayText("Enemy Wave");
                counterText.text = "Enemies Remaining: " + (levelProgress.maxValue - levelProgress.value);
                if (spawnedEnemies != levelProgress.maxValue) {
                    spawnTimer -= Time.deltaTime;
                    if (spawnTimer <= 0) {
                        SpawnEnemy(0, enemySpawners.Length);
                        spawnTimer = cooldown;
                    }
                }
                //Debug.Log("level progress: " + levelProgress.value);
                //Debug.Log(spawnedEnemies + " spawned enemies");

                if(levelProgress.value == levelProgress.maxValue) {
                    gameState = GAME_STATE.SPAWN_BOSS;
                }
                break;

            case GAME_STATE.SPAWN_BOSS:
                SpawnBoss();
                break;

            case GAME_STATE.BOSS_WAVE:
                //Damage to boss happens here. If boss HP = 0 then go to Next Wave game state.
                //Debug.Log("Boss getting damaged here!");
                counterText.text = "Boss Incoming!";
                break;

            case GAME_STATE.NEXT_LEVEL:
                //Debug.Log("next enemy wave incoming!!");
                spawnedEnemies = 0;
                level += 1;
                exp += 1;
                if(level == 6) {
                    world += 1;
                    level = 1;
                }
                levelProgress.value = levelProgress.minValue;
                levelProgress.maxValue += 2;
                gameState = GAME_STATE.ENEMY_WAVE;
                break;

            case GAME_STATE.RESTART_WAVE:
                //FIXME: restarting of level just puts the bar at the bottom at 0, enemies still come in from where they were prior to game ending
                Debug.LogWarning("Wave will restart!");
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy")) {
                    SimplePool.Despawn(go);
                }
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("Boss")) {
                    SimplePool.Despawn(go);
                }
                levelProgress.value = 0;
                spawnTimer = cooldown;
                spawnedEnemies = 0;
                gameState = GAME_STATE.ENEMY_WAVE;
                break;
        }

        if(shopToggle.isOn) {
            shopWindow.SetActive(true);
        } else {
            shopWindow.SetActive(false);
        }
    }

    void SpawnEnemy(int min, int max) {
        int spawnpoint = Random.Range(min, max);
        int enemy = Random.Range(min, enemyTypes.Length);

        if(levelProgress.value != levelProgress.maxValue) {
            SimplePool.Spawn(enemyTypes[0], enemySpawners[spawnpoint].position, enemySpawners[spawnpoint].rotation);
            spawnedEnemies += 1;
        }
    }

    void SpawnBoss() {
        //Spawn the type of boss, set it's HP depending on level here.
        //Debug.Log("Boss Wave");
        int boss = Random.Range(0, bossTypes.Length);

        SimplePool.Spawn(bossTypes[boss], enemySpawners[5].position, enemySpawners[5].rotation);
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
