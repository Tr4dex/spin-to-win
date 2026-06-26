using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private Health health;

    public UnityEvent<EnemyHealth> OnEnemyDeath;

    private void Awake() {
        health ??= GetComponent<Health>();

        health.OnDeath.AddListener(Die);
    }

    public void ResetEnemy() {
        health.ResetHealth();
    }

    private void Die() {
        OnEnemyDeath?.Invoke(this);
    }

    public void Kill() {
        health.TakeDamage(health.CurrentHealth);
    }
}