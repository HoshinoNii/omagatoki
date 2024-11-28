using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreperationPhase : MonoBehaviour
{
    public static bool PreperationIsActive;
    public static PreperationPhase Instance;
    private void Awake() {
        Instance = this;
        PreperationIsActive = false;
    }
    
    // Start is called before the first frame update
    private void Start() {
        
    }
    public void StartPreperationPhase() {
        UIManager.Instance.PreperationPhaseUI();
        UIManager.Instance.Invoke("PreperationPhaseLargeTextDisable", 3f);
        if (PreperationIsActive) return;
        
        PreperationIsActive = true;
        //UI
        UIManager.Instance.PreperationPhaseLargeText();

        //audio
        AudioManager.Instance.Stop("ActionPhaseBGM");
        AudioManager.Instance.Play("PreperationPhaseBGM");

        //Disable Spells
    }
}
