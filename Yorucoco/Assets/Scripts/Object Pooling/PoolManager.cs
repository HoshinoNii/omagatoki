using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    private void Awake() {
        Instance = this;
        //Debug.Log(Instance);
    }


    public List<ObjectPool> Pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;



    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (ObjectPool Pool in Pools) {

            
            Queue<GameObject> objectPool = new Queue<GameObject>();

            //generate the pool
            for( int i = 0; i < Pool.Size; i++ ) {

                GameObject obj = Instantiate(Pool.prefab);
                //disable them once its instantiated
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(Pool.tag, objectPool);
            //Debug.Log(Pool.tag);
        }
        
    }

    //deploy pooled Object
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation) {
        
        if(!poolDictionary.ContainsKey(tag)) {
            Debug.LogWarning("Pool with tag " + tag + "Does not exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();


        poolDictionary[tag].Enqueue(objectToSpawn);

        // if(pooledObject != null) {
        //     pooledObject.OnObjectTaskComplete();
        // }
        
        return objectToSpawn;
    }
}
