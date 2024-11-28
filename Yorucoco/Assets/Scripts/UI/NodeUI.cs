using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeUI : MonoBehaviour
{
    public GameObject nodeUI;

    [Header("Upgrade Attributes")]
    public TextMeshProUGUI upgradeCost;
    public Button upgradeButton;

    [Header("Sell Attributes")]
    public TextMeshProUGUI sellCost;

    [Header("References")]
    private Node target;
    Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        nodeUI.SetActive(false);
    }

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            print("Can UPGRADE YOUR MADER " + target.isUpgraded + " " + _target.name);
            upgradeCost.text = "x" + target.blueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            print("Can UPGRADE YOUR MADER " + target.isUpgraded + " " + _target.name);
            upgradeCost.text = "MAX";
            upgradeButton.interactable = false;
        }

        sellCost.text = "x" + target.blueprint.GetSellAmount();

        nodeUI.SetActive(true);
        anim.Play("FadeIn");
        
    }

    private void Update() {
        //as long as it is in action phase the UI will be kept hidden and disabled
        if(GameManager.Instance.CurrentPhase == GameManager.Phases.Action) {
            Hide();
        }
    }
    public void Hide() //Hide NodeUI
    {
        nodeUI.SetActive(false);
    }

    public void Upgrade() //Upgrade Button
    {
        target.UpgradeTower();
        BuildManager.instance.DeselectNode();
    }

    public void Sell() //Sell Button
    {
        target.SellTower();
        BuildManager.instance.DeselectNode();
    }

}
