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

        points.Add(this);
    }
    void Start () {

    }
    void Update () {
        rend.enabled = GameManager.instance.debugOn;
        pointLight.enabled = GameManager.instance.debugOn;
    }
    // public void SpawnGameObject (GameObject gObj) {
    //     if(objectsToSpawn.Count != 0) {
    //         int index = (int) Random.Range(0, objectsToSpawn.Count);
    //         GameObject obj = objectsToSpawn.ElementAt(index);
    //         Instantiate(obj, transform);
    //     }
    // }
}
