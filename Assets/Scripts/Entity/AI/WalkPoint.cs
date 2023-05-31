using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPoint : MonoBehaviour {
    private Renderer rend;
    private Light pointLight;

    void Awake () {
        rend = GetComponent<Renderer>();
        pointLight = GetComponent<Light>();
    }
    void Start () {
        GameManager.instance.WalkPoints.Add(this);
    }
    void Update () {
        rend.enabled = GameManager.instance.debugOn;
        pointLight.enabled = GameManager.instance.debugOn;
    }
}
