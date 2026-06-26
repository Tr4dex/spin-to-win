using UnityEngine;

public class CastleHealth : MonoBehaviour {
    [SerializeField] private Health health;

    public Health Health => health;

    private void Awake() {
        health ??= GetComponent<Health>();
        health.OnDeath.AddListener(GameOver);
    }

    private void GameOver() {
        Debug.Log("Game Over");
    }
}
