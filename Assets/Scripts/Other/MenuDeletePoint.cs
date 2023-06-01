using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDeletePoint : MonoBehaviour {
    public static List<MenuDeletePoint> points = new List<MenuDeletePoint>();

    void Start () {
        points.Add(this);
    }

    void OnTriggerEnter (Collider col) {
        if(col.CompareTag("Fruit"))
            Destroy(col.gameObject);
    }
}
