using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Roadblock : MonoBehaviour
{   
    [Header("Behaviour")]
    public Quaternion newRotation;
    public float LerpSpeed = 5;

    private float currentDelay;
    public float ButtonDelay;

    public float rayLength;
    public LayerMask layerMask;
    public Transform pivotPoint;

    private void Start() {
        newRotation = pivotPoint.transform.rotation; 
    }
    private void Update() {
       transform.rotation = Quaternion.Lerp(pivotPoint.transform.rotation, newRotation, Time.deltaTime * LerpSpeed);

       if(currentDelay <= 0) {
           currentDelay = 0;
       } else {
           currentDelay -= Time.deltaTime;
       }
    }

    private void OnMouseEnter() {
        
    }

    private void OnMouseUp()
    {
        RoadblockUI.Instance.SetTarget(this);
    }


    public void Rotate90CW() {
        if(currentDelay > 0) return;
        currentDelay = ButtonDelay;


        newRotation = Quaternion.Euler(
            pivotPoint.transform.rotation.eulerAngles.x, 
            transform.rotation.eulerAngles.y + 90f, 
            transform.rotation.eulerAngles.z);
    }
    public void Rotate90ACW() {
        if(currentDelay > 0) return;
        currentDelay = ButtonDelay;

        newRotation = Quaternion.Euler(
            pivotPoint.transform.rotation.eulerAngles.x, 
            pivotPoint.transform.rotation.eulerAngles.y - 90f, 
            pivotPoint.transform.rotation.eulerAngles.z);

         //transform.rotation = Quaternion.Lerp( transform.rotation.eulerAngles, transform.rotation.eulerAngles += 90);
    }

    internal Vector3 GetPosition()
    {
        print(transform.position + " " + gameObject.name);
        return transform.position;
    }
}
