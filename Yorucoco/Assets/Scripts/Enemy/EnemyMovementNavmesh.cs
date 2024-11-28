using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementNavmesh : MonoBehaviour
{
    public Transform NavMeshTarget;
    NavMeshAgent agent;
    public float agentSpeed;
    public bool agentSpeedSlowed = false;

    //private Animator anim;
    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        //Grab a reference to navmesh agent
        agent = GetComponent<NavMeshAgent>();

        //stores the original Speed Value
        agentSpeed = agent.speed;

        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        agent.SetDestination(NavMeshTarget.position);
        //float dist = agent.remainingDistance;
        
        //agent.speed = agentSpeedSlowed ? agentSlowedSpeed : agentSpeed;
    }
    
    void FixedUpdate() {
        //band aid fix for now
        if (isAttacking) return;
        //when the current debuff counter is more than 0 it will apply the slowed speed!
        agent.speed = agentSpeed;
    }
    //PUBLIC Function that allows the user to set the target of the navmesh
    public void setNavTarget(Transform target) {
        NavMeshTarget = target;
    }
     
    public void AttackingState(float duration)
    {
        StartCoroutine(AttackingStateCo(duration));
    }

    IEnumerator AttackingStateCo(float duration)
    {
        isAttacking = true;
        agent.speed = 0;
        yield return new WaitForSeconds(duration);
        agent.speed = agentSpeed;
        isAttacking = false;
    }
}

