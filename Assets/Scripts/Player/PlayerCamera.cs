using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public Transform playerBody;
    public Camera cam;

    public float mouseSensitivity = 100f;
    public float breatheEffectMax = 5f;
    public float breatheAddValue = 0.005f;

    private float startFOV;
    private float endFOV;
    private Func<float, float, float> FovAdder;

    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        startFOV = cam.fieldOfView;
        endFOV = cam.fieldOfView + breatheEffectMax;
    }

    // Update is called once per frame
    void Update() {
        MouseLook();
        CamBreathe();
    }
    private void MouseLook () {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
    private void CamBreathe () {
        if(cam.fieldOfView < endFOV && cam.fieldOfView > startFOV) {
            cam.fieldOfView = FovAdder(cam.fieldOfView, breatheAddValue);
        } else {
            FovAdder = (cam.fieldOfView >= endFOV) ? 
                (x, y) => x - y : 
                (x, y) => x + y;
            cam.fieldOfView = FovAdder(cam.fieldOfView, breatheAddValue);
        }
    }
}
