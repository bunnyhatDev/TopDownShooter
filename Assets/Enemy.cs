using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int health = 100;
    public float moveSpeed;

    Rigidbody2D m_rb;

    private void Start() {
        m_rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if(health <= 0) {
            health = 0;
            Destroy(gameObject);
        } else {
            m_rb.velocity = new Vector2(0f, -moveSpeed);
        }
    }
}
