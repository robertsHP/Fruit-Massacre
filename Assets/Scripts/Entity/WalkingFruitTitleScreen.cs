using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WalkingFruitTitleScreen : MonoBehaviour {
    [SerializeField] private Animator animator;
    
    private float speed = 0.06f;

    private Transform startTransform;
    private Transform endTransform;

    private float distance;
    private float startTime; 

    void Start() {
        startTransform = transform;
        if(MenuDeletePoint.points.Count != 0) {
            endTransform = MenuDeletePoint.points.ElementAt(0).transform;
            distance = Vector3.Distance(startTransform.position, endTransform.position);
        }
    }

    void Update() {
        if(endTransform != null) {
            float t = speed * Time.deltaTime;

            transform.rotation = Quaternion.RotateTowards(startTransform.rotation, endTransform.rotation, 1f * Time.deltaTime);
            transform.position = Vector3.Lerp(startTransform.position, endTransform.position, t);
        }
    }
}
