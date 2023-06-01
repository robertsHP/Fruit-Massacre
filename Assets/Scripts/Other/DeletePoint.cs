using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePoint : MonoBehaviour {
    public static List<DeletePoint> points = new List<DeletePoint>();

    void Start () {
        points.Add(this);
    }

    void OnTriggerEnter (Collider col) {
        if(col.CompareTag("Fruit"))
            Destroy(col.gameObject);
    }
}
