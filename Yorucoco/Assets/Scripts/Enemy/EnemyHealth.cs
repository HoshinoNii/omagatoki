using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public EnemyData enemyData;

    public float initialHealth;
    public float currentHealth;

    public Image fill;
    public Image deplete;

    private float depleteSpeed = 0.006f;

    public int value; //CASH FROM ENEMIES

    [Header("Models")]
    public GameObject Model;
    public GameObject Canvas;

    [Header("Death")]
    public bool isDead = false;
    public float TimeBeforeFullDeath = 1f;

    [Header("References")]
    public EnemyAudio Audio;
    public TowerBlueprint blueprint;
    UIManager uiManager;
    EnemyHealth enemyHealth;

    [Range (1, 5)]
    public int priority; //Target priority

    void Start()
    {
        Audio = GetComponentInChildren<EnemyAudio>();
        uiManager = UIManager.Instance;
        currentHealth = initialHealth;
        isDead = false;
        enemyHealth = GetComponent<EnemyHealth>();
    }

    
    void Update()
    {
        fill.fillAmount = currentHealth / initialHealth;

        if(deplete.fillAmount > fill.fillAmount)
        {
            deplete.fillAmount -= depleteSpeed;
        }
        else
        {
            deplete.fillAmount = fill.fillAmount;
        }

        

        if (currentHealth < 1)
        {
            isDead = true;
            currentHealth = 0;
        }
        else if (currentHealth > initialHealth)
        {
            isDead = false;
            currentHealth = initialHealth;
        }
    }

    public void TakingDamage(int amount)
    {
        //if it turns out the enemy is already dead it should not fucking keep calling this Cheebye function
        
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die(bool enemyReachedTarget = false)
    {
        if (isDead) return;

        isDead = true;
        if(Model != null && Canvas != null) {
            Model.SetActive(false);
            Canvas.SetActive(false);
        }
        Audio.Death(.25f);
        ProcessData(enemyReachedTarget);
        Invoke("DieInvoke", TimeBeforeFullDeath);      
    }

    public void DieInvoke() {
        Destroy(gameObject);
    }

    public void ProcessData(bool enemyReachedTarget) {

        if (enemyReachedTarget == false)
        {
            uiManager.AddMoney(enemyHealth);
            PlayerStats.Money += value; //player gains money if its destroyed by a turret
        }
        else
            PlayerStats.Lives--; //player loses life if enemy has reached destination

        //WaveSpawner.EnemiesAlive--; 
        Debug.Log(gameObject.name + " has died reeeeeeeeeeeeeeeeee");
        AdvancedWaveSpawnner.Instance.CurrentEnemiesLeft++;
    }
}
