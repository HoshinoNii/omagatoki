using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private TowerBlueprint towerToBuild;
    public GameObject archerTowerPrefab;
    public GameObject magicTowerPrefab;
    public GameObject healerTowerPrefab;

    //public bool isUpgraded = false;

    public int currentTowerPrice;

    //Node
    private Node selectedNode;
    public NodeUI nodeUI;

    //Roadblock
    private Roadblock roadblock;
    public RoadblockUI roadblockUI;

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //DeselectNode();
        }
    }

    private void Awake()
    {

        if (instance != null)
        {
            Debug.LogError("More than one BuildManager!");
        }
        instance = this;
    }

    public bool CanBuild { get { return towerToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= towerToBuild.cost; } }

    //public void BuildTowerOn(Node node, TowerBlueprint blueprint)
    //{

    //    if (PlayerStats.Money < blueprint.cost)
    //    {
    //        Debug.Log("Not Enough MONEH!");
    //        if (Blueprint.Instance != null) Blueprint.Instance.OnObjectTaskCompleted();
    //        towerToBuild = null;
    //        return;
    //    }

    //    PlayerStats.Money -= blueprint.cost;

    //    GameObject tower = (GameObject)Instantiate(blueprint.prefab, node.GetBuildPosition(), Quaternion.identity);
    //    node.tower = tower;

    //    towerToBuild = blueprint;

    //    //reset the cost after build is confirmed
    //    currentTowerPrice = 0;

    //    //kill the blueprint 
    //    if (Blueprint.Instance != null) Blueprint.Instance.OnObjectTaskCompleted();

    //    //Debug.Log("Tower Build! Money left:" + PlayerStats.Money);
    //}

    public void RestTowerBuild()
    {
        towerToBuild = null;
    }

    public void SelectTowerToBuild(TowerBlueprint tower)
    {
        towerToBuild = tower;
        currentTowerPrice = tower.cost;

        DeselectNode();
    }

    public void SelectRoadBlockToBuild(Roadblock _roadblock)
    {
        roadblock = _roadblock;

        DeselectNode();
    }

    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        towerToBuild = null; 

        //only allow the interaction of the node when it is during a preperation phase
        if(GameManager.Instance.CurrentPhase == GameManager.Phases.Preperation) {
            nodeUI.SetTarget(node);
        }
        
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public TowerBlueprint GetTowerToBuild()
    {
        return towerToBuild;
    }

    public void DeselectRoadBlock()
    {
        roadblock = null;
        //roadblockUI.Hide();
    }

    public void SelectRoadBlock(Roadblock _roadblock)
    {
        if(roadblock == _roadblock)
        {
            DeselectRoadBlock();
            return;
        }

        roadblock = _roadblock;

       // roadblockUI.SetRoadblockTarget(_roadblock);
    }

    //public void UpgradeTower(Node node)
    //
    //    if(PlayerStats.Money < towerToBuild.upgradeCost)
    //    {
    //        Debug.Log("NOT ENOUGH MONEY TO UPGRADE");
    //        return;
    //    }

    //    PlayerStats.Money-= towerToBuild.upgradeCost;

    //    //Get rid of old tower
    //    Destroy(node.tower);

    //    //Build new upgraded tower
    //    GameObject tower = (GameObject)Instantiate(towerToBuild.upgradedPrefab, node.GetBuildPosition(), Quaternion.identity);
    //    node.tower = tower;

    //    isUpgraded = true;

    //    Debug.Log("Tower upgraded!");
    //}
}
