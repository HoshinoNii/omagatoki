using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New Sound Library", menuName = "Tower_Defense/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public enum EnemyType {
        EnemyRusher,
        EnemyArcher,
        EnemyTank,
    }

    public EnemyType enemy;

    public string GetAudioState(string state) {
        Debug.Log(enemy.ToString() + " state");
        return enemy.ToString() + state; 
    }
}
