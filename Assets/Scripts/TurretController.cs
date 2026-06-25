using UnityEngine;

public class TurretController : MonoBehaviour {
    [SerializeField] private InputReader input;
    [SerializeField] private Transform castle;
    [SerializeField] private Transform turretHead;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float maxRange = 3f;
    [SerializeField] private float spinSpeed = 180f;

    private Vector2 moveInput;

    private void OnEnable() {
        if(input) input.OnMoveEvent += HandleMoveInput;
    }

    private void HandleMoveInput(Vector2 input) {
       moveInput = input;
       Debug.Log("Turret received input: " + input);
    }

    private void Update() {
        
        SpinTurret();

        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f);

         if (movement.magnitude > 1f) {
            movement.Normalize();
        }

        transform.position += movement * moveSpeed * Time.deltaTime;

        if (!castle) {
            return;
        }

        Vector3 offset = transform.position - castle.position;
        
        if (offset.magnitude > maxRange){
            transform.position = castle.position + offset.normalized * maxRange;
        }
    }

    private void SpinTurret() {
            if (!turretHead) {
                return;
            }

            turretHead.Rotate(0f, 0f, spinSpeed * Time.deltaTime);
        }

    private void OnDisable() {
        if(input) input.OnMoveEvent -= HandleMoveInput;
    }
}
