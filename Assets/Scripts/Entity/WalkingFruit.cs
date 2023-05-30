using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingFruit : MonoBehaviour {
    public static uint totalCount;
    public static uint currentCount = 0;

    public GameObject bloodParticle;
    private Animator animator;

    void Awake () {
        totalCount++;
    }
    void Start() {
        animator = gameObject.GetComponent<Animator>();
    }
    void Update() {
        if(GameManager.instance.CurrentState == GameState.Game) {
            animator.SetBool("isWalking", Input.GetKey("m"));
            if(Input.GetKey("k")) {
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerEnter (Collider other) {
        if(other.CompareTag("Player")) {
            OnFruitCollected();
            Instantiate(bloodParticle, transform.position + new Vector3(0, 2, 0), transform.rotation);
            Destroy(gameObject);
        }
    }
    void OnFruitCollected () {
        currentCount++;
        UIManager.instance.UpdateFruitCount();
        if(currentCount >= totalCount) {
            GameManager.instance.CurrentState = GameState.Win;
        }
    }
}
