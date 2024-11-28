﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPooledObject
{

    private Transform target;

    public float speed = 10f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;

    public int damage = 50;
    public Vector3 offset;

    public void Seek(Transform _target)
    {

        target = _target;
    }
    public void OnObjectTaskComplete() {
        gameObject.SetActive(false);
        Debug.Log("Object Sucessfully returned to pool");
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            //if the projectile is not targeting anyone return the projectile back to the pool
            OnObjectTaskComplete();
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

        Vector3 dire = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dire);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * speed).eulerAngles;
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);

    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        OnObjectTaskComplete();
    }

    void Explode()
    {
        bool PlayOnce = true;
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            //Debug.Log(collider.name);
            if (collider.CompareTag("Enemy"))
            {

                ExplosionDamage(collider.transform);
                if(PlayOnce) {
                    PlayOnce = false;
                    collider.GetComponentInChildren<EnemyAudio>().GeneralAudio(0, .3f);
                }
            }
        }
        
    }

    void ExplosionDamage(Transform enemy) {
        EnemyHealth e = enemy.GetComponent<EnemyHealth>();
        Debug.Log(e);
        if (e != null) {
            e.TakingDamage(damage);
        }
        //Destroy(enemy.gameObject);
    }

    void Damage(Transform enemy)
    {
        EnemyHealth e = enemy.GetComponent<EnemyHealth>();
        Debug.Log(e);
        if(e != null)
        {
            e.TakingDamage(damage);
            enemy.GetComponentInChildren<EnemyAudio>().GeneralAudio(1, .3f);
        }
        //Destroy(enemy.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    void Disable() {
        gameObject.SetActive(false);
    }

}
