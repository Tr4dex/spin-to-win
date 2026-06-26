using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private Transform castleTransform;

    [SerializeField] private float spawnRadius = 10f;

    [SerializeField] private EnemyMovement bossPrefab;

    public EnemyMovement SpawnEnemy() {
        EnemyMovement enemy = enemyPool.GetEnemy();

        if (enemy == null) return null;

        PlaceAndInitialize(enemy);
        return enemy;
    }

    public EnemyMovement SpawnBoss() {
        if (bossPrefab == null) return null;

        EnemyMovement boss = Instantiate(bossPrefab);
        PlaceAndInitialize(boss);
        return boss;
    }

    private void PlaceAndInitialize(EnemyMovement enemy) {
        Vector2 direction = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = castleTransform.position + (Vector3)(direction * spawnRadius);

        enemy.transform.position = spawnPosition;
        enemy.Initialize(castleTransform);
        enemy.GetComponent<EnemyHealth>().ResetEnemy();
    }

    public void ReturnEnemy(EnemyMovement enemy) {
        enemyPool.ReturnEnemy(enemy);
    }
}