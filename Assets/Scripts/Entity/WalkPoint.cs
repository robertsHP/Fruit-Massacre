using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPoint : MonoBehaviour {
    public static List<Transform> points = new List<Transform>();
    private Renderer rend;
    private Light pointLight;

    void Awake () {
        points.Add(transform);
        rend = GetComponent<Renderer>();
        pointLight = GetComponent<Light>();
    }
    void Update () {
        rend.enabled = GameManager.instance.debugOn;
        pointLight.enabled = GameManager.instance.debugOn;
    }
}
