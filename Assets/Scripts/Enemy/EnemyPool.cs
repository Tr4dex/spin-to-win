using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour {
    [SerializeField] private EnemyMovement enemyPrefab;
    [SerializeField] private int poolSize = 30;

    private readonly Queue<EnemyMovement> pool = new();

    private void Awake() {
        for (int i = 0; i < poolSize; i++) {
            EnemyMovement enemy = Instantiate(enemyPrefab, transform);
            enemy.gameObject.SetActive(false);
            pool.Enqueue(enemy);
        }
    }

    public EnemyMovement GetEnemy() {
        if (pool.Count == 0)
            return null;

        EnemyMovement enemy = pool.Dequeue();
        enemy.gameObject.SetActive(true);

        return enemy;
    }

    public void ReturnEnemy(EnemyMovement enemy) {
        enemy.gameObject.SetActive(false);
        pool.Enqueue(enemy);
    }
}