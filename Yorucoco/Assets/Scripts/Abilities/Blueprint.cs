using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{
    public static Blueprint instance;

    //Blueprint is a script that allows the feature of dragging and dropping certain items and also spawnning them!
    public Transform ObjectToMove;
    public GameObject prefab;
    public PoolIndex PoolPrefab;
    public GameObject blueprint;
    public LayerMask mask;
    int LastPosX, LastPosY, LastPosZ;
    Vector3 MousePos;

    //To rotate blueprint
    float degrees = 90f;

    //tower buildManager
    BuildManager buildManager;
    public int Cost;
    public bool CostsMoney;

    #region singleton
    [HideInInspector]
    private static Blueprint _instance;
    public static Blueprint Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("Instance of such blueprint exists");
            gameObject.SetActive(false);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        MousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(MousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50000f, mask))
        {
            //Debug.Log(hit.collider);
            int PosX = (int)Mathf.Round(hit.point.x);
            int PosY = (int)Mathf.Round(hit.point.y);
            int PosZ = (int)Mathf.Round(hit.point.z);

            if (PosX != LastPosX || PosY != LastPosY || PosZ != LastPosZ)
            {
                LastPosX = PosX;
                LastPosY = PosY;
                LastPosZ = PosZ;
                //Debug.Log(LastPosX + " " + LastPosZ);
                ObjectToMove.position = new Vector3(PosX, PosY, PosZ);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {

            //Debug.Log(buildManager.CanBuild);
            if (prefab != null)
            {
                
                Destroy(blueprint);
                if (PoolPrefab.ToString() == "Nothing")
                {
                    if(!CostsMoney)
                        Instantiate(prefab, ObjectToMove.position, Quaternion.identity);
                    else
                        Instantiate(prefab, ObjectToMove.position, Quaternion.identity);
                }  
                else
                {
                    if (!CostsMoney)
                        PoolManager.Instance.SpawnFromPool(PoolPrefab.ToString(), ObjectToMove.position, Quaternion.identity);
                    else
                        Build();
                    OnObjectTaskCompleted();
                }
                    
                return;
            }
            else if (!buildManager.CanBuild)
            {
                OnObjectTaskCompleted();
            }

        }

        if (Input.GetMouseButtonUp(1))
        {
            //print("RIGHT CLICKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
            buildManager.RestTowerBuild();
            //destoy the instance if right click is pressed
            OnObjectTaskCompleted();
        }



        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.Rotate(0, degrees, 0);
        }

    }
    public void Build()
    {
        if (PlayerStats.Money < Cost)
        {
            Debug.Log("Not Enough MONEH!");
            OnObjectTaskCompleted();
            
            return;
        }
        PlayerStats.Money -= Cost;
        //Debug.Log(PlayerStats.Money);

        PoolManager.Instance.SpawnFromPool(PoolPrefab.ToString(), ObjectToMove.position, Quaternion.identity);
        //OnObjectTaskCompleted();
    }
  

    public void OnObjectTaskCompleted()
    {
        _instance = null;
        Destroy(gameObject);
    }
}