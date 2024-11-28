
using UnityEngine;
using System;

[System.Serializable]
public class WaveData
{
    public enum SpawnPoint {
        SpawnArea1,
        SpawnArea2
    }
    public GameObject enemy;
    public int count;
    public float rate;
    public Transform spawnPoint;
       

}
