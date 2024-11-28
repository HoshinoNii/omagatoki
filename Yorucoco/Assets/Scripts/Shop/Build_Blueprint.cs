using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build_Blueprint : MonoBehaviour
{
    //BLUEPRINT BUILDER: this is a simple script that can be reused on UI buttons to build custom items such as roadblocks or spells!
    //public PoolIndex whatToBuild;
    public GameObject whatToBuild;

    public Blueprint blueprint;


    public void Build_Item() 
    {
        //PoolManager.Instance.SpawnFromPool(whatToBuild.ToString(), transform.position, transform.rotation);
        if(Blueprint.Instance == null) {
            Instantiate(whatToBuild, transform.position, Quaternion.identity);
        }
    }

}
