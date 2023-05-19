using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public Camera cam;

    private float startFOV;
    private float endFOV;
    private float camIncrement = 0.1f;

    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        startFOV = cam.fieldOfView;
        endFOV = cam.fieldOfView + 200f;
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
        // if(cam.fieldOfView > endFOV) {
        //     Debug.Log(cam.fieldOfView+" - -");
        //     cam.fieldOfView -= camIncrement;
        // } else if (cam.fieldOfView < startFOV) {
        //     Debug.Log(cam.fieldOfView+" - +");
        //     cam.fieldOfView += camIncrement;
        // }
    }
}
