using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells_Slow : MonoBehaviour
{
    public float Duration;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("OnObjectTaskCompleted", Duration);
    }


    void OnTriggerStay(Collider collision) {
        if(collision.CompareTag("Enemy")) {
            collision.GetComponent<EnemyDebuffs>().Slow(5f);   
        }
    }

    void OnObjectTaskCompleted() {
        gameObject.SetActive(false);
    }
}
