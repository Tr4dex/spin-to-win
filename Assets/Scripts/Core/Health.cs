using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {
    [SerializeField] private int maxHealth = 100;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;

    public UnityEvent<int, int> OnHealthChanged;
    public UnityEvent OnDamaged;
    public UnityEvent OnHealed;
    public UnityEvent OnDeath;

    private bool isDead;

    private void Awake() {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        if (isDead || damage <= 0)
            return;

        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);

        OnDamaged?.Invoke();
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

        if (CurrentHealth == 0) {
            isDead = true;
            OnDeath?.Invoke();
        }
    }

    public void Heal(int amount) {
        if (isDead || amount <= 0)
            return;

        CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);

        OnHealed?.Invoke();
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    public void SetMaxHealth(int newMax, bool healToFull = true) {
        maxHealth = Mathf.Max(1, newMax);

        if (healToFull)
            CurrentHealth = maxHealth;
        else
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }

    public void ResetHealth() {
        isDead = false;
        CurrentHealth = maxHealth;

        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
    }
}