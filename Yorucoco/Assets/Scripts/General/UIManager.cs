using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Tower Buttons")]
    public Button archertowerButton;
    public Button magictowerButton;
    public Button healingtowerButton;

    [Header("Spell Buttons")]
    public Button spellSlowButton;

    [Header("UI Buttons")]
    public GameObject TowerButtonsCanvas;
    public GameObject SpellButtonsCanvas;
    public GameObject StartActionPhaseButton;

    [Header("UI Interfaces")]
    public GameObject ActionPhaseUIElement;
    public GameObject PreperationPhaseUIElement;

    public GameObject PauseScreen;
    //public GameObject DeathScreen;

    [Header("Canvas UI")]
    public GameObject NodeUI;
    public GameObject RoadBlockUI;

    [Header("Game Over")]
    public GameObject gameOverScreen;

    [Header("Level Completed")]
    public GameObject winScreen;

    [Header("Deduct Money UI")]
    public TextMeshProUGUI deductMoneyText;
    public Animator deductMoneyCanvas;

    [Header("Add Money UI")]
    public TextMeshProUGUI addMoneyText;
    public Animator addMoneyCanvas;
    public EnemyHealth enemyHealth;

    #region singleton
    public static UIManager Instance;
    private void Awake() {
        Instance = this;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        deductMoneyCanvas.GetComponent<Animator>();
        addMoneyCanvas.GetComponent<Animator>();

        if(enemyHealth != null)
        enemyHealth.GetComponent<EnemyHealth>();
    }

    public void PreperationPhaseLargeText(bool enabled = true)
    {
        PreperationPhaseUIElement.gameObject.SetActive(enabled);
    }

    public void PreperationPhaseLargeTextDisable()
    {
        PreperationPhaseLargeText(false);
    }

    public void ActionPhaseLargeText(bool enabled = true)
    {
        ActionPhaseUIElement.gameObject.SetActive(enabled);
    }
    public void ActionPhaseLargeTextDisable()
    {
        ActionPhaseLargeText(false);
    }

    public void PreperationPhaseUI() {
        TriggerButtonStateTowers(true);
        TriggerButtonStateSpells(false);
        
        StartActionPhaseButton.gameObject.SetActive(true);
        ActionPhaseUIElement.gameObject.SetActive(false); 
    }

    public void ActionPhaseUI() {
        TriggerButtonStateTowers(false);
        TriggerButtonStateSpells(true);
        
        StartActionPhaseButton.gameObject.SetActive(false);
        PreperationPhaseUIElement.gameObject.SetActive(false);
    }

    void TriggerButtonStateTowers(bool enabled = true) {
        TowerButtonsCanvas.gameObject.SetActive(enabled);
    }

    void TriggerButtonStateSpells(bool enabled = true) {
        SpellButtonsCanvas.gameObject.SetActive(enabled);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentPhase == GameManager.Phases.GameWin || GameManager.Instance.CurrentPhase == GameManager.Phases.GameLose)
            return;

        ItemPriceCheck();

    }

    public void ItemPriceCheck()
    {
        if (PlayerStats.Money >= 10)
        {
            archertowerButton.interactable = true; //If player money has more or equals to 10, enable archertower button
        }
        else
        {
            archertowerButton.interactable = false; //If player money has less than 10, disable archertower button
        }

        if (PlayerStats.Money >= 25)
        {
            magictowerButton.interactable = true; //If player money has more or equals to 25, enable magictower button
        }
        else
        {
            magictowerButton.interactable = false; //If player money has less than 25, disable magictower button
        }

        if (PlayerStats.Money >= 40)
        {
            healingtowerButton.interactable = true; //If player money has more or equals to 40, enable healingtower button
        }
        else
        {
            healingtowerButton.interactable = false; //If player money has less than 40, disable healingtower button
        }

        if (PlayerStats.Money >= 45)
        {
            spellSlowButton.interactable = true; //If player money has more or equals to 45, enable spellSlowButton button
        }
        else
        {
            spellSlowButton.interactable = false; //If player money has less than 45, disable spellSlowButton button
        }

    }

    public void EndGameUI()
    {
        gameOverScreen.SetActive(true);
        Debug.Log("Game Over");
    }

    public void DeductMoney(TowerBlueprint blueprint)
    {
        deductMoneyText.text = "-" + blueprint.cost.ToString();

        deductMoneyCanvas.Play("DeductMoney_FadeOut");
    }

    public void DeductUpgradedCost(TowerBlueprint blueprint)
    {
        deductMoneyText.text = "-" + blueprint.upgradeCost.ToString();

        deductMoneyCanvas.Play("DeductMoney_FadeOut");
    }

    public void AddMoney(EnemyHealth enemyHealth)
    {
        addMoneyText.text = "+" + enemyHealth.value.ToString();

        addMoneyCanvas.Play("AddMoney_FadeOut");
    }

}
