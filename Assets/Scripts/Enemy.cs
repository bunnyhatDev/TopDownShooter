using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public EnemyTypes enemyType;
    public int health;
    public TextMesh healthText;
    public float moveSpeed;

    Rigidbody2D m_rb;

    GameManager m_gm;

    private void Start() {
        m_gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        health = enemyType.health;
        moveSpeed = enemyType.moveSpeed;
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        healthText.text = health.ToString();
        if (health <= 0) {
            health = 0;

            if (gameObject.tag == "Enemy") {
                m_gm.levelProgress.value += 1;
            } else {
                m_gm.gameState = GAME_STATE.NEXT_LEVEL;
            }

            Destroy(gameObject);
        } else {
            m_rb.velocity = new Vector2(0f, -moveSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.name == "Deadzone") {
            Destroy(this.gameObject);
        }
    }
}
