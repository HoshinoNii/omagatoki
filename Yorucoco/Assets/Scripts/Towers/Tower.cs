using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Transform target;

    [Header("Archer Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Field Setup")]
    public string enemyTag = "Enemy";
    public LayerMask EnemyTag; 

    public Transform partToRotate;
    public float turnSpeed = 10f;

    //public GameObject projectilePrefab;

    public PoolIndex projectileType;
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

        //audio.Construction(.3f);

        InvokeRepeating("GetHighestPriority", 0f, 0.5f);
    }

    void GetHighestPriority()
    {

        float shortestDistance = Mathf.Infinity;
        Collider[] enemies = Physics.OverlapSphere(transform.position, range, EnemyTag);
        GameObject nearestEnemy = null;

        GameObject highestPriorityEnemy = null;

        

        foreach (Collider enemy in enemies) 
        {

            GameObject Enemy = enemy.gameObject;
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = Enemy;
            }

            for (int i = 0; i < enemies.Length; i++)
            {
                EnemyHealth Enemys = enemies[i].gameObject.GetComponent<EnemyHealth>();

                if (highestPriorityEnemy != null)
                {
                    //runs a loop in the priority system to find the highest target priority
                    if (Enemys.priority > highestPriorityEnemy.GetComponent<EnemyHealth>().priority)
                    {
                        highestPriorityEnemy = Enemys.gameObject;
                    }
                    else if (Enemys.priority <= highestPriorityEnemy.GetComponent<EnemyHealth>().priority)
                    {
                        highestPriorityEnemy = nearestEnemy;
                    } 
                    if(highestPriorityEnemy.GetComponent<EnemyHealth>().isDead == true){
                        target = null;
                    }
                    
                }
                else
                {
                    highestPriorityEnemy = Enemys.gameObject;
                }
            }
        }

        if (highestPriorityEnemy != null && shortestDistance <= range && highestPriorityEnemy.GetComponent<EnemyHealth>().isDead == false)
        {
            target = highestPriorityEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null || health.isDead)
            return;

        //Target Locking
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            audio.Shooting(.4f);
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }

    void Shoot()
    {
        
        GameObject projectileGO = PoolManager.Instance.SpawnFromPool(projectileType.ToString(), firePoint.position, firePoint.rotation);
        //Debug.Log(projectileGO);
        Projectile projectile = projectileGO.GetComponent<Projectile>();

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
