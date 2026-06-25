using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float speed = 8f;
    [SerializeField] private float lifetime = 3f;

    private void Start() {
        Destroy(gameObject, lifetime);
    }

    private void Update() {
        transform.position += transform.up * speed * Time.deltaTime;
    }
}