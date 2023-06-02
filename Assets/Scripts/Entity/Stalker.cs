using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stalker : Enemy {
    [SerializeField] private Renderer visionCapsuleRenderer;
    [SerializeField] private uint stareTimeSeconds = 4;
    [SerializeField] private uint teleportPauseSeconds = 4;

    ////////////////////////////////////////////
    private float maxDistance = 10f;
    private Color raycastColor = Color.red;
    ////////////////////////!!!!!!!!!!!!!!!!!!!!!!!!!!!

    private StalkerPoint occupiedPoint;

    private bool stareCoroutineOn = false;
    private Coroutine stareCoroutine;

    private bool teleportOn = false;

    // Start is called before the first frame update
    void Start() {
        GameManager.instance.enemies.Add(this);
    }

    // Update is called once per frame
    void Update() {
        if(GameManager.instance.CurrentState == GameState.Game) {
            RotationTowardsPlayer();
            if(!teleportOn) 
                StartCoroutine(TeleportCoroutine());
            StareTimer();
        }
    }

    void RotationTowardsPlayer () {
        Player player = GameManager.instance.player;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, player.transform.rotation, 8f * Time.deltaTime);
    }

    IEnumerator TeleportCoroutine () {
        teleportOn = true;

        if(occupiedPoint != null)
            occupiedPoint.UnOccupy();

        List<StalkerPoint> stalkerPoints = GameManager.instance.stalkerPoints;
        int stalkerPointCount = stalkerPoints.Count;
        int stalkerPointIndex = (int) Random.Range(0, stalkerPointCount);
        
        occupiedPoint = stalkerPoints.ElementAt(stalkerPointIndex);

        occupiedPoint.Occupy(this);
        yield return new WaitForSeconds(teleportPauseSeconds);

        teleportOn = false;
    }

    void StareTimer () {
        if(IsObjectFullyVisible() && IsObjectNotBlocked()) {
            if(!stareCoroutineOn) {
                Debug.Log("!!Start stare coroutine");
                stareCoroutine = StartCoroutine(StareTimerCoroutine());
                stareCoroutineOn = true;
            }
        } else {
            if(stareCoroutineOn) {
                Debug.Log("!!End stare coroutine");
                StopCoroutine(stareCoroutine);
                stareCoroutineOn = false;
            }
        }
    }

    bool IsObjectFullyVisible() {
        Camera playerCamera = GameManager.instance.player.camHolder.cam;

        // Get the viewport position of the target object's bounds
        Vector3 targetViewportPos = playerCamera.WorldToViewportPoint(visionCapsuleRenderer.bounds.center);

        // Target object is fully within the camera's view
        return targetViewportPos.x >= 0f && targetViewportPos.x <= 1f &&
                targetViewportPos.y >= 0f && targetViewportPos.y <= 1f &&
                targetViewportPos.z > 0f;
    }
    bool IsObjectNotBlocked () {
        Camera playerCamera = GameManager.instance.player.camHolder.cam;

        //Check if something is blocking the gameObject
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Linecast(playerCamera.transform.position, transform.position, out hit)) {
            if (hit.collider.gameObject == gameObject) {
                //Draw ray for debugging purposes
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

        yield return new WaitForSeconds(stareTimeSeconds);

        // Debug.Log("KILL PLAYER");
        GameManager.instance.CurrentState = GameState.Lose;
    }
}
