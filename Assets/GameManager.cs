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

    public GAME_STATE gameState;

    public int killCount;
    //public float messageTimer = 2.5f;

    [Header("UI Elements")]
    public Slider levelProgress;
    public TextMeshProUGUI stateMessage;

    private float cooldown;

    private void Start() {
        cooldown = spawnTimer;

        gameState = GAME_STATE.ENEMY_WAVE;
    }

    private void Update() {
        levelProgress.value = killCount;

        switch(gameState) {
            case GAME_STATE.ENEMY_WAVE:
                //DisplayText("Enemy Wave");
                if (spawnTimer <= 0) {
                    SpawnEnemy(0, enemySpawners.Length);
                    spawnTimer = cooldown;
                }
                spawnTimer -= Time.deltaTime;

                if(killCount >= 15) {
                    //DisplayText("Boss Wave");
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
