using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPoint : Point {
    void Start () {
        GameManager.instance.walkPoints.Add(this);
    }
}
