using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {
    private Renderer rend;
    private Light pointLight;
    private bool inPlayerBounds;

    void Awake () {
        rend = GetComponent<Renderer>();
        pointLight = GetComponent<Light>();
    }
    void Update () {
        rend.enabled = GameManager.instance.debugOn;
        pointLight.enabled = GameManager.instance.debugOn;
        inPlayerBounds = GameManager.instance.player.viewBounds.InBounds(transform.position);
    }
}
