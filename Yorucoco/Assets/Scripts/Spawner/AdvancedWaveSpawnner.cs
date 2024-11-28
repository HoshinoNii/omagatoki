using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedWaveSpawnner : MonoBehaviour
{
    public WaveSpawnData[] Wave;
    public Transform EnemyDestination;
    public int EnemiesLeft;
    public int CurrentEnemiesLeft;

    public bool WaveStarted = false;

    public Transform[] SpawnPositions;

    public float timeBetweenWaves = 5f;
    private float countdown = .5f;


    #region singleton
    public static AdvancedWaveSpawnner Instance;

    private void Awake() {
        Instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        ResetEnemiesLeft();
        //Spawn(ActionPhase.Instance.CurrentWave);
    }

    public void Spawn(int currentWave) {
        StartCoroutine(SpawnCo(currentWave));
    }

    IEnumerator SpawnCo( int currentWave = 0)
    {
        WaveStarted = true;

        //Retrieve the information of the current wave
        WaveSpawnData waveBatch = Wave[currentWave];

        ResetEnemiesLeft();
        EnemiesLeft = waveBatch.GetWaveEnemyCount();

        Debug.Log(waveBatch.GetWaveEnemyCount());
        //get the wave details within the scriptable Object
        for (int i = 0; i < waveBatch.WaveDetails.Length; i++)
        {
            for(int j = 0; j < waveBatch.WaveDetails[i].Quantity; j ++) {

                Transform chosenSpawnPosition = null;
                switch(waveBatch.WaveDetails[i].SpawnPosition.ToString()) {
                
                    case "SpawnArea1":
                    chosenSpawnPosition = SpawnPositions[0];
                    break;

                    case "SpawnArea2":
                    chosenSpawnPosition = SpawnPositions[1];
                    break;
                }

                if(chosenSpawnPosition != null) {
                    SpawnEnemy(waveBatch.WaveDetails[i].enemyToSpawn, chosenSpawnPosition);
                    yield return new WaitForSeconds(1f / waveBatch.WaveDetails[i].rate);
                }
            }
            
            yield return new WaitForSeconds(waveBatch.WaveDetails[i].TimeBeforeNextSpawn);
        }
        
    }

    private void FixedUpdate() {

        Debug.Log(CurrentEnemiesLeft + "  " + EnemiesLeft);

        if(CurrentEnemiesLeft >= EnemiesLeft && WaveStarted == true) {
            WaveStarted = false;
            ActionPhase.Instance.CurrentWave++;
            ActionPhase.ActionIsActive = false;
            PreperationPhase.PreperationIsActive = false;
            CurrentEnemiesLeft = 0;
            GameManager.Instance.CurrentPhase = GameManager.Phases.Preperation;
            WavesUI.Instance.UpdateWaveCount();
            print("Run Preperation Phase State");
        }
    }
    void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        //EnemiesAlive++;
        EnemyMovementNavmesh enemyNav = enemy.GetComponent<EnemyMovementNavmesh>();

        if(enemyNav != null) {
            enemyNav.setNavTarget(EnemyDestination);
        }
    }

    void ResetEnemiesLeft() {
        //this will clear out both values allowing for a clean slate.
        EnemiesLeft = 0;
        CurrentEnemiesLeft = 0;
    }
}
