using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points;

    private int currentWaypoint;

    private void Awake()
    {
        points = new Transform[transform.childCount]; //Create spaces for the array.
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }


}
