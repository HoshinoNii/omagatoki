using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject tower;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;
    UIManager uiManager;

    public TowerBlueprint blueprint;
    public bool isUpgraded = false;

    public int currentTowerPrice;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
        uiManager = UIManager.Instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseUp()
    {

        if (tower != null)
        {
            buildManager.SelectNode(this);
            //Debug.Log("Can't build there! - Display on screen.");
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }


        //if (PlayerStats.Money < BuildManager.instance.currentTowerPrice) return;

        BuildTowerOn(buildManager.GetTowerToBuild());
        buildManager.RestTowerBuild();
    }

    private void OnMouseEnter()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor; ;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    public void UpgradeTower()
    {
        if (PlayerStats.Money < blueprint.upgradeCost)
        {
            Debug.Log("NOT ENOUGH MONEY TO UPGRADE");
            AudioManager.Instance.Play("Denied");
            return;
        }

        PlayerStats.Money -= blueprint.upgradeCost;

        //Get rid of old tower
        Destroy(tower);

        //Build new upgraded tower
        GameObject _tower = (GameObject)Instantiate(blueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        tower = _tower;

        isUpgraded = true;

        uiManager.DeductUpgradedCost(blueprint);

        Debug.Log("Tower upgraded!");
    }

    public void BuildTowerOn(TowerBlueprint _blueprint)
    {

        if (PlayerStats.Money < _blueprint.cost)
        {
            Debug.Log("Not Enough MONEH!");
            if (Blueprint.Instance != null) Blueprint.Instance.OnObjectTaskCompleted();
            _blueprint = null;
            return;
        }

        PlayerStats.Money -= _blueprint.cost;


        GameObject _tower = (GameObject)Instantiate(_blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        tower = _tower;

        blueprint = _blueprint;

        uiManager.DeductMoney(blueprint);

        //reset the cost after build is confirmed
        currentTowerPrice = 0;

        //kill the blueprint 
        if (Blueprint.Instance != null) Blueprint.Instance.OnObjectTaskCompleted();

        //Debug.Log("Tower Build! Money left:" + PlayerStats.Money);
    }

    public void SellTower()
    {
        PlayerStats.Money += blueprint.GetSellAmount();

        isUpgraded = false;
        //Get rid of tower
        Destroy(tower);

        blueprint = null;
    }

}
