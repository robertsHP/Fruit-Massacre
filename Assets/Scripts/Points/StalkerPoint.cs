using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerPoint : Point {
    private Stalker occupier;

    void Start () {
        GameManager.instance.stalkerPoints.Add(this);
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
