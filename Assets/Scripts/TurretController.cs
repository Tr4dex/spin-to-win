using UnityEngine;

public class TurretController : MonoBehaviour {
    [SerializeField] private InputReader input;
    [SerializeField] private Transform castle;
    [SerializeField] private Transform turretHead;
    [SerializeField] private Transform barrelPivot;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float acceleration = 12f;
    [SerializeField] private float deceleration = 16f;
    [SerializeField] private float maxRange = 3f;
    [SerializeField] private float spinSpeed = 180f;
    [SerializeField] private float fireCooldown = 0.001f;

    private Vector2 moveInput;
    private Vector3 currentVelocity;
    private float nextFireTime;

    private void OnEnable() {
        if(input) {
            input.OnMoveEvent += HandleMoveInput;
            input.OnFireEvent += HandleFireInput;
        }
    }

    private void HandleMoveInput(Vector2 input) {
       moveInput = input;
       //Debug.Log("Turret received input: " + input);
    }

    private void HandleFireInput() {
        if (Time.time < nextFireTime) {
            return;
        }

        if (!firePoint || !projectilePrefab) {
            return;
        }

        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        
        nextFireTime = Time.time + fireCooldown;
    }

    private void Update() {
        
        SpinTurret();

        Vector3 inputDirection = new Vector3(moveInput.x, moveInput.y, 0f);

         if (inputDirection.magnitude > 1f) {
            inputDirection.Normalize();
        }

        Vector3 targetVelocity = inputDirection * moveSpeed;

        float changeRate = inputDirection.sqrMagnitude > 0.01f ? acceleration : deceleration;

        currentVelocity = Vector3.MoveTowards(
            currentVelocity,
            targetVelocity,
            changeRate * Time.deltaTime
        );

        transform.position += currentVelocity * moveSpeed * Time.deltaTime;

        if (!castle) {
            return;
        }

        Vector3 offset = transform.position - castle.position;
        
        if (offset.magnitude > maxRange){
            transform.position = castle.position + offset.normalized * maxRange;
        }
    }

    private void SpinTurret() {
            if (!barrelPivot) {
                return;
            }

            barrelPivot.Rotate(0f, 0f, spinSpeed * Time.deltaTime);
        }

    private void OnDisable() {
        if(input) {
            input.OnMoveEvent -= HandleMoveInput;
            input.OnFireEvent -= HandleFireInput;
        }

    }
}
