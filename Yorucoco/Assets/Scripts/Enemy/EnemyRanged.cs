using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    private Transform target;

    [Header("Archer Attributes")]
    public float range = 10f;
    public float fireRate = 0.5f;
    private float fireCountdown = 0f;

    [Header("Field Setup")]
    public string enemyTag = "Tower";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    //public GameObject projectilePrefab;

    public PoolIndex projectileType;
    public Transform firePoint;

    private EnemyMovementNavmesh m_movement;

    [Header("Data")]
    public EnemyData enemyData;
    public EnemyAudio Audio;
    public EnemyHealth Health;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        m_movement = GetComponent<EnemyMovementNavmesh>();

        Audio = GetComponentInChildren<EnemyAudio>();
        Health = GetComponent<EnemyHealth>();
    }

    void UpdateTarget()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;

        GameObject nearestTower = null;

        foreach (GameObject tower in towers)
        {
            float distanceToTower = Vector3.Distance(transform.position, tower.transform.position);
            if (distanceToTower < shortestDistance)
            {
                shortestDistance = distanceToTower;
                nearestTower = tower;

                if (tower.GetComponent<TowerHealth>().isDead) {
                    nearestTower = null;
                }
            }
            
        }

        if (nearestTower != null && shortestDistance <= range)
        {
            target = nearestTower.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null || Health.isDead)
            return;

        //Target Locking
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            m_movement.AttackingState(0.75f);
            Shoot();
            Audio.Shooting(.4f);
            fireCountdown = 1f / fireRate;
            
        }

        fireCountdown -= Time.deltaTime;

    }

    void Shoot()
    {
        //GameObject projectileGO = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        
        GameObject projectileGO = PoolManager.Instance.SpawnFromPool(projectileType.ToString(), firePoint.position, firePoint.rotation);
        //Debug.Log(projectileGO);
        EnemyProjectile projectile = projectileGO.GetComponent<EnemyProjectile>();

        if (projectile != null)
        {
            projectile.Seek(target);
        }
    }

    //Range of Tower
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
