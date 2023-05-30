using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour {
    public static int totalGameObjectAmount;

    void Awake () {
        totalGameObjectAmount++;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.localRotation = Quaternion.Euler(90f, Time.time * 100f, 0);
    }
    void OnTriggerEnter (Collider other) {
        // if(other.CompareTag("Player")) {
        //     GameManager.instance.FruitCollected();
        //     Destroy(gameObject);
        // }
    }
}
