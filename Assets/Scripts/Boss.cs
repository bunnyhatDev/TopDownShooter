using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public EnemyTypes bossType;
    public int health;
    public TextMesh healthText;
    public float moveSpeed;

    Rigidbody2D m_rb;

    GameManager m_gm;

    private void Start() {
        m_gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        health = bossType.health;
        moveSpeed = bossType.moveSpeed;
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        healthText.text = health.ToString();
        if(health <= 0) {
            gameObject.tag = "Boss";
            health = 0;

            SimplePool.Despawn(gameObject);
            m_gm.gameState = GAME_STATE.NEXT_LEVEL;
        } else {
            gameObject.tag = "ActiveBoss";
            m_rb.velocity = new Vector2(0f, -moveSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name == "Deadzone") {
            health = bossType.health;
            m_gm.gameState = GAME_STATE.RESTART_WAVE;
        }
    }

}
