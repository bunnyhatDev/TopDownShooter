using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public BossTypes bossType;
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
            health = 0;

            Destroy(gameObject);
        } else {
            m_rb.velocity = new Vector2(0f, -moveSpeed);
        }
    }

}
