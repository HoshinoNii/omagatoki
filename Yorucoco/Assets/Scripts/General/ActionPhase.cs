using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPhase : MonoBehaviour
{
    public static bool ActionIsActive;
    public int CurrentWave = 0;

    public static ActionPhase Instance;
    private void Awake() {
        Instance = this;
        ActionIsActive = false;
    }

    public void StartActionPhase() {

        UIManager.Instance.ActionPhaseUI();
        UIManager.Instance.Invoke("ActionPhaseLargeTextDisable", 1.5f);

        if (ActionIsActive) return;
        ActionIsActive = true;

        //UI
        
        UIManager.Instance.ActionPhaseLargeText();

        //Audio
        AudioManager.Instance.Stop("PreperationPhaseBGM");
        AudioManager.Instance.Play("ActionPhaseBGM");

        //Enable Spells

        //Spawn Start
        Countdown();
    }

    void Countdown() {
        StartCoroutine(CountdownCo());
    }
    IEnumerator CountdownCo() {
        //wait for 3 seconds before we start
        yield return new WaitForSeconds(3f);
        AdvancedWaveSpawnner.Instance.Spawn(CurrentWave);
    }
}
