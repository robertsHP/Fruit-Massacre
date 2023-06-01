using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingFruit : MonoBehaviour {
    public static uint totalFruitCount;
    public static uint fruitKillCount;

    [SerializeField] public GameObject bloodParticle;
    [SerializeField] public Animator animator;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public MovementAI movementAI;

    private WalkPoint currentWalkPoint;

    void Awake () {
        totalFruitCount++;
    }
    void Start() {
        animator.SetBool("isWalking", true);
    }
    void Update() {
        if(GameManager.instance.CurrentState == GameState.Game) {
            currentWalkPoint = movementAI.Patrol(agent, currentWalkPoint);
        } else {
            movementAI.Idle(agent, currentWalkPoint);
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
        fruitKillCount++;
        UIManager.instance.UpdateFruitCount();
        if(fruitKillCount >= totalFruitCount) {
            GameManager.instance.CurrentState = GameState.Win;
        }
    }
}
