using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerPoint : MonoBehaviour {
    public static List<Transform> points = new List<Transform>();
    private Renderer rend;
    private Light light;

    void Awake () {
        points.Add(transform);
        rend = GetComponent<Renderer>();
        light = GetComponent<Light>();
    }
    void Update () {
        rend.enabled = GameManager.instance.debugOn;
        light.enabled = GameManager.instance.debugOn;
    }
}
