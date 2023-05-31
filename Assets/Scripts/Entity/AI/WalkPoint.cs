using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPoint : MonoBehaviour {
    public static List<WalkPoint> points = new List<WalkPoint>();

    private Renderer rend;
    private Light pointLight;

    void Awake () {
        rend = GetComponent<Renderer>();
        pointLight = GetComponent<Light>();

        Debug.Log("WALKPOINT");
        points.Add(this);
    }
    void Start () {

    }
    void Update () {
        rend.enabled = GameManager.instance.debugOn;
        pointLight.enabled = GameManager.instance.debugOn;
    }
}
