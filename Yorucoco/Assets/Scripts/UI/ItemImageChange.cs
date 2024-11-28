using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemImageChange : MonoBehaviour
{
    TowerBlueprint blueprint;

    [Header("Image")]
    public Image archerMoneyBoxDefault;
    public Image magicMoneyBoxDefault;
    public Image healingMoneyBoxDefault;
    public Image spellSlowBoxDefault;

    [Header("Sprites")]
    public Sprite MoneyBoxWhite;
    public Sprite MoneyBoxRed;

    // Update is called once per frame
    void Update()
    {
        ArcherItemImageChanges();
        MagicItemImageChanges();
        HealingItemImageChanges();
        SpellSlowItemImageChanges();
    }

    public void ArcherItemImageChanges()
    {
        if (PlayerStats.Money >= 10)
        {
            if (archerMoneyBoxDefault != null)
            {
                archerMoneyBoxDefault = GetComponentInChildren<Image>();

                archerMoneyBoxDefault.sprite = MoneyBoxWhite;
            }
        }
        else
        {
            if (archerMoneyBoxDefault != null)
            {
                archerMoneyBoxDefault = GetComponentInChildren<Image>();

                archerMoneyBoxDefault.sprite = MoneyBoxRed;
            }
        }
    }

    public void MagicItemImageChanges()
    {
        if (PlayerStats.Money >= 25)
        {
            if (magicMoneyBoxDefault != null)
            {
                magicMoneyBoxDefault = GetComponentInChildren<Image>();

                magicMoneyBoxDefault.sprite = MoneyBoxWhite;
            }
        }
        else
        {
            if (magicMoneyBoxDefault != null)
            {
                magicMoneyBoxDefault = GetComponentInChildren<Image>();

                magicMoneyBoxDefault.sprite = MoneyBoxRed;
            }
        }
    }

    public void HealingItemImageChanges()
    {
        if (PlayerStats.Money >= 40)
        {
            if (healingMoneyBoxDefault != null)
            {
                healingMoneyBoxDefault = GetComponentInChildren<Image>();

                healingMoneyBoxDefault.sprite = MoneyBoxWhite;
            }
        }
        else
        {
            if (healingMoneyBoxDefault != null)
            {
                healingMoneyBoxDefault = GetComponentInChildren<Image>();

                healingMoneyBoxDefault.sprite = MoneyBoxRed;
            }
        }
    }

    public void SpellSlowItemImageChanges()
    {
        if (PlayerStats.Money >= 45)
        {
            if (spellSlowBoxDefault != null)
            {
                spellSlowBoxDefault = GetComponentInChildren<Image>();

                spellSlowBoxDefault.sprite = MoneyBoxWhite;
            }
        }
        else
        {
            if (spellSlowBoxDefault != null)
            {
                spellSlowBoxDefault = GetComponentInChildren<Image>();

                spellSlowBoxDefault.sprite = MoneyBoxRed;
            }
        }
    }
}
