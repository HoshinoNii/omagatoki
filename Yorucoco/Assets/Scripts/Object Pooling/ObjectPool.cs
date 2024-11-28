using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Tower_Defense/ObjectPool", order = 1)]

//Scriptable Pools for easier management!
public class ObjectPool : ScriptableObject
{

    public string tag;
    public GameObject prefab;
    public int Size;
}
