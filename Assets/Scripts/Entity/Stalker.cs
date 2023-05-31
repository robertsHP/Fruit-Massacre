using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalker : MonoBehaviour {
    public PlayerCamHolder camHolder;
    public Renderer rend;

    public float maxDistance = 10f;
    public Color raycastColor = Color.red;

    private bool stareCoroutineOn = false;
    private Coroutine stareCoroutine;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

    }
    void FixedUpdate () {
        StareTimer();
    }

    void TeleportTimer () {
        
    }

    void StareTimer () {
        //if(rend.isVisible) {
        if(IsObjectFullyVisible() && IsObjectNotBlocked()) {
            if(!stareCoroutineOn) {
                Debug.Log("!!Start coroutine");
                stareCoroutine = StartCoroutine(StareTimerCoroutine());
                stareCoroutineOn = true;
            }
        } else {
            if(stareCoroutineOn) {
                Debug.Log("!!End coroutine");
                StopCoroutine(stareCoroutine);
                stareCoroutineOn = false;
            }
        }
    }

    bool IsObjectFullyVisible() {
        Camera mainCamera = camHolder.cam;

        // Get the viewport position of the target object's bounds
        Vector3 targetViewportPos = mainCamera.WorldToViewportPoint(rend.bounds.center);

        // Check if the target object's bounds are fully within the camera's viewport
        if (targetViewportPos.x >= 0f && targetViewportPos.x <= 1f &&
            targetViewportPos.y >= 0f && targetViewportPos.y <= 1f &&
            targetViewportPos.z > 0f) {
            // Target object is fully within the camera's view
            return true;
        }

        // Target object is not fully within the camera's view
        return false;
    }
    bool IsObjectNotBlocked () {
        //Check if something is blocking the gameObject
        Ray ray = new Ray(camHolder.cam.transform.position, camHolder.cam.transform.forward);
        RaycastHit hit;
        if (Physics.Linecast(camHolder.cam.transform.position, transform.position, out hit)) {
            if (hit.collider.gameObject == gameObject) {
                if(GameManager.instance.debugOn) {
                    Debug.DrawRay(ray.origin, ray.direction * maxDistance, raycastColor);
                }
                return true;
            }
        }
        return false;
    }
    IEnumerator StareTimerCoroutine () {
        // gameObject.GetComponent<AudioSource>().Play();

        // Debug.Log("STARE TIMER BEGIN");

        yield return new WaitForSeconds(5);

        // Debug.Log("KILL PLAYER");
        GameManager.instance.CurrentState = GameState.Lose;
    }
}
