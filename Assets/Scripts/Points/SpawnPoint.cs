using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : Point {
    void Start() {
        SpawnManager.instance.spawnPoints.Add(this);
    }
    public void SpawnGameObject (GameObject obj) {
        Instantiate(obj, transform.position, new Quaternion(0f, 0f, 0f, 0f));
    }
}
