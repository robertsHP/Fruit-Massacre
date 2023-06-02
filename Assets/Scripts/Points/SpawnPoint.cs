using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : Point {
    void Start() {
        GameManager.instance.spawnPoints.Add(this);
    }
    public void SpawnGameObject (GameObject obj) {
        Instantiate(obj, transform);
    }
}
