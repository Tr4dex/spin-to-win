using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class HealthBarUI : MonoBehaviour {
    [SerializeField] private Health health;
    [SerializeField] private Image fillImage;

    [SerializeField] private float visibleDuration = 2f;
    [SerializeField] private float fadeSpeed = 3f;

    private CanvasGroup canvasGroup;
    private float visibleTimer;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();

        if (health == null) health = GetComponentInParent<Health>();

        canvasGroup.alpha = 0f;
    }

    private void OnEnable() {
        if (health != null) health.OnHealthChanged.AddListener(UpdateHealthBar);
    }

    private void OnDisable() {
        if (health != null) health.OnHealthChanged.RemoveListener(UpdateHealthBar);
    }

    private void UpdateHealthBar(int currentHealth, int maxHealth) {
        fillImage.fillAmount = (float)currentHealth / maxHealth;

        canvasGroup.alpha = 1f;
        visibleTimer = visibleDuration;
    }

    private void Update() {
        if (visibleTimer > 0) {
            visibleTimer -= Time.deltaTime;
        } else if (canvasGroup.alpha > 0) {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
        }
    }
}