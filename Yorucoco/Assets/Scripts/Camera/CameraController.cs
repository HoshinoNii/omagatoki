using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovment = true;

    public float panSpeed = 90f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 20f;

    [Header("Zoom Smoothing")]
    float zoomTime;
    float zoomTarget;
    float lastScrollWheelDirection;

    [Header("Clamp For Camera")]
    public float minY = 50f;
    public float maxY = 125f;

    public float minZ = -200f;
    public float maxZ = 200f;

    public float minX = -200f;
    public float maxX = 200f;

    //[Header("Camera Rotate")]
    //public Transform target;
    //int degrees;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
            doMovment = !doMovment;

        if (!doMovment)
            return;

        if (Input.GetKey("d"))
        {
            transform.Translate((Vector3.forward + Vector3.left) * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a"))
        {
            transform.Translate((Vector3.back + Vector3.right) * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s"))
        {
            transform.Translate((Vector3.right + Vector3.forward) * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("w"))
        {
            transform.Translate((Vector3.left + Vector3.back) * panSpeed * Time.deltaTime, Space.World);
        }

        float ScrollWheel = Input.GetAxis("Mouse ScrollWheel");

        //Not Smooth Camera
        Vector3 pos = transform.position;

        //pos.y -= ScrollWheel * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;

        //Rotate Camera
        /*if (Input.GetMouseButton(1))
        {
            degrees = 5;
            transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * degrees);
            //transform.RotateAround (target.position, Vector3.left, Input.GetAxis ("Mouse Y")* dragSpeed);
        }
        if (!Input.GetMouseButton(1))
            transform.RotateAround(target.position, Vector3.up, degrees * Time.deltaTime);
        else
        {
            degrees = 0;
        }*/

        // If this camera is currently zooming in and the player started zooming
        // out (or vice versa), reset the amount that is remaining to be zoomed
        if ((this.lastScrollWheelDirection > 0 && ScrollWheel < 0) ||
            (this.lastScrollWheelDirection < 0 && ScrollWheel > 0))
        {
            this.zoomTarget = 0;
        }
        if (ScrollWheel != 0)
        {
            this.lastScrollWheelDirection = ScrollWheel;
        }

        // zoomTarget is the total distance that is remaining to be zoomed.
        // Each frame that the scroll wheel is moved, we'll add a little more
        // to the distance that we want to zoom
        zoomTarget += ScrollWheel * this.scrollSpeed;

        // zoomTime is used to do linear interpolation to create a smooth zoom.
        // Each time the player moves the mouse wheel, we reset zoomTime so that 
        // we restart our linear interpolation
        if (ScrollWheel != 0)
        {
            this.zoomTime = 0;
        }

        if (this.zoomTarget != 0)
        {
            this.zoomTime += Time.deltaTime;

            // Calculate how much our camera will be moved this frame using linear
            // interpolation.  You can adjust how fast the camera zooms by
            // changing the divisor for zoomTime
            var translation = Vector3.Lerp(
                new Vector3(0, 0, 0),
                new Vector3(0, this.zoomTarget, 0),
                zoomTime / 10f);   // see comment above

            // Zoom the camera by the amount that we calculated for this frame
            this.transform.position -= translation;

            // Decrease the amount that's remaining to be zoomed by the amount 
            // that we zoomed this frame
            this.zoomTarget -= translation.y;
        }

    }
    
}
