using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestination : MonoBehaviour
{

    //destroy enemy when it collides with destination
    void OnTriggerEnter(Collider collision) {
        if(collision.CompareTag("Enemy")) {
            Debug.Log("Enemy has reached sire");
            collision.GetComponent<EnemyHealth>().Die(true);

            EndPath();
        }

        if(collision.CompareTag("EnemyTrail")) {
            Destroy(collision.gameObject);
            AdvancedWaveSpawnner.Instance.CurrentEnemiesLeft++;
        }
    }

    void EndPath()
    {
        PlayerStats.Lives--;
    }

}
