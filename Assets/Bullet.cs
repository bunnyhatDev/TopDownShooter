using UnityEngine;

public class Bullet : MonoBehaviour {
    public float bulletDamage;

    private Transform target;
    private float speed = 10f;

    Vector2 dir;

    public void Seek(Transform _target) {
        target = _target;
    }

    void Start() {
        if(target != null) {
            dir = target.position - transform.position;
        }
    }

    void Update() {
        if(target == null) {
            Destroy(gameObject);
            return;
        }

        float disThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= disThisFrame) {
            Debug.Log("hi");
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * disThisFrame, Space.World);
    }

    void HitTarget() {
        Destroy(gameObject);
        return;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy") {
            other.gameObject.GetComponent<Enemy>().health -= (int)bulletDamage;
            Destroy(gameObject);
        }
    }
}
