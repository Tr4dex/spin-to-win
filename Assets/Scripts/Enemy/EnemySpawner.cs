using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private Transform castleTransform;

    [SerializeField] private float spawnRadius = 10f;

    public EnemyMovement SpawnEnemy() {
        EnemyMovement enemy = enemyPool.GetEnemy();

        if (enemy == null) return null;

        Vector2 direction = Random.insideUnitCircle.normalized;
        Vector3 spawnPosition = castleTransform.position + (Vector3)(direction * spawnRadius);
        enemy.transform.position = spawnPosition;

        enemy.Initialize(castleTransform);

        enemy.GetComponent<EnemyHealth>().ResetEnemy();
        return enemy;
    }

    public void ReturnEnemy(EnemyMovement enemy) {
        enemyPool.ReturnEnemy(enemy);
    }
}