using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WavesUI : MonoBehaviour
{
    public TextMeshProUGUI EnemyText;
    public static WavesUI Instance;

    private void Awake() {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //This will always be updated ONLY During acton phase else it will be 0/0
        if(GameManager.Instance.CurrentPhase == GameManager.Phases.Action) {
            EnemyText.text = AdvancedWaveSpawnner.Instance.CurrentEnemiesLeft.ToString() +
            "/" + AdvancedWaveSpawnner.Instance.Wave[ActionPhase.Instance.CurrentWave].GetWaveEnemyCount();
        }

        if(GameManager.Instance.CurrentPhase == GameManager.Phases.Preperation) {
            EnemyText.text = "0/0";
        }
          

    }

    public void UpdateWaveCount() {
        
    }
}
