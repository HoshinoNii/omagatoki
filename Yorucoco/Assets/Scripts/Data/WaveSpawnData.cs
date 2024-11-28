using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Tower_Defense/WaveData", order = 1)]

public class WaveSpawnData : ScriptableObject
{  
    [Serializable]
    public struct WaveData {
        public enum Spawns{
            SpawnArea1,
            SpawnArea2,
        }

        public Spawns SpawnPosition;
        public GameObject enemyToSpawn;
        public int Quantity;
        public float TimeBeforeNextSpawn;
        public float rate;
    }

    public WaveData[] WaveDetails;

    //this function will loop through all the wave details and return the value of the Current Wave's Enemy Count
    public int GetWaveEnemyCount() {
        int result = 0;
        for(int i = 0; i < WaveDetails.Length; i++) {
            result += WaveDetails[i].Quantity;
        }
        return result;
    }
}
