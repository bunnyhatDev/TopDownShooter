using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy Type", order = 1)]
public class EnemyTypes : ScriptableObject {
    public string enemyName;
    public float moveSpeed;
    public int health;
    public int coins;
}
