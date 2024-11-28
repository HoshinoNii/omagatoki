using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Vector3 positionOffset;

    public TowerBlueprint archerTower;
    public TowerBlueprint magicTower;
    public TowerBlueprint healingTower;
    public TowerBlueprint slowSpell;

    [Header("Optional")]
    public GameObject spellSlow;

    BuildManager buildManager;
    public TowerBlueprint _blueprint;
    private Blueprint spellBlueprint;


    void Start()
    {
        buildManager = BuildManager.instance;

    }

    public void SelectArcherTower() //Archer Tower
    {
        if (Blueprint.Instance == null)
        {
            Debug.Log("Archer Tower Selected!");
            buildManager.SelectTowerToBuild(archerTower);
        }
    }

    public void SelectMagicTower() //Magic Tower
    {
        if (Blueprint.Instance == null)
        {
            Debug.Log("Magic Tower Selected!");
            buildManager.SelectTowerToBuild(magicTower);
        }
    }

    public void SelectHealingTower() //Healing Tower
    {
        if (Blueprint.Instance == null)
        {
            Debug.Log("Healing Tower Selected!");
            buildManager.SelectTowerToBuild(healingTower);
        }
    }
}
