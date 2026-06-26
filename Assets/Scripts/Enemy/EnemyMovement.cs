using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] private float moveSpeed = 1.5f;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float spinSpeed = 720f;

    private Transform target;

    private bool isKnockedBack;

    public void Initialize(Transform castle) {
        target = castle;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        isKnockedBack = false;
    }

    private void FixedUpdate() {
        if (target == null || isKnockedBack) return;

        Vector2 newPosition = Vector2.MoveTowards(rb.position, target.position, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }

    public void Knockback(Vector2 direction, float force, float duration) {
        StopAllCoroutines();
        StartCoroutine(KnockbackRoutine(direction, force, duration));
    }

    private IEnumerator KnockbackRoutine(Vector2 direction, float force, float duration) {
        isKnockedBack = true;

        rb.linearVelocity = direction * force;
        rb.angularVelocity = spinSpeed;

        yield return new WaitForSeconds(duration);

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.rotation = Quaternion.identity;
        isKnockedBack = false;
    }
}
