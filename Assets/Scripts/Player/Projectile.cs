using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float speed = 8f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 50;

    private void Start() {
        Destroy(gameObject, lifetime);
    }

    private void Update() {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        EnemyHealth enemy = other.GetComponentInParent<EnemyHealth>();

        if(!enemy) {
            return;
        }

        Health health = other.GetComponentInParent<Health>();

        if (!health) {
            return;
        }

        health.TakeDamage(damage);
        Destroy(gameObject);
    }
}