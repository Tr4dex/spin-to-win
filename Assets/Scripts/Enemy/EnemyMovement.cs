using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] private float moveSpeed = 1.5f;

    private Transform target;

    public void Initialize(Transform castle) {
        target = castle;
    }

    private void Update() {
        if (target == null) return;

        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}
