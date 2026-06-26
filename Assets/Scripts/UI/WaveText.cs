using TMPro;
using UnityEngine;

public class WaveText : MonoBehaviour {
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private TextMeshProUGUI waveText;

    private void Awake() {
        waveManager.OnWaveStart.AddListener(UpdateWaveText);
    }

    private void UpdateWaveText(int waveNumber) {
        waveText.text = "Wave " + waveNumber.ToString();
    }
}
