using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WalkingFruitTitleScreen : MonoBehaviour {
    [SerializeField] private Animator animator;
    
    private float speed = 0.06f; // Speed of movement

    private Transform startTransform;
    private Transform endTransform;

    private float distance; // Distance between points
    private float startTime; // Start time of interpolation

    void Start() {
        startTransform = transform;
        if(DeletePoint.points.Count != 0) {
            endTransform = DeletePoint.points.ElementAt(0).transform;
            distance = Vector3.Distance(startTransform.position, endTransform.position);
        }
        animator.SetBool("isWalking", true);
    }

    // Update is called once per frame
    void Update() {
        if(endTransform != null) {
            float duration = distance / speed;
            float t = speed * Time.deltaTime;

            transform.rotation = Quaternion.RotateTowards(startTransform.rotation, endTransform.rotation, 1f * Time.deltaTime);
            transform.position = Vector3.Lerp(startTransform.position, endTransform.position, t);
        }
    }
}
