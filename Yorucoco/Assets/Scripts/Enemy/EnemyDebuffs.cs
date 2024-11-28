using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMovementNavmesh))]
public class EnemyDebuffs : MonoBehaviour
{
    [HideInInspector]
    public EnemyHealth enemyHealth;

    [HideInInspector]
    public EnemyMovementNavmesh enemyMovement;

    [Header("Enemy Stats")]
    public float OriginalSpeed;

    [Header("Debuff Multipliers")]
    public float MovementSpeedMultiplier = .5f;

    private void Start() {

        enemyHealth = GetComponent<EnemyHealth>();
        enemyMovement = GetComponent<EnemyMovementNavmesh>();

        //Gets and stores the agent's original Speed on Start;
        OriginalSpeed = enemyMovement.agentSpeed;
    }

    public void Slow(float duration) {

        StartCoroutine(SlowCo(duration));
    }

    IEnumerator SlowCo(float duration){

        float slowedSpeed = ScalingCalculation(OriginalSpeed, MovementSpeedMultiplier);
        enemyMovement.agentSpeed = slowedSpeed;
        yield return new WaitForSeconds(duration);
        enemyMovement.agentSpeed = OriginalSpeed;
    }

    public float ScalingCalculation(float value, float multiplier) {
        float result = 0;
        result = value * multiplier;
        print(result);
        return result;
    }
}
