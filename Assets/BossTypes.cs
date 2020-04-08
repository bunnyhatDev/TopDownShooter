using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ABILITY {
    None, Split, Freeze
}

[CreateAssetMenu(fileName = "Boss", menuName = "Boss Type", order = 1)]
public class BossTypes : ScriptableObject {
    public string bossName;
    public ABILITY ability;
    public float moveSpeed;
    public int health;
}
