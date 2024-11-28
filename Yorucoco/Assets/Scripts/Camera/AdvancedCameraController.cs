using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedCameraController : MonoBehaviour
{
    public Transform cameraTransform;


    public float normalSpeed;
    public float fastSpeed;

    private float moveSpeed;
    public float moveTime;


    public float rotationAmount;

    public Vector3 zoomAmount;

    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public Vector3 dragCurrentPosition;
    public Vector3 dragStartPosition;

    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;


    #region singleton
    public bool isEnabled = true;

    public static AdvancedCameraController Instance;

    private void Awake() {
        Instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnabled) return;
        HandleMouseInput();
    }

    private void FixedUpdate() {
        if (!isEnabled) return;
        HandleMovementInput();
    }

    void HandleMouseInput() {
        if (Input.mouseScrollDelta.y != 0) {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }

        if(Input.GetMouseButtonDown(2)) {
            rotateStartPosition = Input.mousePosition;
        }
        if(Input.GetMouseButton(2)) {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;
            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5));
        }
    }

    void HandleMovementInput(){
        
        if(Input.GetKey(KeyCode.LeftShift)) {
            moveSpeed = fastSpeed;
        } else {
            moveSpeed = normalSpeed;
        }

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            newPosition += (transform.forward * moveSpeed);
        }

        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            newPosition += (transform.forward * -moveSpeed);
        }

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            newPosition += (transform.right * moveSpeed);
        }

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            newPosition += (transform.right * -moveSpeed);
        }

        if(Input.GetKey(KeyCode.Q)) {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }
        
        if(Input.GetKey(KeyCode.E)) {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * moveTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * moveTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * moveTime);
    }
}
