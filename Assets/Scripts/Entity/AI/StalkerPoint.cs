using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerPoint : MonoBehaviour {
    private Renderer rend;
    private Light pointLight;

    private Stalker occupier;

    void Awake () {        
        rend = GetComponent<Renderer>();
        pointLight = GetComponent<Light>();
    }
    void Start () {
        GameManager.instance.StalkerPoints.Add(this);
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
