using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    [SerializeField] private EnemySpawner spawner;

    [SerializeField] private int enemiesPerWave = 10;
    [SerializeField] private float spawnDelay = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;

    private int currentWave = 1;
    private int aliveEnemies;

    private void Start() {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave() {
        currentWave++;
        aliveEnemies = enemiesPerWave;

        for (int i = 0; i < enemiesPerWave; i++) {
            EnemyMovement enemy = spawner.SpawnEnemy();

            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();

            enemyHealth.OnEnemyDeath.RemoveListener(OnEnemyDeath);
            enemyHealth.OnEnemyDeath.AddListener(OnEnemyDeath);

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void OnEnemyDeath(EnemyHealth enemyHealth) {
        enemyHealth.OnEnemyDeath.RemoveListener(OnEnemyDeath);

        spawner.ReturnEnemy(enemyHealth.GetComponent<EnemyMovement>());

        aliveEnemies--;
        if (aliveEnemies <= 0) {
            StartCoroutine(NextWave());
        }
    }

    private IEnumerator NextWave() {
        yield return new WaitForSeconds(timeBetweenWaves);

        StartCoroutine(StartWave());
    }
}