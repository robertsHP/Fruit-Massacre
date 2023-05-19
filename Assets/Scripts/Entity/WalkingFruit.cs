using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingFruit : MonoBehaviour {
    private Animator animator;

    void Start() {
        animator = gameObject.GetComponent<Animator>();
        // animator.Play("Walk");
    }
    void Update() {
        if(Input.GetKey("m")) {
            animator.SetBool("isWalking", true);
        } else {
            animator.SetBool("isWalking", false);
        }
        if(Input.GetKey("k")) {
            Destroy(gameObject);
        }
    }
}
