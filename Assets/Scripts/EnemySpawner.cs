using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private EnemyMovement enemyPrefab;
    [SerializeField] private Transform castleTransform;

    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float spawnInterval = 2f;

    private float timer;

    private void Update() {
        timer += Time.deltaTime;

        if(timer >= spawnInterval) {
            timer = 0;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy() {
        Vector2 direction = Random.insideUnitCircle.normalized;

        Vector3 spawnPosition = castleTransform.position + (Vector3)(direction * spawnRadius);

        EnemyMovement enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, this.transform);

        enemy.Initialize(castleTransform);
    }
}
