using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour {
    [SerializeField] private EnemySpawner spawner;

    [SerializeField] private int startingEnemies = 5;
    [SerializeField] private int enemiesIncreasePerWave = 2;
    [SerializeField] private float spawnDelay = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private int bossWave = 5;

    public UnityEvent<int> OnWaveStart;
    public UnityEvent OnGameWin;

    private int currentWave = 0;
    private int aliveEnemies;

    private void Start() {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave() {
        currentWave++;
        OnWaveStart?.Invoke(currentWave);

        if (currentWave == bossWave) {
            aliveEnemies = 1;
            EnemyMovement boss = spawner.SpawnBoss();
            if (boss != null) {
                EnemyHealth bossHealth = boss.GetComponent<EnemyHealth>();
                bossHealth.OnEnemyDeath.AddListener(OnBossDeath);
            }
        } else {
            // Formula to increase enemies: 5, then 7, then 9, etc.
            int enemiesToSpawn = startingEnemies + ((currentWave - 1) * enemiesIncreasePerWave);
            aliveEnemies = enemiesToSpawn;

            for (int i = 0; i < enemiesToSpawn; i++) {
                EnemyMovement enemy = spawner.SpawnEnemy();
                if (enemy != null) {
                    EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                    enemyHealth.OnEnemyDeath.RemoveListener(OnEnemyDeath);
                    enemyHealth.OnEnemyDeath.AddListener(OnEnemyDeath);
                }
                yield return new WaitForSeconds(spawnDelay);
            }
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

    private void OnBossDeath(EnemyHealth bossHealth) {
        bossHealth.OnEnemyDeath.RemoveListener(OnBossDeath);

        Destroy(bossHealth.gameObject);

        aliveEnemies--;
        if (aliveEnemies <= 0) {
            Debug.Log("Boss defeated! You win!");
            OnGameWin?.Invoke();
        }
    }

    private IEnumerator NextWave() {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(StartWave());
    }
}