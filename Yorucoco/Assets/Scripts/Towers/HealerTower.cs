using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerTower : MonoBehaviour
{
    private Transform target;

    [Header("Healer Attributes")]
    public float range = 10f;
    public float fireRate = 0.75f;
    private float fireCountdown = 0f;
    public int healing = 20;

    [Header("Field Setup")]
    public string towerTag = "Tower";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("References")]
    public TowerData towerData;
    public TowerAudio audio;
    public TowerHealth health;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponentInChildren<TowerAudio>();
        health = GetComponent<TowerHealth>();
    }


    // Update is called once per frame
    void Update()
    {
        if (health.isDead) return;

        if (fireCountdown <= 0f)
        {
            Heal();
            audio.Shooting(.1f);
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }

    void Heal()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.name);
            if (collider.CompareTag("Tower"))
            {
                Restore(collider.transform);
            }
        }
    }

    void Restore(Transform tower)
    {
        TowerHealth t = tower.GetComponent<TowerHealth>();
        Debug.Log(t);
        if (t != null)
        {
            t.Healing(healing);
        }
    }

    //Range of Tower
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
