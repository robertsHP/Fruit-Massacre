using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingFruit : MonoBehaviour {
    private static bool objectSpawnDone = false;
    public static uint totalCount;
    public static uint killCount = 0;

    public GameObject bloodParticle;
    private Animator animator;

    void Awake () {
        if(!objectSpawnDone) {
            totalCount++;
        }
    }
    void Start() {
        //crappy but whatever
        killCount = 0;
        objectSpawnDone = true;
        
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
            OnFruitKilled();
            Instantiate(bloodParticle, transform.position + new Vector3(0, 2, 0), transform.rotation);
            Destroy(gameObject);
        }
    }
    void OnFruitKilled () {
        killCount++;
        UIManager.instance.UpdateFruitCount();
        if(killCount >= totalCount) {
            GameManager.instance.CurrentState = GameState.Win;
        }
    }
}
