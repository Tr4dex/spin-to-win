using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    [SerializeField] private int damage = 10;

    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackDuration = 0.4f;

    [SerializeField] private EnemyMovement enemyMovement;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log($"Hit {other.name}");

        CastleHealth castle = other.GetComponent<CastleHealth>();

        if (castle == null) return;

        castle.Health.TakeDamage(damage);
        Debug.Log("Castle Damage: " + damage);

        Vector2 direction = (transform.position - castle.transform.position).normalized;

        enemyMovement.Knockback(direction, knockbackForce, knockbackDuration);
    }
}