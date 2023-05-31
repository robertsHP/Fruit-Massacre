using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingFruit : MonoBehaviour {
    public GameObject bloodParticle;
    private Animator animator;

    public NavMeshAgent agent;
    public MovementAI movementAI;

    private WalkPoint currentWalkPoint;

    void Awake () {

    }
    void Start() {
        GameManager.instance.TotalFruitCount++;
        animator = gameObject.GetComponent<Animator>();

        animator.SetBool("isWalking", true);
    }
    void Update() {
        if(GameManager.instance.CurrentState == GameState.Game) {
            currentWalkPoint = movementAI.Patrol(agent, currentWalkPoint);

            // animator.SetBool("isWalking", true);

            // animator.SetBool("isWalking", Input.GetKey("m"));
            // if(Input.GetKey("k")) {
            //     OnFruitKilled();
            // }
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
        GameManager.instance.FruitKillCount++;
        UIManager.instance.UpdateFruitCount();
        if(GameManager.instance.FruitKillCount >= GameManager.instance.TotalFruitCount) {
            GameManager.instance.CurrentState = GameState.Win;
        }
    }
}
