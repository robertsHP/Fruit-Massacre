using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerPoint : MonoBehaviour {
    public static List<StalkerPoint> points = new List<StalkerPoint>();

    private Renderer rend;
    private Light pointLight;

    private Stalker occupier;

    void Awake () {        
        rend = GetComponent<Renderer>();
        pointLight = GetComponent<Light>();

        Debug.Log("STALKERPOINT");
        points.Add(this);
    }
    void Start () {

    }
    void Update () {
        rend.enabled = GameManager.instance.debugOn;
        pointLight.enabled = GameManager.instance.debugOn;
    }
    public bool Occupied () {
        return occupier != null;
    }
    public void Occupy (Stalker nextOccupier) {
        nextOccupier.transform.position = transform.position;
        occupier = nextOccupier;
    }
    public void UnOccupy () {
        occupier = null;
    }
}
