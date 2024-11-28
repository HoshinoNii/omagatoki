using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PreperationPhase))]
[RequireComponent(typeof (UIManager))]
[RequireComponent(typeof (ActionPhase))]

public class GameManager : MonoBehaviour
{
    public enum Phases {
        Preperation,
        Action,
        GameWin,
        GameLose,
    }
    public Phases CurrentPhase;

    public float CountdownBeforeActionPhaseStart;
    public bool PathFound;
    public static GameManager Instance;

    public GameObject[] nodes;

    private void Awake() {  
        Instance = this;
        CurrentPhase = Phases.Preperation;
        
    }

    
    // Start is called before the first frame update
    void Start()
    {
        CurrentPhase = Phases.Preperation;
        WavesUI.Instance.UpdateWaveCount();

    }
    // Update is called once per frame
    void Update()
    {
        //Switch the states for different phases
        switch (CurrentPhase) {

            case Phases.Preperation:
                PreperationPhase.Instance.StartPreperationPhase();
                CheckPathStatus();
                break;

            case Phases.Action:
                ActionPhase.Instance.StartActionPhase();
                break;

            case Phases.GameWin:
                GameWin();
                break;

            case Phases.GameLose:
                GameLose();
                break;
        }

        if (ActionPhase.Instance.CurrentWave >= AdvancedWaveSpawnner.Instance.Wave.Length && PlayerStats.Lives > 0) {
            //when all waves are done
            CurrentPhase = Phases.GameWin;
        }

        if (PlayerStats.Lives <= 0)
        {
            PlayerStats.Lives = 0;
            CurrentPhase = Phases.GameLose;
        }
    }

    //this loop will run constantly checking if the path exists and when it returns both as true it will run;
    void CheckPathStatus() {
        int CheckNodeStatus = 0;
         for(int i = 0; i < nodes.Length; i++) {
             PathPlotting node = nodes[i].GetComponent<PathPlotting>();
             if(node.PathLocated == false) {
                 CheckNodeStatus --;
             } else 
                CheckNodeStatus++;
         }

        if(CheckNodeStatus >= nodes.Length)
            PathFound = true;
        else
            PathFound = false;
    }

    private void GameLose()
    {
        Time.timeScale = 0f;
        print("Player Lost");
        //do UI Stuff here for UI Manager
        UIManager.Instance.gameOverScreen.gameObject.SetActive(true);

        //Other Stuff that might need to be disabled etc
        ActionPhase.Instance.CurrentWave = 0;//this can maybe be used as save data?
    }

    public void GameWin()
    {
        Time.timeScale = 0f;
        print("Player Win");
        //do UI Stuff here for UI Manager
        UIManager.Instance.winScreen.gameObject.SetActive(true);

        //Other Stuff that might need to be disabled etc
        ActionPhase.Instance.CurrentWave = 0;
    }
    public void ChangePhase(bool SwitchToAction = false) {
        if(SwitchToAction){
            CurrentPhase = Phases.Preperation;
        }
        else {
            if(PathFound) {
                AudioManager.Instance.Play("ActionPhaseStart");
                CurrentPhase = Phases.Action;
            }
            else {
                Debug.LogWarning("WARNING THERE IS NO PATH FOR THE ENEMY ");
                AudioManager.Instance.PlayMultiple("Denied");
            }
                
        }
    }
}
