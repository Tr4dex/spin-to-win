using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private Health health;

    private void Awake() {
        health ??= GetComponent<Health>();
        health.OnDeath.AddListener(Die);
    }

    private void Die() {
        Destroy(gameObject);
    }
}
