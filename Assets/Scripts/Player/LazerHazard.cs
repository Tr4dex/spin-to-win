using UnityEngine;

public class LaserHazard : MonoBehaviour{
    private enum LaserState {
        Cooldown,
        Tracking,
        Locked,
        Firing
    }
    [SerializeField] private Transform target;
    [SerializeField] private Transform center;

    [SerializeField] private float spawnRadius = 8f;
    [SerializeField] private float laserLength = 16f;

    [SerializeField] private float trackingTime = 1f;
    [SerializeField] private float lockTime = 0.6f;
    [SerializeField] private float fireTime = 0.2f;
    [SerializeField] private float cooldownTime = 2f;

    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private float hitRadius = 0.2f;
    [SerializeField] private float warningWidth = 0.05f;
    [SerializeField] private float firingWidth = 0.1f;
    [SerializeField] private Color trackingColor = Color.orange;
    [SerializeField] private Color firingColor = Color.red;

    private LineRenderer lineRenderer;

    private LaserState state = LaserState.Cooldown;
    private Vector3 laserStartPoint;
    private Vector3 laserEndPoint;
    private float timer;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    private void Start() {
        timer = cooldownTime;
    }

    private void Update() {
        if (!target || !center) {
            return;
        }

        timer -= Time.deltaTime;

        switch (state) {
            case LaserState.Cooldown:
            if (timer <= 0f) {
                StartTracking();
            }
            break;

            case LaserState.Tracking:
            UpdateTrackingLaser();

            if (timer <= 0f) {
                LockLaser();
            }
            break;

            case LaserState.Locked:
            DrawLockedLaser();
                
            if (timer <= 0f) {
                FireLaser();
            }
            break;

            case LaserState.Firing:
            DrawLockedLaser();

            if (timer <= 0f) {
                StopLaser();
            }
            break;
        }
    }

    private void StartTracking() {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        laserStartPoint = center.position + (Vector3)(randomDirection * spawnRadius);

        state = LaserState.Tracking;
        timer = trackingTime;

        SetLineWidth(warningWidth);
        lineRenderer.enabled = true;
        SetLineColor(trackingColor);

        UpdateTrackingLaser();
    }

    private void UpdateTrackingLaser() {
        Vector3 direction = (target.position - laserStartPoint).normalized;
        laserEndPoint = laserStartPoint + direction * laserLength;

        lineRenderer.SetPosition(0, laserStartPoint);
        lineRenderer.SetPosition(1, laserEndPoint);
    }

    private void LockLaser() {
        Vector3 direction = (target.position - laserStartPoint). normalized;
        laserEndPoint = laserStartPoint + direction * laserLength;

        lineRenderer.SetPosition(0, laserStartPoint);
        lineRenderer.SetPosition(1, laserEndPoint);

        state = LaserState.Locked;
        timer = lockTime;
        SetLineWidth(warningWidth);
        DrawLockedLaser();
    }

    private void FireLaser() {
        state = LaserState.Firing;
        timer = fireTime;

        SetLineWidth(firingWidth);
        SetLineColor(firingColor);
        DrawLockedLaser();

        CheckHits();
    }

    private void StopLaser() {
        state = LaserState.Cooldown;
        timer = cooldownTime;

        lineRenderer.enabled = false;
    }

    private void CheckHits() {
        Vector2 origin = laserStartPoint;
        Vector2 direction = (laserEndPoint - laserStartPoint).normalized;
        float distance = Vector2.Distance(laserStartPoint, laserEndPoint);

        RaycastHit2D[] hits = Physics2D.CircleCastAll(
            origin,
            hitRadius,
            direction,
            distance,
            hitLayers
        );

        foreach (RaycastHit2D hit in hits) {
            Debug.Log("Laser hit: " + hit.collider.name);
        }
    }

    private void SetLineColor(Color color) {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
    
    private void DrawLockedLaser() {
        lineRenderer.SetPosition(0, laserStartPoint);
        lineRenderer.SetPosition(1, laserEndPoint);
    }

    private void SetLineWidth(float width) {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }
}