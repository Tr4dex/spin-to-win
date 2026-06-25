using UnityEngine;

public class TurretController : MonoBehaviour {
    [SerializeField] private InputReader input;
    [SerializeField] private Transform castle;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float maxRange = 3f;

    private void OnEnable() {
        if(input) input.OnMoveEvent += HandleMoveInput;
    }

    private void HandleMoveInput(Vector2 input) {
        Debug.Log("Turret" + input);
        transform.position += (Vector3)input * moveSpeed;

        Vector3 offset = transform.position - castle.position;

        if (offset.magnitude > maxRange) {
            transform.position = castle.position + offset.normalized * maxRange;
        }
    }

    private void OnDisable() {
        if(input) input.OnMoveEvent -= HandleMoveInput;
    }
}
