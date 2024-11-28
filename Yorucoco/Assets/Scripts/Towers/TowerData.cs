using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Data", menuName = "Tower_Defense/Tower Data")]
public class TowerData : ScriptableObject
{
    public enum TowerType {
        ArcherTower,
        MagicTower,
        HealingTower,
    }

    public TowerType tower;

    public string GetAudioState(string state) {
        Debug.Log(tower.ToString() + " state");
        return tower.ToString() + state;
    }
}
