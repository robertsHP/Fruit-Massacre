using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour {
    public GameObject GameObjectInView {get; set;}

    [SerializeField] private string gameObjectTag;
    private Renderer rend;

    void Start() {
        rend = GetComponent<Renderer>();
    }
    void Update () {
        rend.enabled = GameManager.instance.debugOn;
    }

    void OnTriggerEnter (Collider col) {
        if(col.CompareTag(gameObjectTag)) {
            // Debug.Log("Enter Cone "+col.gameObject);
            GameObjectInView = col.gameObject;
        }
    }
    void OnTriggerExit (Collider col) {
        if(col.CompareTag(gameObjectTag)) {
            // Debug.Log("Exit Cone "+col.gameObject);
            GameObjectInView = null;
        }
    }
}