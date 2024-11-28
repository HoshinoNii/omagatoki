using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
    public float initialHealth;
    public float currentHealth;

    public Image fill;
    public Image deplete;
    public Image restore;

    private float depleteSpeed = 0.006f;
    private float recoverSpeed = 0.001f;

    [Header("References")]

    [Header("3D Model")]
    public GameObject Model;
    public GameObject HealthCanvas;

    [Header("Death")]
    public bool isDead; 
    public float TimeBeforeFullDeath = 1f;

    private Tower tower;

    void Start()
    {
        currentHealth = initialHealth;
        tower = GetComponent<Tower>();
        isDead = false;
    }


    void Update()
    {
        /*fill.fillAmount = currentHealth / initialHealth;*/
        restore.fillAmount = currentHealth / initialHealth;

        if (deplete.fillAmount > fill.fillAmount)
        {
            deplete.fillAmount -= depleteSpeed;
        }
        else if (fill.fillAmount < restore.fillAmount)
        {
            fill.fillAmount += recoverSpeed;
        }
        else
        {
            deplete.fillAmount = fill.fillAmount;
            fill.fillAmount = restore.fillAmount;
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
        if (isDead) return;
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Healing(int amount)
    {
        currentHealth += amount;
    }

    public void Die()
    {
        isDead = true;
        Model.SetActive(false);
        HealthCanvas.SetActive(false);
        tower.audio.PlayGeneralAudio(1,.5f);
        Invoke("DieInvoke", TimeBeforeFullDeath);
    }

    public void DieInvoke() {
        Destroy(gameObject);
    }
}
