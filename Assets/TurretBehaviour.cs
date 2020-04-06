using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour {

    public Transform target;
    public float fireRate;
    public GameObject bulletPrefab;

    private float cooldown = 2f;
    private bool inRange = false;

    void Start() {
        cooldown = fireRate;
    }


    void Update() {
        if(inRange) {
            GetClosestEnemy();
        } else {
            target = null;
        }
        
        if(fireRate <= 0f) {
            Shoot();
            fireRate = cooldown;
        }
        fireRate -= Time.deltaTime;
    }

    void GetClosestEnemy () {
        float distanceToClosestEnemy = Mathf.Infinity;
        Enemy closestEnemy = null;
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();

        foreach(Enemy currentEnemy in allEnemies) {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if(distanceToEnemy < distanceToClosestEnemy) {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }


        if (closestEnemy != null) {
            Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
            target = closestEnemy.transform;
        } else {
            target = null;
        }
    }

    void Shoot() {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet != null) {
            bullet.Seek(target);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.tag == "Enemy") {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.tag == "Enemy") {
            inRange = false;
        }
    }
}
